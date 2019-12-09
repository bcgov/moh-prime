using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
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
                var response = await client.PostAsync(PrimeConstants.HIBC_API_URL, CreateCollegeLicenceRequestContent(licenceNumber, collegeReferenceId));
                System.Console.WriteLine($"---status code[{(int)response.StatusCode}]");
                System.Console.WriteLine($"---status code[{JsonConvert.SerializeObject(response)}]");

                var srt = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine($"---content:[{srt}]");

                // CollegeLicenceResponse data = JsonConvert.DeserializeObject<CollegeLicenceResponse>(srt);
                // System.Console.WriteLine($"-----data:[{data.ToString()}]");

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

        private StringContent CreateCollegeLicenceRequestContent(string licenceNumber, string collegeReferenceId)
        {
            var parameters = new CollegeLicenceRequestParams(licenceNumber, collegeReferenceId);

            return new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
        }

        private class CollegeLicenceRequestParams
        {
            Guid applicationUUID { get; set; }
            string programArea { get; set; }
            string licenceNumber { get; set; }
            string collegeReferenceId { get; set; }

            public CollegeLicenceRequestParams(string licenceNumber, string collegeReferenceId)
            {
                applicationUUID = Guid.NewGuid();
                programArea = "PRIME";
                this.licenceNumber = licenceNumber;
                this.collegeReferenceId = collegeReferenceId;
            }
        }

        private class CollegeLicenceResponse
        {
            Guid applicationUUID { get; set; }
            string firstName { get; set; }
            string middleInitial { get; set; }
            string lastName { get; set; }
            DateTime dateofBirth { get; set; }
            string status { get; set; }
            DateTime effectiveDate { get; set; }
        }
    }
}
