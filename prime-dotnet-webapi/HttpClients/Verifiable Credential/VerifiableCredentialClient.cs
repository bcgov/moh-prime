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
using System.Reflection;

namespace Prime.HttpClients
{
    public class VerifiableCredentialClient : IVerifiableCredentialClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public VerifiableCredentialClient(
            HttpClient client,
            ILogger<VerifiableCredentialClient> logger)
        {
            // Credentials and Base Url are set in Startup.cs
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger;
        }

        public async Task<ConnectionResponse> CreateInvitationAsync(string alias)
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

            var connectionResponse = JsonConvert.DeserializeObject<ConnectionResponse>(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("Create connection invitation response {object}", JsonConvert.SerializeObject(connectionResponse));

            return connectionResponse;
        }

        public async Task<CredentialResponse> IssueCredentialAsync(CredentialOfferRequest credentialOffer)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(credentialOffer), Encoding.UTF8, "application/json");

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

            return JsonConvert.DeserializeObject<CredentialResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> RevokeCredentialAsync(Credential credential)
        {
            _logger.LogInformation("Revoking credential cred_ex-Id={id}", credential.CredentialExchangeId);

            var revocationRequest = new RevokeCredentialRequest
            {
                CredentialExchangeId = credential.CredentialExchangeId
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(revocationRequest), Encoding.UTF8, "application/json");

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
                response = await _client.GetAsync($"schemas/created?schema_version={PrimeConfiguration.Current.VerifiableCredentialApi.SchemaVersion}&schema_issuer_did={did}&schema_name={PrimeConfiguration.Current.VerifiableCredentialApi.SchemaName}");
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

            var schemaIdResponse = JsonConvert.DeserializeObject<SchemaIdResponse>(await response.Content.ReadAsStringAsync());

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
            var schemaRequest = new SchemaRequest
            {
                SchemaName = PrimeConfiguration.Current.VerifiableCredentialApi.SchemaName,
                SchemaVersion = PrimeConfiguration.Current.VerifiableCredentialApi.SchemaVersion
            };

            var properties = JObject.FromObject(new CredentialPayload { });

            foreach (var property in properties.Properties())
            {
                schemaRequest.Attributes.Add(property.Name);
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

            var schemaResponse = JsonConvert.DeserializeObject<SchemaResponse>(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("Schema Created successfully {@JObject}", schemaResponse);

            return schemaResponse.SchemaId;
        }

        public async Task<string> GetIssuerDidAsync()
        {
            HttpResponseMessage response;
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

            var issuerDidResponse = JsonConvert.DeserializeObject<IssuerDidResponse>(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET Issuer DID response {did}", issuerDidResponse.Result.Did);

            return issuerDidResponse.Result.Did;
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

            var credentialResponse = JsonConvert.DeserializeObject<CredentialDefinitionIdResponse>(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("GET Credential Definition IDs {@JObject}", JsonConvert.SerializeObject(credentialResponse));
            if (credentialResponse.CredentialDefinitionIds != null && credentialResponse.CredentialDefinitionIds.Count > 0)
            {
                return credentialResponse.CredentialDefinitionIds.Last();
            }

            return null;
        }

        public async Task<string> CreateCredentialDefinitionAsync(string schemaId)
        {
            var credentialDefinitionRequest = new CredentialDefinitionRequest
            {
                SchemaId = schemaId,
                Tag = "Prime"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(credentialDefinitionRequest), Encoding.UTF8, "application/json");

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

            var credentialDefinitionResponse = JsonConvert.DeserializeObject<CredentialDefinitionResponse>(await response.Content.ReadAsStringAsync());

            _logger.LogInformation("Credential Definition Created successfully {@JObject}", JsonConvert.SerializeObject(credentialDefinitionResponse));

            return credentialDefinitionResponse.CredentialDefinitionId;
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
