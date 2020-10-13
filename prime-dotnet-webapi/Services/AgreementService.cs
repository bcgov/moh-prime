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
    public class AgreementService : BaseService, IAgreementService
    {
        private static readonly TimeSpan AGREEMENT_EXPIRY = TimeSpan.FromDays(365);

        private readonly IRazorConverterService _razorConverterService;

        public AgreementService(
            ApiDbContext context, IHttpContextAccessor httpContext,
            IRazorConverterService razorConverterService)
            : base(context, httpContext)
        {
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
            filters = filters ?? new AgreementFilters();

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
            var enrollee = await _context.Enrollees
                .AsNoTracking()
                .Include(e => e.EnrolleeCareSettings)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
                .Include(e => e.AccessAgreementNote)
                .SingleAsync(e => e.Id == enrolleeId);

            var agreement = new Agreement
            {
                EnrolleeId = enrolleeId,
                AgreementVersionId = await GetCurrentAgreementVersionIdForUserAsync(enrollee),
                LimitsConditionsClause = LimitsConditionsClause.FromAgreementNote(enrollee.AccessAgreementNote),
                CreatedDate = DateTimeOffset.Now
            };

            _context.Add(agreement);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Accepts the Enrollee's newest Agreement, if it hasn't already been accepted.
        /// </summary>
        public async Task AcceptCurrentEnrolleeAgreementAsync(int enrolleeId)
        {
            var agreement = await _context.Agreements
                .OrderByDescending(a => a.CreatedDate)
                .FirstAsync(a => a.EnrolleeId == enrolleeId);

            if (agreement.AcceptedDate == null)
            {
                agreement.AcceptedDate = DateTimeOffset.Now;
                agreement.ExpiryDate = DateTimeOffset.Now.Add(AGREEMENT_EXPIRY);

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
        /// Gets the text of a given Org Agreement, optionally in the form of a Base 64 encoded PDF.
        /// Returns null if the Agreement does not exist on the given organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <param name="asEncodedPdf"></param>
        /// <returns></returns>
        public async Task<string> GetOrgAgreementTextAsync(int organizationId, int agreementId, bool asEncodedPdf = false)
        {
            var orgDto = await _context.Organizations
                .Include(o => )
                .AsNoTracking()
                .Where(a => a.Id == agreementId)
                .Where(a => a.OrganizationId == organizationId)
                .Select(a => (AgreementType?)a.AgreementVersion.AgreementType)
                .SingleOrDefaultAsync();

            return null;



            // var fileName = "CommunityPracticeOrganizationAgreement.pdf";
            // var assembly = Assembly.GetExecutingAssembly();
            // var resourcePath = assembly.GetManifestResourceNames()
            //     .Single(str => str.EndsWith(fileName));

            // string base64;
            // using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            // using (var reader = new MemoryStream())
            // {
            //     stream.CopyTo(reader);
            //     base64 = Convert.ToBase64String(reader.ToArray());
            // }

            // return Ok(ApiResponse.Result(base64));
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
                    agreement.AgreementMarkup = await _razorConverterService.RenderViewToStringAsync("/Views/TermsOfAccess.cshtml", agreement);
                }
            }
        }

        /// <summary>
        /// Gets the ID of the most current AgreementVersion based on the scope of practice of the Enrollee.
        /// See JIRA PRIME-880
        /// </summary>
        private async Task<int> GetCurrentAgreementVersionIdForUserAsync(Enrollee enrollee)
        {
            if (!enrollee.IsRegulatedUser())
            {
                return await FetchNewestAgreementVersionIdOfType(AgreementType.OboTOA);
            }

            if (enrollee.HasCareSetting(CareSettingType.CommunityPharmacy))
            {
                return await FetchNewestAgreementVersionIdOfType(AgreementType.CommunityPharmacistTOA);
            }
            else
            {
                return await FetchNewestAgreementVersionIdOfType(AgreementType.RegulatedUserTOA);
            }
        }

        private async Task<int> FetchNewestAgreementVersionIdOfType(AgreementType type)
        {
            return await _context.AgreementVersions
                .AsNoTracking()
                .OrderByDescending(a => a.EffectiveDate)
                .Where(a => a.AgreementType == type)
                .Select(a => a.Id)
                .FirstAsync();
        }
    }
}
