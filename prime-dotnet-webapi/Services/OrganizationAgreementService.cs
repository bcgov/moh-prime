using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Services.Razor;
using Prime.ViewModels;
using Prime.ViewModels.Agreements;

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
            var template = forPdf ? RazorTemplates.OrgAgreements.Pdf : RazorTemplates.OrgAgreements.Base;

            var displayDate = acceptedDate ?? DateTimeOffset.Now;
            // Converting to BC time here since we aren't localizing this time in the web client
            displayDate = displayDate.ToOffset(new TimeSpan(-7, 0, 0));

            // Pull content from database
            var agreementVersion = await GetAgreementVersionByType(type);
            var stringAry = agreementVersion.Text.Split("{{signature_block}}");

            var agreementContent = stringAry[0].Replace("{{organization_name}}", orgName);
            var scheduleContent = stringAry[1];

            var viewModel = new OrgAgreementRazorViewModel(displayDate, withSignature, agreementContent, scheduleContent);

            return await _razorConverterService.RenderTemplateToStringAsync(template, viewModel);
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

        private async Task<AgreementVersion> GetAgreementVersionByType(AgreementType type)
        {
            var agreementVersion = await _context.AgreementVersions
                .AsNoTracking()
                .Where(av => av.AgreementType == type)
                .OrderByDescending(av => av.EffectiveDate)
                .FirstOrDefaultAsync();

            return agreementVersion;
        }
    }
}
