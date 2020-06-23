using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

using Prime.Models;
using Newtonsoft.Json.Linq;

namespace Prime.Services.Clients
{
    public class VerifiableCredentialClient : IVerifiableCredentialClient
    {
        private readonly HttpClient _client;

        public VerifiableCredentialClient(HttpClient client)
        {
            // Auth header and api-key are injected in Startup.cs
            _client = client;
        }

        public async Task<JObject> CreateInvitation()
        {
            var values = new List<KeyValuePair<string, string>>();
            // values.Add(new KeyValuePair<string, string>("x-api-key", PrimeConstants.VERIFIABLE_CREDENTIAL_API_KEY));
            var httpContent = new FormUrlEncodedContent(values);

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync("connections/create-invitation", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException("Error occurred when calling Verfiable Credential API. Try again later.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(httpContent, response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was returned when calling Verifiable Credential API.");
            }

            JObject joResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
            return joResponse;
        }

        // TODO use real logger
        private async Task LogError(HttpContent content, HttpResponseMessage response, Exception exception = null)
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

            Console.WriteLine($"{DateTime.Now} - Error sending invitation.");
        }

        public class VerifiableCredentialApiException : Exception
        {
            public VerifiableCredentialApiException() : base() { }
            public VerifiableCredentialApiException(string message) : base(message) { }
            public VerifiableCredentialApiException(string message, Exception inner) : base(message, inner) { }
        }
    }
}
