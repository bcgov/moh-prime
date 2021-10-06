using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace Prime.HttpClients
{
    public class CollegeLicenceClient : BaseClient, ICollegeLicenceClient
    {
        private readonly HttpClient _client;

        public CollegeLicenceClient(HttpClient client)
            : base(PropertySerialization.CamelCase)
        {
            // Auth header and cert are injected in Startup.cs
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<PharmanetCollegeRecord> GetCollegeRecordAsync(string licencePrefix, string licenceNumber)
        {
            licencePrefix.ThrowIfNull(nameof(licencePrefix));

            if (string.IsNullOrWhiteSpace(licenceNumber))
            {
                return null;
            }

            var requestParams = new CollegeRecordRequestParams(licencePrefix, licenceNumber);

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync(PrimeConfiguration.Current.PharmanetApi.Url, CreateStringContent(requestParams));
            }
            catch (Exception ex)
            {
                await LogError(requestParams.ApplicationUUID, response, ex);
                throw new PharmanetCollegeApiException("Error occurred when calling Pharmanet API. Try again later.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(requestParams.ApplicationUUID, response);
                throw new PharmanetCollegeApiException($"Error code {response.StatusCode} was returned when calling Pharmanet API.");
            }

            var content = await response.Content.ReadAsAsync<List<PharmanetCollegeRecord>>();
            var practicionerRecord = content.SingleOrDefault();

            // If we get a record back, it should have the same transaction UUID as our request.
            if (practicionerRecord != null && practicionerRecord.ApplicationUUID != requestParams.ApplicationUUID)
            {
                var ex = new PharmanetCollegeApiException($"Expected matching applicationUUIDs between request data and response data. Request was \"{requestParams.ApplicationUUID}\", response was \"{practicionerRecord.ApplicationUUID}\".");
                await LogError(requestParams.ApplicationUUID, response, ex);
                throw ex;
            }

            return practicionerRecord;
        }

        // TODO use real logger
        private async Task LogError(string requestUUID, HttpResponseMessage response, Exception exception = null)
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

            Console.WriteLine($"{DateTime.Now} - Error validating college licence. UUID:{requestUUID}, with {secondaryMessage}.");
        }
    }

    public class CollegeLicenceClientHandler : HttpClientHandler
    {
        public CollegeLicenceClientHandler()
        {
            ClientCertificates.Add(new X509Certificate2(PrimeConfiguration.Current.PharmanetApi.SslCertFilename, PrimeConfiguration.Current.PharmanetApi.SslCertPassword));
            ClientCertificateOptions = ClientCertificateOption.Manual;
        }
    }
}
