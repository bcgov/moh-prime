using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Prime.Models.VerifiableCredentials;
using System.Linq;

namespace Prime.HttpClients
{
    public class VerifiableCredentialClient : IVerifiableCredentialClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        // If schema changes, the following must be updated in all agents for each environment as the code changes are pushed so versions are the same
        // and have verifier app updated by aries team in each environment (send them schema id, if claims change send them new attributes)
        // Update the following through postman:
        // 1. Add new schema, incrementing schema version -> schema_name = enrollee
        // 2. Create a credential definition for schema -> support_revocation = true, tag = prime

        public VerifiableCredentialClient(
            HttpClient client,
            ILogger<VerifiableCredentialClient> logger)
        {
            // Credentials and Base Url are set in Startup.cs
            _client = client ?? throw new ArgumentNullException(nameof(client));
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

        public async Task<bool> RevokeCredentialAsync(Credential credential)
        {
            _logger.LogInformation("Revoking credential cred_ex-Id={id}", credential.CredentialExchangeId);

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

            _logger.LogInformation("Revoke credential cred_ex_id={id} success", credential.CredentialExchangeId);

            return true;
        }

        public async Task<string> GetSchemaId(string did)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"schemas/created?schema_version={PrimeEnvironment.VerifiableCredentialApi.SchemaVersion}&schema_issuer_did={did}&schema_name={PrimeEnvironment.VerifiableCredentialApi.SchemaName}");
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

            SchemaIdResponse schemaIdResponse = JsonConvert.DeserializeObject<SchemaIdResponse>(await response.Content.ReadAsStringAsync());

            if (schemaIdResponse.SchemaIds != null && schemaIdResponse.SchemaIds.Count > 0)
            {
                _logger.LogInformation("SCHEMA_ID: {schemaid}", (string)schemaIdResponse.SchemaIds.First());
                return schemaIdResponse.SchemaIds.First();
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CreateSchemaAsync()
        {
            // TODO create credential schema model with schema attriubtes(name, value) named
            var attributes = new List<string>
            {
                "GPID",
                "Renewal Date",
                "TOA Name",
                "Care Type Setting",
                "Remote User"
            };

            var schemaRequest = new SchemaRequest
            {
                SchemaName = PrimeEnvironment.VerifiableCredentialApi.SchemaName,
                SchemaVersion = PrimeEnvironment.VerifiableCredentialApi.SchemaVersion
            };

            foreach (var attribute in attributes)
            {
                schemaRequest.Attributes.Add(attribute);
            }

            var httpContent = new StringContent(JsonConvert.SerializeObject(schemaRequest), Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"schemas", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to create a schema: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::CreateSchemaAsync");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("Schema Created successfully {@JObject}", JsonConvert.SerializeObject(body));

            return (string)body.SelectToken("schema_id");
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
                throw new VerifiableCredentialApiException("Error occurred attempting to get the issuer DID: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
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
            if (credentialDefinitionIds != null && credentialDefinitionIds.Count > 0)
            {
                return (string)body.SelectToken($"credential_definition_ids[{credentialDefinitionIds.Count - 1}]");
            }

            return null;
        }

        public async Task<string> CreateCredentialDefinitionAsync(string schemaId)
        {
            var credentialDefinition = new JObject
            {
                { "schema_id", schemaId },
                { "support_revocation", true },
                { "tag", "prime" }
            };

            var httpContent = new StringContent(credentialDefinition.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"credential-definitions", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to create a credential-definition: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::CreateCredentialDefinitionAsync");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("Credential Definition Created successfully {@JObject}", JsonConvert.SerializeObject(body));

            return (string)body.SelectToken("credential_definition_id");
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

        public async Task<bool> DeleteCredentialAsync(Credential credential)
        {
            _logger.LogInformation("Deleting credential cred_ex-Id={id}", credential.CredentialExchangeId);

            HttpResponseMessage response = null;
            try
            {
                response = await _client.DeleteAsync($"issue-credential/records/{credential.CredentialExchangeId}");
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to delete a credential: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                return false;
            }

            _logger.LogInformation("Deleting credential cred_ex_id={id} success", credential.CredentialExchangeId);

            return true;
        }

        public async Task<bool> SendMessageAsync(string connectionId, string content)
        {
            _logger.LogInformation("Sending a message to connection_id={id}", connectionId);

            JObject messageObject = new JObject
            {
                { "content", content }
            };

            var httpContent = new StringContent(messageObject.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"connections/{connectionId}/send-message", httpContent);
            }
            catch (Exception ex)
            {
                await LogError(response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to send a message to the connection: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                await LogError(response);
                return false;
            }

            return true;
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
