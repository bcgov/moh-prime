using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Prime.Models;
using Prime.Models.Api;
using Prime.Services.Razor;

namespace Prime.Services
{
    public class EnrolleeAgreementService : BaseService, IEnrolleeAgreementService
    {
        private static readonly TimeSpan AgreementRenewalPeriod = TimeSpan.FromDays(365);

        private readonly IAgreementService _agreementService;
        private readonly IRazorConverterService _razorConverterService;

        public EnrolleeAgreementService(
            ApiDbContext context,
            ILogger<EnrolleeAgreementService> logger,
            IAgreementService agreementService,
            IRazorConverterService razorConverterService)
            : base(context, logger)
        {
            _agreementService = agreementService;
            _razorConverterService = razorConverterService;
        }

        /// <summary>
        /// Gets the agreement for an enrollee by ID, if it exists (No Tracking).
        /// </summary>
        public async Task<Agreement> GetEnrolleeAgreementAsync(int enrolleeId, int agreementId, bool includeText = false)
        {
            var agreement = await _context.Agreements
                .AsNoTracking()
                .Where(at => at.Id == agreementId)
                .Where(at => at.EnrolleeId == enrolleeId)
                .If(includeText, q => q.Include(at => at.AgreementVersion).Include(at => at.LimitsConditionsClause))
                .SingleOrDefaultAsync();

            if (includeText)
            {
                await RenderHtml(agreement);
            }

            return agreement;
        }

        /// <summary>
        /// Get the list of agreements for an enrollee, using filters (No Tracking).
        /// </summary>
        public async Task<IEnumerable<Agreement>> GetEnrolleeAgreementsAsync(int enrolleeId, AgreementFilters filters)
        {
            filters ??= new AgreementFilters();

            var agreements = await _context.Agreements
                .AsNoTracking()
                .Where(at => at.EnrolleeId == enrolleeId)
                .OrderByDescending(at => at.CreatedDate)
                .If(filters.OnlyLatest, q => q.Take(1))
                .If(filters.Accepted == true || filters.YearAccepted.HasValue, q => q.Where(at => at.AcceptedDate.HasValue))
                .If(filters.Accepted == false, q => q.Where(at => !at.AcceptedDate.HasValue))
                .If(filters.IncludeText, q => q.Include(at => at.AgreementVersion).Include(at => at.LimitsConditionsClause))
                .ToArrayAsync();

            if (filters.YearAccepted.HasValue)
            {
                // NpgSQL does not support DateTimeOffset operations, this filtering must be done after fetching all the data :(
                agreements = agreements
                    .Where(at => at.AcceptedDate.Value.Year == filters.YearAccepted)
                    .ToArray();
            }

            if (filters.IncludeText)
            {
                await RenderHtml(agreements);
            }

            return agreements;
        }

        public async Task CreateEnrolleeAgreementAsync(int enrolleeId)
        {
            var dto = await _context.Enrollees
                .AsNoTracking()
                .Where(e => e.Id == enrolleeId)
                .Select(e => new
                {
                    AssignedAgreementType = e.Submissions
                        .OrderByDescending(s => s.CreatedDate)
                        .Select(s => s.AgreementType)
                        .FirstOrDefault(),
                    e.AccessAgreementNote
                })
                .SingleAsync();

            if (dto.AssignedAgreementType == null)
            {
                throw new InvalidOperationException("Agreement type is required to approve an enrollee");
            }

            var agreement = new Agreement
            {
                EnrolleeId = enrolleeId,
                AgreementVersionId = await _agreementService.GetLatestAgreementVersionIdOfTypeAsync(dto.AssignedAgreementType.Value),
                LimitsConditionsClause = LimitsConditionsClause.FromAgreementNote(dto.AccessAgreementNote),
                CreatedDate = DateTimeOffset.Now
            };

            _context.Add(agreement);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the Enrollee's newest Agreement
        /// </summary>
        public async Task<Agreement> GetCurrentAgreementAsync(int enrolleeId)
        {
            return await _context.Agreements
                .OrderByDescending(at => at.CreatedDate)
                .FirstAsync(at => at.EnrolleeId == enrolleeId);
        }

        /// <summary>
        /// Accepts the Enrollee's newest Agreement, if it hasn't already been accepted.
        /// </summary>
        public async Task AcceptCurrentEnrolleeAgreementAsync(int enrolleeId)
        {
            var agreement = await GetCurrentAgreementAsync(enrolleeId);

            if (agreement.AcceptedDate == null)
            {
                agreement.AcceptedDate = DateTimeOffset.Now;
                agreement.ExpiryDate = DateTimeOffset.Now.Add(AgreementRenewalPeriod);

                await _context.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Expires the Enrollee's most recently accepted Agreement.
        /// </summary>
        public async Task ExpireCurrentEnrolleeAgreementAsync(int enrolleeId)
        {
            var agreement = await _context.Agreements
                .OrderByDescending(a => a.CreatedDate)
                .Where(a => a.EnrolleeId == enrolleeId)
                .FirstOrDefaultAsync(a => a.AcceptedDate.HasValue);

            if (agreement != null)
            {
                agreement.ExpiryDate = DateTimeOffset.Now;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Renders the HTML text of the Agreement for viewing on the frontend.
        /// </summary>
        private async Task RenderHtml(params Agreement[] agreements)
        {
            foreach (var agreement in agreements)
            {
                if (agreement != null)
                {
                    agreement.AgreementContent = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Agreements.Base, agreement);
                }
            }
        }
    }
}
