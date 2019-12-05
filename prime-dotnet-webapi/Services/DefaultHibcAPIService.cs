using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace Prime.Services
{
    public class DefaultHibcApiService : BaseService, IHibcApiService
    {
        public DefaultHibcApiService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<bool> ValidCollegeLicense(string licenceNumber, string collegeReferenceId)
        {
            throw new NotImplementedException();
        }

        private async Task<HibcApiResponse> GetCollegeLicenceHibc()
        {
            X509Certificate2 certificate = new X509Certificate2(@"/opt/app-root/etc/certs/hibc-api-cert.pfx", PrimeConstants.HIBC_SSL_CERT_PASSWORD);
            var httpClientHandler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ClientCertificates = { certificate }
            };
            using (var client = new HttpClient(httpClientHandler))
            {
                var values = new
                {
                    applicationUUID = "b7a2993a-e55a-4455-b8c2-fcd12e57ce66",
                    programArea = "PRIME",
                    licenceNumber = "2036P",
                    collegeReferenceId = "P1"
                };
                var resp = await client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, values);
                string content = await resp.Content.ReadAsStringAsync();
                return $"status code:[{(int)resp.StatusCode}], headers:[{resp.Content.Headers}], content:[{content}]";
            };
        }

        private class HibcApiRequest
        {
            Guid applicationUUID { get; set; }
            string programArea { get { return "PRIME"; } }
            string licenceNumber { get; set; }
            string collegeReferenceId { get; set; }
        }

        private class HibcApiResponse
        {
            Guid applicationUUID { get; set; }
            string firstName { get; set; }
            string middleInitial { get; set; }
            string lastName { get; set; }
            string dateofBirth { get; set; }
            string status { get; set; }
            string effectiveDate { get; set; }
        }
    }
}
