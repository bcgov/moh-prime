using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Services
{
    public class AgreementService : BaseService, IAgreementService
    {
        private static readonly TimeSpan AGREEMENT_EXPIRY = TimeSpan.FromDays(365);

        private readonly IMapper _mapper;
        private readonly IPdfService _pdfService;
        private readonly IRazorConverterService _razorConverterService;

        public AgreementService(
            ApiDbContext context, IHttpContextAccessor httpContext,
            IMapper mapper,
            IPdfService pdfService,
            IRazorConverterService razorConverterService)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _pdfService = pdfService;
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

        public async Task<IEnumerable<AgreementViewModel>> GetOrgAgreementsAsync(int organizationId)
        {
            return await _context.Agreements
                .AsNoTracking()
                .OrderByDescending(a => a.CreatedDate)
                .Where(a => a.OrganizationId == organizationId)
                .ProjectTo<AgreementViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the Agreement + text of a given Org Agreement, optionally with the text in the form of a Base 64 encoded PDF.
        /// Returns null if the Agreement does not exist on the given organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <param name="asEncodedPdf"></param>
        /// <returns></returns>
        public async Task<AgreementViewModel> GetOrgAgreementAsync(int organizationId, int agreementId, bool asEncodedPdf = false)
        {
            var agreementVm = await _context.Agreements
                .AsNoTracking()
                .Where(a => a.Id == agreementId && a.OrganizationId == organizationId)
                .ProjectTo<AgreementViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (agreementVm == null)
            {
                return null;
            }

            var orgName = await _context.Organizations
                .Where(o => o.Id == organizationId)
                .Select(o => o.Name)
                .SingleAsync();

            var html = await RenderOrgAgreementHtmlAsync(agreementVm.AgreementType, orgName, agreementVm.AcceptedDate, asEncodedPdf);

            if (asEncodedPdf)
            {
                var pdf = _pdfService.Generate(html);
                agreementVm.AgreementContent = Convert.ToBase64String(pdf);
            }
            else
            {
                agreementVm.AgreementContent = html;
            }

            return agreementVm;
        }

        /// <summary>
        /// Returns a Base64 encoded PDF of a given org agreement, for signing.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <returns></returns>
        public async Task<string> GetSignableOrgAgreementAsync(int organizationId, int agreementId)
        {
            var type = await _context.Agreements
                .AsNoTracking()
                .Where(a => a.Id == agreementId && a.OrganizationId == organizationId)
                .Select(a => (AgreementType?)a.AgreementVersion.AgreementType)
                .SingleOrDefaultAsync();

            if (!type.HasValue)
            {
                return null;
            }

            return GetEncodedPdf(type.Value);
        }

        public async Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId)
        {
            return await _context.Agreements
                .Where(a => a.Id == agreementId)
                .Select(a => a.SignedAgreement)
                .SingleOrDefaultAsync();
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
                    agreement.AgreementContent = await _razorConverterService.RenderViewToStringAsync("/Views/TermsOfAccess.cshtml", agreement);
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

        private string GetEncodedPdf(AgreementType type)
        {
            string filename;
            switch (type)
            {
                case AgreementType.CommunityPracticeOrgAgreement:
                    filename = "CommunityPracticeOrganizationAgreement.pdf";
                    break;

                case AgreementType.CommunityPharmacyOrgAgreement:
                    filename = "CommunityPharmacyOrganizationAgreement.pdf";
                    break;

                default:
                    throw new ArgumentException($"Invalid AgreementType {type} in {nameof(GetEncodedPdf)}");
            }

            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(filename));

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (var reader = new MemoryStream())
            {
                stream.CopyTo(reader);
                return Convert.ToBase64String(reader.ToArray());
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

        private async Task<string> RenderOrgAgreementHtmlAsync(AgreementType type, string orgName, DateTimeOffset? acceptedDate, bool forPdf)
        {
            string viewName;
            switch (type)
            {
                case AgreementType.CommunityPracticeOrgAgreement:
                    viewName = forPdf
                        ? "/Views/CommunityPracticeOrganizationAgreementPdf.cshtml"
                        : "/Views/CommunityPracticeOrganizationAgreement.cshtml";
                    break;

                case AgreementType.CommunityPharmacyOrgAgreement:
                    viewName = forPdf
                        ? "/Views/CommunityPharmacyOrganizationAgreementPdf.cshtml"
                        : "/Views/CommunityPharmacyOrganizationAgreement.cshtml";
                    break;

                default:
                    throw new ArgumentException($"Invalid AgreementType {type} in {nameof(RenderOrgAgreementHtmlAsync)}");
            }

            DateTimeOffset displayDate = acceptedDate ?? DateTimeOffset.Now;
            // Converting to BC time here since we arn't localizing this time in the Front End.
            displayDate = displayDate.ToOffset(new TimeSpan(-7, 0, 0));

            return await _razorConverterService.RenderViewToStringAsync(viewName, new Tuple<string, DateTimeOffset>(orgName, displayDate));
        }
    }
}
