using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Prime.Services.Clients
{
    public class VerifiableCredentialClient : IVerifiableCredentialClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        private static readonly string SchemaName = "enrollee";
        // TODO update version to 2.0 in test agent (and update cred_def_id) so versions are the same between dev and test
        private static readonly string SchemaVersion = "2.0";

        public VerifiableCredentialClient(
            HttpClient client,
            ILogger<VerifiableCredentialClient> logger)
        {
            // Auth header and api-key are injected in Startup.cs
            _client = client;
            _logger = logger;
        }

        public async Task<JObject> CreateInvitationAsync(string alias)
        {
            _logger.LogInformation("Create connection invitation");

            var values = new List<KeyValuePair<string, string>>();
            var httpContent = new FormUrlEncodedContent(values);

            HttpResponseMessage response = null;
            try
            {
                _logger.LogInformation($"Full Path: {_client.BaseAddress}connections/create-invitation?alias={alias}");
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

            _logger.LogInformation("Create connection invitation response {@JObject}", JsonConvert.SerializeObject(response));

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> IssueCredentialAsync(JObject credentialOffer)
        {
            var httpContent = new StringContent(credentialOffer.ToString(), Encoding.UTF8, "application/json");
            // httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var byteArray = await httpContent.ReadAsByteArrayAsync();
            httpContent.Headers.ContentLength = byteArray.Length;
            _logger.LogInformation("Credential offer in client {@JObject}", JsonConvert.SerializeObject(credentialOffer));
            _logger.LogInformation("Default Headers {headers}", _client.DefaultRequestHeaders.ToString());


            HttpResponseMessage response = null;
            try
            {
                _logger.LogInformation($"Full Path: {_client.BaseAddress}issue-credential/send");
                response = await _client.PostAsync("issue-credential/send", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to issue a credential: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::IssueCredentialAsync");
            }

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }


        public async Task<string> GetSchemaId(string did)
        {
            HttpResponseMessage response = null;
            try
            {
                _logger.LogInformation($"Full Path: {_client.BaseAddress}schemas/created?schema_version={SchemaVersion}&schema_issuer_did={did}&schema_name={SchemaName}");
                response = await _client.GetAsync($"schemas/created?schema_version={SchemaVersion}&schema_issuer_did={did}&schema_name={SchemaName}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to get the schema id by issuer did: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetSchema");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET Schema id by issuer id response {@JObject}", JsonConvert.SerializeObject(body));
            _logger.LogInformation("SCHEMA_ID: {schemaid}", (string)body.SelectToken("schema_ids[0]"));

            return (string)body.SelectToken("schema_ids[0]");
        }

        public async Task<JObject> GetSchema(string schemaId)
        {
            HttpResponseMessage response = null;
            try
            {
                _logger.LogInformation($"Full Path: {_client.BaseAddress}schemas/{schemaId}");
                response = await _client.GetAsync($"schemas/{schemaId}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to get the schema: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetSchema");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET Schema response {@JObject}", JsonConvert.SerializeObject(body));

            return body;
        }

        public async Task<string> GetIssuerDidAsync()
        {
            HttpResponseMessage response = null;
            try
            {
                _logger.LogInformation($"Full Path: {_client.BaseAddress}wallet/did/public");
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

            _logger.LogInformation("GET Issuer DID response {@JObject}", JsonConvert.SerializeObject(body));

            return (string)body.SelectToken("result.did");
        }

        public async Task<string> GetCredentialDefinitionIdAsync(string schemaId)
        {
            HttpResponseMessage response = null;
            try
            {
                _logger.LogInformation($"Full Path: {_client.BaseAddress}credential-definitions/created?schema_id={schemaId}");
                response = await _client.GetAsync($"credential-definitions/created?schema_id={schemaId}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to get credential definition: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetCredentialDefinitionAsync");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET Credential Definition IDs {@JObject}", JsonConvert.SerializeObject(body));

            return (string)body.SelectToken("credential_definition_ids[0]");
        }

        public async Task<JObject> GetPresentationProof(string presentationExchangeId)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"presentation-proof/records/{presentationExchangeId}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to get presentation proof: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetPresentationProof");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET Presentation proof @JObject", JsonConvert.SerializeObject(body));

            return body;
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

            _logger.LogError(exception, secondaryMessage, new Object[] { content, response });
        }
    }

    public class VerifiableCredentialApiException : Exception
    {
        public VerifiableCredentialApiException() : base() { }
        public VerifiableCredentialApiException(string message) : base(message) { }
        public VerifiableCredentialApiException(string message, Exception inner) : base(message, inner) { }
    }
}

