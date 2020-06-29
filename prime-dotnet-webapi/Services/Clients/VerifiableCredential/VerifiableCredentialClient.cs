using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prime.Services.Clients
{
    public class VerifiableCredentialClient : IVerifiableCredentialClient
    {
        private readonly HttpClient _client;

        public VerifiableCredentialClient(
            HttpClient client)
        {
            // Auth header and api-key are injected in Startup.cs
            _client = client;
        }

        public async Task<JObject> CreateInvitationAsync(string alias)
        {
            // _logger.Information("Create connection invitation");
            System.Console.WriteLine("Create connection invitation");

            var values = new List<KeyValuePair<string, string>>();
            var httpContent = new FormUrlEncodedContent(values);

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"connections/create-invitation?alias={alias}", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException("Error occurred when calling Verfiable Credential API. Try again later.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(httpContent, response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::CreateInvitationAsync");
            }

            // _logger.Information("Create connection invitation response @JObject", response);
            System.Console.WriteLine("Create connection invitation response");
            System.Console.WriteLine(JsonConvert.SerializeObject(response));

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> IssueCredentialAsync(JObject credentialOffer)
        {
            var httpContent = new StringContent(credentialOffer.ToString());
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync("issue-credential/send", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to issue a credential: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(httpContent, response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::IssueCredentialAsync");
            }

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> GetIssuerDidAsync()
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync("wallet/did/public");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to get the issuer DID: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetIssuerDidAsync");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            System.Console.WriteLine("GET Issuer DID");
            System.Console.WriteLine(JsonConvert.SerializeObject(body));

            return body.Value<string>("did");
        }

        public async Task<string> GetCredentialDefinitionIdAsync(string schemaId)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"credential-definitions/created?schema_id={schemaId}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to GET credential definition: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetCredentialDefinitionAsync");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            System.Console.WriteLine("GET Credential Definition IDs");
            System.Console.WriteLine(JsonConvert.SerializeObject(body));

            return (string)body["credential_definition_ids"][0];
        }

        private async Task LogError(HttpResponseMessage response, Exception exception = null)
        {
            await LogError(null, response, exception);
        }

        private async Task LogError(HttpContent content, HttpResponseMessage response, Exception exception = null)
        {
            string secondaryMessage;
            if (exception != null)
            {
                secondaryMessage = $"Exception: {exception.Message}";
            }
            else if (response != null)
            {
                string responseMessage = await response.Content.ReadAsStringAsync();
                secondaryMessage = $"Response code: {(int)response.StatusCode}, response body:{responseMessage}";
            }
            else
            {
                secondaryMessage = "No additional message. Http response and exception were null.";
            }

            // _logger.Error(exception, secondaryMessage, new Object[] { content, response });
            System.Console.WriteLine("ERROR_RESPONSE_AND_CONTENT ------------------------------------------------");
            System.Console.WriteLine(JsonConvert.SerializeObject(secondaryMessage));
            System.Console.WriteLine("---------------------------------------------------------------------------");
            System.Console.WriteLine(JsonConvert.SerializeObject(content));
            System.Console.WriteLine("---------------------------------------------------------------------------");
            System.Console.WriteLine(JsonConvert.SerializeObject(response));
            System.Console.WriteLine("END_ERROR_RESPONSE_AND_CONTENT --------------------------------------------");
        }
    }

    public class VerifiableCredentialApiException : Exception
    {
        public VerifiableCredentialApiException() : base() { }
        public VerifiableCredentialApiException(string message) : base(message) { }
        public VerifiableCredentialApiException(string message, Exception inner) : base(message, inner) { }
    }
}

