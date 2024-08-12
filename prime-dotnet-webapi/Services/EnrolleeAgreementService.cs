using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Prime.Extensions;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services.Razor;
using Prime.DTOs.AgreementEngine;
using Prime.Engines;

namespace Prime.Services
{
    public class EnrolleeAgreementService : BaseService, IEnrolleeAgreementService
    {
        private static readonly TimeSpan AgreementRenewalPeriod = TimeSpan.FromDays(365);

        private readonly IAgreementService _agreementService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IMapper _mapper;

        public EnrolleeAgreementService(
            ApiDbContext context,
            ILogger<EnrolleeAgreementService> logger,
            IAgreementService agreementService,
            IMapper mapper,
            IRazorConverterService razorConverterService)
            : base(context, logger)
        {
            _agreementService = agreementService;
            _mapper = mapper;
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
                .Include(at => at.SignedAgreement)
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

        public async Task<bool> IsOboToRuAgreementTypeChangeAsync(int enrolleeId)
        {
            var currentAgreementType = await GetCurrentAgreementTypeAsync(enrolleeId);

            // Get only the information needed to determine expected agreement type
            var enrollee = await _context.Enrollees
                .AsNoTracking()
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
                        .ThenInclude(l => l.LicenseDetails)
                .Include(e => e.EnrolleeCareSettings)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (!enrollee.ProfileCompleted)
            {
                //return false if the profile is not even completed yet.
                return false;
            }

            var agreementDto = _mapper.Map<AgreementEngineDto>(enrollee);

            var expectedAgreementType = AgreementEngine.DetermineAgreementType(agreementDto);
            return expectedAgreementType != null && currentAgreementType != null &&
                currentAgreementType.Value.IsOnBehalfOfAgreement() && expectedAgreementType.Value.IsRegulatedUserAgreement();
        }

        public async Task<AgreementGroup?> GetCurrentAgreementGroupForAnEnrolleeAsync(int enrolleeId)
        {
            var currentAgreementType = await GetCurrentAgreementTypeAsync(enrolleeId);

            if (currentAgreementType == null)
            {
                return null;
            }

            if (currentAgreementType.Value.IsOnBehalfOfAgreement())
            {
                return AgreementGroup.OnBehalfOf;
            }

            return AgreementGroup.RegulatedUser;
        }


        private async Task<AgreementType?> GetCurrentAgreementTypeAsync(int enrolleeId)
        {
            return await _context.Agreements
                .OrderByDescending(a => a.CreatedDate)
                .Where(a => a.EnrolleeId == enrolleeId)
                .Where(a => a.AcceptedDate != null)
                .Select(a => (AgreementType?)a.AgreementVersion.AgreementType)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteObsoleteEnrolleeAgreementAsync(int enrolleeId)
        {
            var agreement = await GetCurrentAgreementAsync(enrolleeId);
            if (agreement.AcceptedDate == null)
            {
                _context.Remove(agreement);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Current Agreement was expected to be unaccepted.");
            }
        }
    }
}
