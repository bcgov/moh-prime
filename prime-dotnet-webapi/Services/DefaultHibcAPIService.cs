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

        public async Task<string> ValidCollegeLicense(string licenceNumber, string collegeReferenceId)
        {
            var par = await CallPharmanetCollegeLicenceService(licenceNumber, collegeReferenceId);
            return par;
        }

        private async Task<string> CallPharmanetCollegeLicenceService(string licenceNumber, string collegeReferenceId)
        {
            Console.WriteLine("-------------------in method----------------");
            Console.WriteLine($"licence:[{licenceNumber}], collref:[{collegeReferenceId}]");
            using (var client = new HttpClient(CreateClientHandler()))
            {
                var requestParams = new CollegeLicenceRequestParams(licenceNumber, collegeReferenceId);
                System.Console.WriteLine($"---params:[{requestParams.ToString()}]");
                var response = await client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, requestParams);
                System.Console.WriteLine($"---status code[{(int)response.StatusCode}]");
                var srt = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine($"---content:[{srt}]");
                return srt;
                // return await response.Content.ReadAsAsync<CollegeLicenceResponseParams>();
            };
        }

        private HttpClientHandler CreateClientHandler()
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                return new HttpClientHandler();
            }

            X509Certificate2 certificate = new X509Certificate2(PrimeConstants.HIBC_SSL_CERT_FILENAME, PrimeConstants.HIBC_SSL_CERT_PASSWORD);
            return new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ClientCertificates = { certificate }
            };
        }

        private class CollegeLicenceRequestParams
        {
            Guid applicationUUID { get; }
            string programArea { get; }
            string licenceNumber { get; }
            string collegeReferenceId { get; }

            public CollegeLicenceRequestParams(string licenceNumber, string collegeReferenceId)
            {
                applicationUUID = Guid.NewGuid();
                programArea = "PRIME";
                this.licenceNumber = licenceNumber;
                this.collegeReferenceId = collegeReferenceId;
            }
        }

        private class CollegeLicenceResponseParams
        {
            string applicationUUID { get; set; }
            string firstName { get; set; }
            string middleInitial { get; set; }
            string lastName { get; set; }
            string dateofBirth { get; set; }
            string status { get; set; }
            string effectiveDate { get; set; }
        }
    }
}
