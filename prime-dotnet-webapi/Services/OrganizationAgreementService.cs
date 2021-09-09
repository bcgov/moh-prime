using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.Agreements;
using Prime.Services.Razor;

namespace Prime.Services
{
    public class OrganizationAgreementService : BaseService, IOrganizationAgreementService
    {
        private readonly IMapper _mapper;
        private readonly IPdfService _pdfService;
        private readonly IRazorConverterService _razorConverterService;

        public OrganizationAgreementService(
            ApiDbContext context,
            ILogger<OrganizationAgreementService> logger,
            IMapper mapper,
            IPdfService pdfService,
            IRazorConverterService razorConverterService)
            : base(context, logger)
        {
            _mapper = mapper;
            _pdfService = pdfService;
            _razorConverterService = razorConverterService;
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

            var orgName = await GetOrganizationName(organizationId);
            var html = await RenderOrgAgreementHtmlAsync(agreementVm.AgreementType, orgName, agreementVm.AcceptedDate, asEncodedPdf, false);

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

        public async Task<string> RenderOrgAgreementHtmlAsync(AgreementType type, string orgName, DateTimeOffset? acceptedDate, bool forPdf, bool withSignature)
        {
            var template = (type, forPdf) switch
            {
                (AgreementType.CommunityPharmacyOrgAgreement, false) => RazorTemplates.OrgAgreements.CommunityPharmacy,
                (AgreementType.CommunityPharmacyOrgAgreement, true) => RazorTemplates.OrgAgreements.CommunityPharmacyPdf,
                (AgreementType.CommunityPracticeOrgAgreement, false) => RazorTemplates.OrgAgreements.CommunityPractice,
                (AgreementType.CommunityPracticeOrgAgreement, true) => RazorTemplates.OrgAgreements.CommunityPracticePdf,
                (AgreementType.DeviceProviderOrgAgreement, false) => RazorTemplates.OrgAgreements.DeviceProvider,
                (AgreementType.DeviceProviderOrgAgreement, true) => RazorTemplates.OrgAgreements.DeviceProviderPdf,
                _ => throw new ArgumentException($"Invalid AgreementType {type} in {nameof(RenderOrgAgreementHtmlAsync)}")
            };

            var displayDate = acceptedDate ?? DateTimeOffset.Now;
            // Converting to BC time here since we aren't localizing this time in the web client
            displayDate = displayDate.ToOffset(new TimeSpan(-7, 0, 0));

            return await _razorConverterService.RenderTemplateToStringAsync(template, new OrgAgreementRazorViewModel(orgName, displayDate, withSignature));
        }

        /// <summary>
        /// Returns a Base64 encoded PDF of a given org agreement for signing.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<string> GetSignableOrgAgreementAsync(int organizationId, AgreementType type)
        {
            var orgName = await GetOrganizationName(organizationId);
            var html = await RenderOrgAgreementHtmlAsync(type, orgName, DateTimeOffset.Now, true, true);
            var pdf = _pdfService.Generate(html);
            return Convert.ToBase64String(pdf);
        }

        private async Task<string> GetOrganizationName(int organizationId)
        {
            return await _context.Organizations
                .Where(o => o.Id == organizationId)
                .Select(o => o.Name)
                .SingleAsync();
        }
    }
}
