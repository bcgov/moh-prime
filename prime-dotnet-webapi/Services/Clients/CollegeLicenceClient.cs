using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

using Prime.Models;

namespace Prime.Services.Clients
{
    public class CollegeLicenceClient : ICollegeLicenceClient
    {
        private readonly HttpClient _client;

        public CollegeLicenceClient(HttpClient client)
        {
            var authBytes = ASCIIEncoding.ASCII.GetBytes($"{PrimeConstants.PHARMANET_API_USERNAME}:{PrimeConstants.PHARMANET_API_PASSWORD}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));

            _client = client;
        }

        public async Task<PharmanetCollegeRecord> GetPharmanetCollegeRecordAsync(Certification certification)
        {
            if (certification.College == null)
            {
                throw new ArgumentNullException(nameof(certification.College));
            }

            var requestParams = new CollegeRecordRequestParams(certification.LicenseNumber, certification.College.Prefix);
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestParams));

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(PrimeConstants.PHARMANET_API_URL, requestContent);
            }
            catch (Exception ex)
            {
                await LogError(requestParams, response, ex);
                throw new PharmanetCollegeApiException("Error occurred when calling Pharmanet API. Try again later.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(requestParams, response);
                throw new PharmanetCollegeApiException($"Error code {response.StatusCode} was returned when calling Pharmanet API.");
            }

            var content = await response.Content.ReadAsAsync<List<PharmanetCollegeRecord>>();
            var practicionerRecord = content.SingleOrDefault();

            // If we get a record back, it should have the same transaction UUID as our request.
            if (practicionerRecord != null && practicionerRecord.applicationUUID != requestParams.applicationUUID)
            {
                throw new PharmanetCollegeApiException($"Expected matching applicationUUIDs between request data and response data. Request was \"{requestParams.applicationUUID}\", response was \"{practicionerRecord.applicationUUID}\".");
            }

            return practicionerRecord;
        }

        // TODO use real logger
        private async Task LogError(CollegeRecordRequestParams requestParams, HttpResponseMessage response, Exception exception = null)
        {
            string secondaryMessage;
            if (exception != null)
            {
                secondaryMessage = $"exception:{exception.Message}";
            }
            else if (response != null)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                secondaryMessage = $"response code:{(int)response.StatusCode}, response body:{responseMessage}";
            }
            else
            {
                secondaryMessage = "no additional message. Http response and exception were null.";
            }

            Console.WriteLine($"{DateTime.Now} - Error validating college licence. UUID:{requestParams.applicationUUID}, with {secondaryMessage}.");
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
    }

    public class CollegeLicenceClientHandler : HttpClientHandler
    {
        public CollegeLicenceClientHandler() : base()
        {
            X509Certificate2 certificate = new X509Certificate2(PrimeConstants.PHARMANET_SSL_CERT_FILENAME, PrimeConstants.PHARMANET_SSL_CERT_PASSWORD);
            ClientCertificates.Add(certificate);
            ClientCertificateOptions = ClientCertificateOption.Manual;
        }
    }
}
