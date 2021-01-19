using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Prime.Models;

namespace Prime.HttpClients
{
    public class VerifiableCredentialClient : IVerifiableCredentialClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        private static readonly string SchemaName = "enrollee";
        private static readonly string SchemaVersion = "2.1";
        // If schema changes, the following must be updated in all agents for each environment as the code changes are pushed so versions are the same
        // and have verifier app updated by aries team in each environment
        // Update the following through postman:
        // 1. Add new schema, incrementing schema version -> schema_name = enrollee
        // 2. Create a credential definition for schema -> support_revocation = true, tag = prime, revocation_registry_size = 100
        // 3. Create a new revocation registry using the credential_definition_id
        // 4. Manually set revocation registry state to active

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

            HttpResponseMessage response = null;
            try
            {
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

        public async Task<JObject> RevokeCredentialAsync(Credential credential)
        {
            JObject revocationObject = new JObject
            {
                { "cred_ex_id", credential.CredentialExchangeId },
                { "publish", true }
            };

            var httpContent = new StringContent(revocationObject.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"revocation/revoke", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to revoke a credential: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::RevokeCredentialAsync");
            }

            _logger.LogInformation("Revoke credential id={id} success", credential.Id);

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }


        public async Task<string> GetSchemaId(string did)
        {
            HttpResponseMessage response = null;
            try
            {
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

            _logger.LogInformation("SCHEMA_ID: {schemaid}", (string)body.SelectToken("schema_ids[0]"));

            return (string)body.SelectToken("schema_ids[0]");
        }

        public async Task<JObject> GetSchema(string schemaId)
        {
            HttpResponseMessage response = null;
            try
            {
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

            _logger.LogInformation("GET Issuer DID response {did}", (string)body.SelectToken("result.did"));

            return (string)body.SelectToken("result.did");
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
                throw new VerifiableCredentialApiException("Error occurred attempting to get credential definition: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetCredentialDefinitionAsync");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());
            JArray credentialDefinitionIds = (JArray)body.SelectToken("credential_definition_ids");

            _logger.LogInformation("GET Credential Definition IDs {@JObject}", JsonConvert.SerializeObject(body));

            return (string)body.SelectToken($"credential_definition_ids[{credentialDefinitionIds.Count - 1}]");
        }

        public async Task<string> GetRevocationRegistryIdAsync(string credentialDefinitionId)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"revocation/active-registry/{credentialDefinitionId}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to get the current active revocation registry by credential definition id: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetRevocationRegistryIdAsync");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET GetRevocationRegistryId response {revoc_reg_id}", (string)body.SelectToken("result.revoc_reg_id"));

            return (string)body.SelectToken("result.revoc_reg_id");
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

