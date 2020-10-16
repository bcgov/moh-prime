using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.HttpClients;

namespace Prime.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly ISiteService _siteService;
        private readonly IOrganizationService _organizationService;
        private readonly IDocumentManagerClient _documentManagerClient;

        public DocumentService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ISiteService siteService,
            IOrganizationService organizationService,
            IDocumentManagerClient documentManagerClient)
            : base(context, httpContext)
        {
            _siteService = siteService;
            _organizationService = organizationService;
            _documentManagerClient = documentManagerClient;
        }

        public async Task<string> GetDownloadTokenForLatestBusinessLicenceDocument(int siteId)
        {
            var licence = await _siteService.GetLatestBusinessLicenceAsync(siteId);
            return await _documentManagerClient.CreateDownloadTokenAsync(licence.DocumentGuid);
        }

        public async Task<string> GetDownloadTokenForLatestSignedAgreementDocument(int organizationId)
        {
            var agreement = await _organizationService.GetLatestSignedAgreementAsync(organizationId);
            return await _documentManagerClient.CreateDownloadTokenAsync(agreement.DocumentGuid);
        }

        public async Task<string> GetDownloadTokenForSelfDeclarationDocument(int selfDeclarationDocumentId)
        {
            var selfDeclarationDocument = await _context.SelfDeclarationDocuments
                .Where(sa => sa.Id == selfDeclarationDocumentId).SingleAsync();
            return await _documentManagerClient.CreateDownloadTokenAsync(selfDeclarationDocument.DocumentGuid);
        }

        public async Task<string> GetDownloadTokenForIdentificationDocument(int identificationDocumentId)
        {
            var identificationDocument = await _context.IdentificationDocuments
                .Where(sa => sa.Id == identificationDocumentId).SingleAsync();
            return await _documentManagerClient.CreateDownloadTokenAsync(identificationDocument.DocumentGuid);
        }

        public async Task<string> GetDownloadTokenForEnrolleeAdjudicationDocument(int enrolleeAdjudicationDocumentId)
        {
            var document = await _context.EnrolleeAdjudicationDocuments
                .Where(sa => sa.Id == enrolleeAdjudicationDocumentId).SingleAsync();
            return await _documentManagerClient.CreateDownloadTokenAsync(document.DocumentGuid);
        }

        public async Task<string> GetDownloadTokenForSiteAdjudicationDocument(int siteAdjudicationDocumentId)
        {
            var document = await _context.SiteAdjudicationDocuments
                .Where(sa => sa.Id == siteAdjudicationDocumentId).SingleAsync();
            return await _documentManagerClient.CreateDownloadTokenAsync(document.DocumentGuid);
        }

        public async Task<Stream> GetStreamForLatestBusinessLicenceDocument(int siteId)
        {
            var licence = await _siteService.GetLatestBusinessLicenceAsync(siteId);
            return await _documentManagerClient.GetFileStreamAsync(licence.DocumentGuid);
        }

        public async Task<Stream> GetStreamForLatestSignedAgreementDocument(int organizationId)
        {
            var agreement = await _organizationService.GetLatestSignedAgreementAsync(organizationId);
            return await _documentManagerClient.GetFileStreamAsync(agreement.DocumentGuid);
        }

        public async Task<string> FinalizeDocumentUpload(Guid documentGuid, string filePath)
        {
            return await _documentManagerClient.FinalizeUploadAsync(documentGuid, filePath);
        }
    }
}
