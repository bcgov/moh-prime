using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

// TODO add logging
namespace Prime.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly ISiteService _siteService;
        private readonly HttpClient _client;

        public DocumentService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ISiteService siteService)
            : base(context, httpContext)
        {
            _siteService = siteService;
            _client = new HttpClient();
        }

        public async Task<IEnumerable<Document>> GetBusinessLicenceDocumentsBySiteId(int siteId)
        {
            var result = new List<Document>();
            var businessLicences = await _siteService.GetBusinessLicencesAsync(siteId);

            foreach (var licence in businessLicences)
            {
                var document = await GetBusinessLicenceDocument(licence);
                result.Add(document);
            };

            return result;
        }

        public async Task<Document> GetLatestBusinessLicenceDocumentBySiteId(int siteId)
        {
            var licence = await _siteService.GetLatestBusinessLicenceAsync(siteId);
            return await GetBusinessLicenceDocument(licence);
        }

        private async Task<Document> GetBusinessLicenceDocument(BusinessLicence licence)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{PrimeConstants.DOCUMENT_MANAGER_URL}?token={licence.DocumentGuid}"),
                Method = HttpMethod.Get
            };
            var response = await _client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new Document(licence.FileName, response.Content.ReadAsByteArrayAsync().Result);
            }

            return null;
        }
    }
}
