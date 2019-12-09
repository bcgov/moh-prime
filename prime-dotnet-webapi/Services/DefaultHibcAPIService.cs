using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            Console.WriteLine(">>>>>>>>>-------------------in method----------------");
            using (var client = new HttpClient(CreateClientHandler()))
            {



                var response = await client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, new CollegeLicenceRequestParams(licenceNumber, collegeReferenceId));
                System.Console.WriteLine($"---status code:[{(int)response.StatusCode}]");

                var srt = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine($"---content:[{srt}]");

                // List<CollegeLicenceResponse> data = JsonConvert.DeserializeObject<List<CollegeLicenceResponse>>(srt);
                // System.Console.WriteLine($"-----data:[{data[0].ToString()}]");

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
            public string applicationUUID { get; set; }
            public string programArea { get; set; }
            public string licenceNumber { get; set; }
            public string collegeReferenceId { get; set; }

            public CollegeLicenceRequestParams(string licenceNumber, string collegeReferenceId)
            {
                applicationUUID = Guid.NewGuid().ToString();
                programArea = "PRIME";
                this.licenceNumber = licenceNumber;
                this.collegeReferenceId = collegeReferenceId;
            }
        }

        private class CollegeLicenceResponse
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
