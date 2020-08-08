using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public class AccessTermService : BaseService, IAccessTermService
    {
        private static readonly TimeSpan ACCESS_TERM_EXPIRY = TimeSpan.FromDays(365);

        private readonly IRazorConverterService _razorConverterService;

        public AccessTermService(
            ApiDbContext context, IHttpContextAccessor httpContext,
            IRazorConverterService razorConverterService)
            : base(context, httpContext)
        {
            _razorConverterService = razorConverterService;
        }

        /// <summary>
        /// Gets the access term for an enrollee by ID, if it exists (No Tracking).
        /// </summary>
        public async Task<AccessTerm> GetEnrolleeAccessTermAsync(int enrolleeId, int accessTermId, bool includeText = false)
        {
            var accessTerm = await _context.AccessTerms
                .AsNoTracking()
                .Where(at => at.Id == accessTermId)
                .Where(at => at.EnrolleeId == enrolleeId)
                .If(includeText, q => q.Include(at => at.Agreement).Include(at => at.LimitsConditionsClause))
                .SingleOrDefaultAsync();

            if (includeText)
            {
                await RenderHtml(accessTerm);
            }

            return accessTerm;
        }

        /// <summary>
        /// Get the list of access terms for an enrollee, using filters (No Tracking).
        /// </summary>
        public async Task<IEnumerable<AccessTerm>> GetAccessTermsAsync(int enrolleeId, AccessTermFilters filters)
        {
            filters = filters ?? new AccessTermFilters();

            var accessTerms = await _context.AccessTerms
                .AsNoTracking()
                .Where(at => at.EnrolleeId == enrolleeId)
                .OrderByDescending(at => at.CreatedDate)
                .If(filters.OnlyLatest, q => q.Take(1))
                .If(filters.Accepted == true || filters.YearAccepted.HasValue, q => q.Where(at => at.AcceptedDate.HasValue))
                .If(filters.Accepted == false, q => q.Where(at => !at.AcceptedDate.HasValue))
                .If(filters.IncludeText, q => q.Include(at => at.Agreement).Include(at => at.LimitsConditionsClause))
                .ToArrayAsync();

            if (filters.YearAccepted.HasValue)
            {
                // NpgSQL does not support DateTimeOffset operations, this filtering must be done after fetching all the data :(
                accessTerms = accessTerms
                    .Where(at => at.AcceptedDate.Value.Year == filters.YearAccepted)
                    .ToArray();
            }

            if (filters.IncludeText)
            {
                await RenderHtml(accessTerms);
            }

            return accessTerms;
        }

        public async Task CreateEnrolleeAccessTermAsync(Enrollee enrollee)
        {
            var accessTerm = new AccessTerm
            {
                EnrolleeId = enrollee.Id,
                AgreementId = await GetCurrentAgreementIdForUserAsync(enrollee),
                LimitsConditionsClause = await GenerateLimitsAndConditionsClause(enrollee.Id),
                CreatedDate = DateTimeOffset.Now
            };

            _context.Add(accessTerm);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Accepts the Enrollee's newest Access Term, if it hasn't already been accepted.
        /// </summary>
        public async Task AcceptCurrentAccessTermAsync(int enrolleeId)
        {
            var accessTerm = await _context.AccessTerms
                .OrderByDescending(at => at.CreatedDate)
                .FirstAsync(at => at.EnrolleeId == enrolleeId);

            if (accessTerm.AcceptedDate == null)
            {
                accessTerm.AcceptedDate = DateTimeOffset.Now;
                accessTerm.ExpiryDate = DateTimeOffset.Now.Add(ACCESS_TERM_EXPIRY);

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Expires the Enrollee's most recently accepted Access Term.
        /// </summary>
        public async Task ExpireCurrentAccessTermAsync(int enrolleeId)
        {
            var accessTerm = await _context.AccessTerms
                .OrderByDescending(at => at.CreatedDate)
                .Where(at => at.EnrolleeId == enrolleeId)
                .FirstOrDefaultAsync(at => at.AcceptedDate.HasValue);

            if (accessTerm != null)
            {
                accessTerm.ExpiryDate = DateTimeOffset.Now;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Renders the HTML text of the Access Term for viewing on the frontend.
        /// </summary>
        private async Task RenderHtml(params AccessTerm[] accessTerms)
        {
            foreach (var term in accessTerms)
            {
                if (term != null)
                {
                    term.TermsOfAccess = await _razorConverterService.RenderViewToStringAsync("/Views/TermsOfAccess.cshtml", term);
                }
            }
        }

        /// <summary>
        /// Gets the ID of the most current Agreement based on the scope of practice of the Enrollee.
        /// See JIRA PRIME-880
        /// </summary>
        private async Task<int> GetCurrentAgreementIdForUserAsync(Enrollee enrollee)
        {
            if (!enrollee.IsRegulatedUser())
            {
                return await FetchNewestAgreementIdOfType<OboAgreement>();
            }

            if (enrollee.HasCareSetting(CareSettingType.CommunityPharmacy))
            {
                return await FetchNewestAgreementIdOfType<CommunityPharmacistAgreement>();
            }
            else
            {
                return await FetchNewestAgreementIdOfType<RegulatedUserAgreement>();
            }
        }

        private async Task<int> FetchNewestAgreementIdOfType<T>() where T : Agreement
        {
            return await _context.Agreements
                .AsNoTracking()
                .OfType<T>()
                .OrderByDescending(a => a.EffectiveDate)
                .Select(a => a.Id)
                .FirstAsync();
        }

        /// <summary>
        /// Generates a Limits and Conditions Clause for the Enrollee, based on the text of any Access Agreement Note they have.
        /// Does not save to the database.
        /// </summary>
        private async Task<LimitsConditionsClause> GenerateLimitsAndConditionsClause(int enrolleeId)
        {
            var text = await _context.AccessAgreementNotes
                .AsNoTracking()
                .Where(n => n.EnrolleeId == enrolleeId)
                .Select(n => n.Note)
                .SingleOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            return new LimitsConditionsClause
            {
                Text = text,
                EffectiveDate = DateTimeOffset.Now
            };
        }
    }
}
