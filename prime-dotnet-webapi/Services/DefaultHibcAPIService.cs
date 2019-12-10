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

using Prime.Models;

namespace Prime.Services
{
    public class DefaultHibcApiService : BaseService, IHibcApiService
    {
        private static HttpClient Client = InitHttpClient();

        public DefaultHibcApiService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<PharmanetCollegeRecord> GetCollegeRecord(string licenceNumber, string collegeReferenceId)
        {
            if (PrimeConstants.ENVIRONMENT_NAME == "local")
            {
                // TODO handle local dev
                throw new NotImplementedException();
            }

            return await CallPharmanetCollegeLicenceService(licenceNumber, collegeReferenceId);
        }

        private async Task<PharmanetCollegeRecord> CallPharmanetCollegeLicenceService(string licenceNumber, string collegeReferenceId)
        {
            var requestParams = new CollegeRecordRequestParams(licenceNumber, collegeReferenceId);
            var response = await Client.PostAsJsonAsync(PrimeConstants.HIBC_API_URL, requestParams);
            if (!response.IsSuccessStatusCode)
            {
                // TODO Try again, log error? Probably dont handle like this.
                throw new PharmanetCollegeApiException($"API returned status code {(int)response.StatusCode}, with reason \"{response.ReasonPhrase}\".");
            }

            var practicionerRecord = await CollegeRecordFromResponseAsync(response);
            if (practicionerRecord != null && practicionerRecord.applicationUUID != requestParams.applicationUUID)
            {
                throw new PharmanetCollegeApiException($"Expected matching applicationUUIDs between request data and response data. Request was\"{requestParams.applicationUUID}\", response was \"{practicionerRecord.applicationUUID}\".");
            }

            return practicionerRecord;
        }

        private async Task<PharmanetCollegeRecord> CollegeRecordFromResponseAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsAsync<List<PharmanetCollegeRecord>>();
            return content.SingleOrDefault();
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
                        $"{PrimeConstants.HIBC_API_USERNAME}:{PrimeConstants.HIBC_API_PASSWORD}r"
                    )
                )
            );

            return client;
        }

        private class CollegeRecordRequestParams
        {
            public string applicationUUID { get; set; }
            public string programArea { get; set; }
            public string licenceNumber { get; set; }
            public string collegeReferenceId { get; set; }

            public CollegeRecordRequestParams(string licenceNumber, string collegeReferenceId)
            {
                applicationUUID = Guid.NewGuid().ToString();
                programArea = "PRIME";
                this.licenceNumber = licenceNumber;
                this.collegeReferenceId = collegeReferenceId;
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
