using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<string> ValidateCollegeLicense(string licenceNumber, string collegeReferenceId)
        {
            var par = await CallPharmanetCollegeLicenceService(licenceNumber, collegeReferenceId);


            return JsonConvert.SerializeObject(par);
        }

        private async Task<CollegePracticionerRecord> CallPharmanetCollegeLicenceService(string licenceNumber, string collegeReferenceId)
        {
            using (var client = CreateHttpClient())
            {
                var requestParams = new CollegeLicenceRequestParams(licenceNumber, collegeReferenceId);
                var response = await client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, requestParams);
                if (!response.IsSuccessStatusCode)
                {
                    // TODO Try again, log error? Probably dont handle like this.
                    throw new PharmanetCollegeApiException($"API returned status code {(int)response.StatusCode}, with reason \"{response.ReasonPhrase}\".");
                }

                var practicionerRecord = await CollegePracticionerRecord.FromResponseContentAsync(response.Content);
                if (practicionerRecord.applicationUUID != requestParams.applicationUUID)
                {
                    throw new PharmanetCollegeApiException($"Expected matching applicationUUIDs between request and response. Request was\"{requestParams.applicationUUID}\", response was \"{practicionerRecord.applicationUUID}\".");
                }

                return practicionerRecord;
            };
        }

        private HttpClient CreateHttpClient()
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                return new HttpClient();
            }

            X509Certificate2 certificate = new X509Certificate2(PrimeConstants.HIBC_SSL_CERT_FILENAME, PrimeConstants.HIBC_SSL_CERT_PASSWORD);
            var client = new HttpClient(
                new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ClientCertificates = { certificate }
                }
            );

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        $"{PrimeConstants.HIBC_API_USERNAME}:{PrimeConstants.HIBC_API_PASSWORD}"
                    )
                )
            );

            return client;
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

        private class CollegePracticionerRecord
        {
            public string applicationUUID { get; set; }
            public string firstName { get; set; }
            public string middleInitial { get; set; }
            public string lastName { get; set; }
            public DateTime dateofBirth { get; set; }
            public string status { get; set; }
            public DateTime effectiveDate { get; set; }

            public static async Task<CollegePracticionerRecord> FromResponseContentAsync(HttpContent content)
            {
                var stringContent = await content.ReadAsStringAsync();
                List<CollegePracticionerRecord> data = JsonConvert.DeserializeObject<List<CollegePracticionerRecord>>(stringContent);

                return data.SingleOrDefault();
            }
        }

        public class PharmanetCollegeApiException : Exception
        {
            public PharmanetCollegeApiException(string message)
                : base(message)
            {
            }

            public PharmanetCollegeApiException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    }
}
