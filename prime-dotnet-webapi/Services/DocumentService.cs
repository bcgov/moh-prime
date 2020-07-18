using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.Services.Clients;

// TODO add logging
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

        public async Task<Stream> GetStreamForLatestBusinessLicenceDocument(int siteId)
        {
            var licence = await _siteService.GetLatestBusinessLicenceAsync(siteId);
            return await _documentManagerClient.GetFileAsync(licence.DocumentGuid);
        }
        public async Task<Stream> GetStreamForLatestSignedAgreementDocument(int organizationId)
        {
            var agreement = await _organizationService.GetLatestSignedAgreementAsync(organizationId);
            return await _documentManagerClient.GetFileAsync(agreement.DocumentGuid);
        }

        public async Task<string> GetDownloadTokenForSelfDeclarationDocument(int selfDeclarationDocumentId)
        {
            var selfDeclarationDocument = await _context.SelfDeclarationDocuments
                .Where(sa => sa.Id == selfDeclarationDocumentId).SingleAsync();
            return await _documentManagerClient.CreateDownloadTokenAsync(selfDeclarationDocument.DocumentGuid);
        }

    }
}
