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

        public AccessTermService(ApiDbContext context, IHttpContextAccessor httpContext,
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
                .If(includeText, q => q.Include(at => at.UserClause).Include(at => at.LimitsConditionsClause))
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
                .If(filters.YearAccepted.HasValue, q => q.Where(at => at.AcceptedDate.HasValue && at.AcceptedDate.Value.Year == filters.YearAccepted))
                .If(filters.OnlyLatest, q => q.Take(1))
                .If(filters.Accepted == true, q => q.Where(at => at.AcceptedDate.HasValue))
                .If(filters.Accepted == false, q => q.Where(at => !at.AcceptedDate.HasValue))
                .If(filters.IncludeText, q => q.Include(at => at.UserClause).Include(at => at.LimitsConditionsClause))
                .ToArrayAsync();

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
                UserClauseId = await GetCurrentAgreementIdForUserAsync(enrollee),
                LimitsConditionsClause = await GenerateLimitsAndConditionsClause(enrollee.Id),
                CreatedDate = DateTimeOffset.Now
            };

            _context.Add(accessTerm);
            await _context.SaveChangesAsync();
        }

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

        public async Task ExpireCurrentAccessTermAsync(int enrolleeId)
        {
            var accessTerm = await _context.AccessTerms
                .OrderByDescending(at => at.CreatedDate)
                .FirstOrDefaultAsync(at => at.EnrolleeId == enrolleeId);

            if (accessTerm != null)
            {
                accessTerm.ExpiryDate = DateTimeOffset.Now;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Returns true if the enrollees' most recent accepted access term has no newer versions
        /// </summary>
        public async Task<bool> IsCurrentByEnrolleeAsync(Enrollee enrollee)
        {
            var currentAgreement = await GetCurrentAgreementIdForUserAsync(enrollee);

            if (enrollee.AccessTerms == null)
            {

            }

            _context.UserClauses.wher

            return enrollee.a

            // var accessTerm = await _context.AccessTerms
            //     .Include(at => at.AccessTermLicenseClassClauses)
            //     .Where(at => at.EnrolleeId == enrollee.Id)
            //     .Where(at => at.AcceptedDate != null)
            //     .OrderByDescending(at => at.AcceptedDate)
            //     .FirstOrDefaultAsync();

            // if (accessTerm != null)
            // {
            //     var currentAccessTerm = await GenerateAccessTermAsync(enrollee);

            //     if (accessTerm.GlobalClauseId != currentAccessTerm.GlobalClause.Id
            //        || accessTerm.UserClauseId != currentAccessTerm.UserClause.Id)
            //     {
            //         current = false;
            //     }

            //     foreach (var lcc in accessTerm.AccessTermLicenseClassClauses)
            //     {
            //         if (currentAccessTerm.AccessTermLicenseClassClauses.FindAll(c => c.LicenseClassClause.Id == lcc.LicenseClassClauseId).Count == 0)
            //         {
            //             current = false;
            //         }
            //     }
            // }
            // else
            // {
            return false;
            // }

            // return current;
        }

        /// <summary>
        /// Enrollee’s that are:
        /// Under Review ->  -> Current TOA status: --
        /// Locked, Declined -> Current TOA status: NA
        /// Required TOA -> Current TOA status: Pending
        /// Editable (AND before their renewal date) -> Current TOA status: Is their signed TOA the most current version
        /// Editable (AND on/after their renewal date) -> Current TOA status: --
        /// </summary>
        public async Task<string> GetCurrentTOAStatusAsync(Enrollee enrollee)
        {
            var currentStatus = enrollee.CurrentStatus;
            var toaStatus = "";

            if (currentStatus.IsType(StatusType.Locked) || currentStatus.IsType(StatusType.Declined))
            {
                toaStatus = "N/A";
            }
            else if (currentStatus.IsType(StatusType.RequiresToa))
            {
                toaStatus = "Pending";
            }
            else if (currentStatus.IsType(StatusType.Editable))
            {
                var accessTerm = await GetMostRecentAcceptedEnrolleesAccessTermAsync(enrollee.Id);
                var isCurrent = await this.IsCurrentByEnrolleeAsync(enrollee);

                if (accessTerm?.ExpiryDate > DateTimeOffset.Now)
                {
                    toaStatus = (accessTerm != null && isCurrent)
                        ? "Yes"
                        : "No";
                }
            }
            return toaStatus;
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

        private async Task<int> FetchNewestAgreementIdOfType<T>() where T : UserClause
        {
            return await _context.UserClauses
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
