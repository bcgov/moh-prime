using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prime.Models;

namespace Prime.Services.Clients
{
    public class VerifiableCredentialClient : IVerifiableCredentialClient
    {
        private readonly HttpClient _client;

        // private readonly ILogger _logger;

        public VerifiableCredentialClient(
            HttpClient client)
        {
            // Auth header and api-key are injected in Startup.cs
            _client = client;
        }

        public async Task<JObject> CreateInvitation()
        {
            var values = new List<KeyValuePair<string, string>>();
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

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> ReceiveInvitation(String invitation)
        {
            var httpContent = new StringContent(invitation);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync("connections/receive-invitation", httpContent);
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

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> AcceptInvitation(String connection_id)
        {
            var httpContent = new StringContent("");
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"connections/{connection_id}/accept-invitation", httpContent);
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

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> AcceptRequest(String connection_id)
        {
            var httpContent = new StringContent("");
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"connections/{connection_id}/accept-request", httpContent);
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

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> SendCredential(string requestContent)
        {
            var credParams = new SendCredentialParams("assign gpid");
            var httpContent = new StringContent(credParams.ToString());
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync("issue-credential/send", httpContent);
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

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> IssueCredential(string credential_exchange_id, JArray attributes)
        {
            var issueParams = new JObject();
            var credential_preview = new JObject();
            credential_preview.Add("@type", "did:sov:BzCbsNYhMrjHiqZDTUASHg;spec/issue-credential/1.0/credential-preview");
            credential_preview.Add("attributes", attributes);
            issueParams.Add("credential_preview", credential_preview);
            issueParams.Add("comment", "issue_credential");

            var httpContent = new StringContent(issueParams.ToString());
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"issue-credential/records/{credential_exchange_id}/issue", httpContent);
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

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> StoreCredential(string credential_exchange_id)
        {
            var httpContent = new StringContent("");
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await _client.PostAsync($"issue-credential/records/{credential_exchange_id}/store", httpContent);
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

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

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

            // _logger.Error(exception, secondaryMessage, new Object[] { response, content });
            System.Console.WriteLine("RESPONSE");
            System.Console.WriteLine(JsonConvert.SerializeObject(response));
            System.Console.WriteLine("CONTENT");
            System.Console.WriteLine(JsonConvert.SerializeObject(content));
        }

        private class SendCredentialParams
        {
            public string issuer_did;
            public string schema_id;
            public string cred_def_id;
            public string schema_version;
            public bool auto_remove;
            public string revoc_reg_id = null;
            public string comment;
            public string schema_issuer_did;
            public string schema_name;
            public JObject credential_proposal;

            // string issuer_did, string schema_id, string cred_def_id, string schema_version, string comment
            public SendCredentialParams(string comment)
            {
                // issuer_did == public DID from (GET /wallet/DID/public)
                // TODO get DID from agent through api every time
                issuer_did = "QDaSxvduZroHDKkdXKV5gG";
                // TODO get schema_id from GET /schemas/created, or make constant
                this.schema_id = "QDaSxvduZroHDKkdXKV5gG:2:enrollee: 1.0";
                // TODO get cred_def_id from GET /credential-definitions/created, or make constant
                this.cred_def_id = "QDaSxvduZroHDKkdXKV5gG:3:CL:112114:default";
                // set to the last segment of the schema_id, a three part version number that was randomly generated on startup of the Faber agent. Segments of the schema_id are separated by ":"s
                this.schema_version = "1.0";
                schema_name = "enrollee";
                // By setting auto-remove to true, ACA-Py will automatically remove the credential exchange record after the protocol completes.
                auto_remove = true;
                // The revoc_reg_id being null means that we won't be using a revocation registry and therefore can't revoke the credentials we issue.
                revoc_reg_id = null;
                // intended to let you know something about the credential being offered
                this.comment = comment;
                // same as issuer_did
                schema_issuer_did = "QDaSxvduZroHDKkdXKV5gG";

                var credential_proposal_object = new JObject();
                var attributes = new JArray();
                var attribute = new JObject();
                attribute.Add("name", "gpid");
                attribute.Add("value", "MY_TEST_GPID");
                attributes.Add(attribute);
                credential_proposal_object.Add("@type", "did:sov:BzCbsNYhMrjHiqZDTUASHg;spec/issue-credential/1.0/credential-preview");
                credential_proposal_object.Add("attributes", attributes);
                credential_proposal = credential_proposal_object;
            }

            public StringContent ToRequestContent()
            {
                return new StringContent(JsonConvert.SerializeObject(this));
            }
        }
    }

    public class VerifiableCredentialApiException : Exception
    {
        public VerifiableCredentialApiException() : base() { }
        public VerifiableCredentialApiException(string message) : base(message) { }
        public VerifiableCredentialApiException(string message, Exception inner) : base(message, inner) { }
    }
}

