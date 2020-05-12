using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Prime.Services
{
    public class OrganizationInfo
    {

    }
    
    public class OrgBookApiService : BaseService, IOrgBookApiService
    {
        private readonly string ORGBOOK_API_URL = "https://www.orgbook.gov.bc.ca/api"; // TODO add to constants
        private HttpClient Client;
        private ILogger _logger;

        public OrgBookApiService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IHttpClientFactory clientFactory,
            ILogger logger)
            : base(context, httpContext)
        {
            Client = this.OrgBookHttpClient(clientFactory);
            _logger = logger;
        }

        public async Task GetOrganizationInfo(string businessNumber)
        {
            // TODO implement request to OrgBook using business number
        }

        private HttpClient OrgBookHttpClient(IHttpClientFactory clientFactory)
        {
            return clientFactory.CreateClient();
        }
    }
}
