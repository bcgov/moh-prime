using System;
using System.Linq;
using System.Net;
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
        private static HttpClient Client = InitHttpClient();

        public DefaultHibcApiService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<string> ValidateCollegeLicense(string licenceNumber, string collegeReferenceId)
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                // TODO handle local dev
                throw new NotImplementedException();
            }

            var par = await CallPharmanetCollegeLicenceService(licenceNumber, collegeReferenceId);


            return JsonConvert.SerializeObject(par);
        }

        private async Task<CollegePracticionerRecord> CallPharmanetCollegeLicenceService(string licenceNumber, string collegeReferenceId)
        {
            var requestParams = new CollegeLicenceRequestParams(licenceNumber, collegeReferenceId);
            var response = await Client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, requestParams);
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
        }

        private static HttpClient InitHttpClient()
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                return null;
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

            var sp = ServicePointManager.FindServicePoint(new Uri(PrimeConstants.HIBC_API_URL));
            sp.ConnectionLeaseTimeout = 60 * 1000; // 1 minute

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
