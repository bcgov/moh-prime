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

        public VerifiableCredentialClient(
            HttpClient client)
        {
            // Auth header and api-key are injected in Startup.cs
            _client = client;
        }

        public async Task<JObject> CreateInvitation()
        {
            System.Console.WriteLine("CREATE_INVITATION**********************************************");

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
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::CreateInvitation");
            }

            System.Console.WriteLine("CREATE_INVITATION_RESPONSE");
            System.Console.WriteLine(JsonConvert.SerializeObject(response));
            System.Console.WriteLine("END_CREATE_INVITATION_RESPONSE---------------------------------------------");
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> IssueCredential(string connectionId)
        {
            System.Console.WriteLine("ISSUE_CREDENTIAL *******************************************************");

            JObject payload = new JObject
                {
                    { "connection_id", connectionId },
                    { "issuer_did", "QDaSxvduZroHDKkdXKV5gG" },
                    { "schema_id", "QDaSxvduZroHDKkdXKV5gG:2:enrollee:1.1" },
                    { "schema_issuer_did", "QDaSxvduZroHDKkdXKV5gG" },
                    { "schema_name", "enrollee" },
                    { "schema_version", "1.1" },
                    { "cred_def_id", "QDaSxvduZroHDKkdXKV5gG:3:CL:113261:default" },
                    { "comment", "PharmaNet GPID" },
                    { "auto_remove", true },
                    { "revoc_reg_id", null },
                    { "credential_proposal", new JObject
                        {
                            { "@type", "did:sov:BzCbsNYhMrjHiqZDTUASHg;spec/issue-credential/1.0/credential-preview" },
                            { "attributes", new JArray
                                {
                                    new JObject
                                    {
                                        { "name", "gpid" },
                                        { "value", "EXAMPLE_GPID_FOR_TESTING" }
                                    }
                                }
                            }
                        }
                    }
                };

            var httpContent = new StringContent(payload.ToString());
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
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::IssueCredential");
            }

            System.Console.WriteLine("ISSUE_CREDENTIAL_RESPONSE -------------------------------------------------");
            System.Console.WriteLine(JsonConvert.SerializeObject(response));
            System.Console.WriteLine("END_ISSUE_CREDENTIAL_RESPONSE ---------------------------------------------");

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        // public async Task<JObject> IssueCredential(string credential_exchange_id, JArray attributes)
        // {
        //     var credential_preview = new JObject();
        //     credential_preview.Add("@type", "did:sov:BzCbsNYhMrjHiqZDTUASHg;spec/issue-credential/1.0/credential-preview");
        //     credential_preview.Add("attributes", attributes);

        //     var issueParams = new JObject();
        //     issueParams.Add("credential_preview", credential_preview);
        //     issueParams.Add("comment", "issue_credential");

        //     System.Console.WriteLine("ISSUE_CREDENTIAL_PREVIEW_AND_PARAMS ------------------------------------------");
        //     System.Console.WriteLine(JsonConvert.SerializeObject(credential_preview));
        //     System.Console.WriteLine("------------------------------------------------------------------------------");
        //     System.Console.WriteLine(JsonConvert.SerializeObject(issueParams));
        //     System.Console.WriteLine("END_ISSUE_CREDENTIAL_ISSUE_PREVIEW_AND_PARAMS --------------------------------");

        //     var httpContent = new StringContent(issueParams.ToString());
        //     httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //     HttpResponseMessage response = null;
        //     try
        //     {
        //         response = await _client.PostAsync($"issue-credential/records/{credential_exchange_id}/issue", httpContent);
        //     }
        //     catch (Exception ex)
        //     {
        //         await LogError(httpContent, response, ex);
        //         throw new VerifiableCredentialApiException("Error occurred when calling Verfiable Credential API. Try again later.", ex);
        //     }

        //     if (!response.IsSuccessStatusCode)
        //     {
        //         await LogError(httpContent, response);
        //         throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was returned when calling Verifiable Credential API.");
        //     }

        //     System.Console.WriteLine("ISSUE_CREDENTIAL_RESPONSE ------------------------------------------------");
        //     System.Console.WriteLine(JsonConvert.SerializeObject(response));
        //     System.Console.WriteLine("END_ISSUE_CREDENTIAL_RESPONSE --------------------------------------------");

        //     return JObject.Parse(await response.Content.ReadAsStringAsync());
        // }

        public async Task<string> GetIssuerDID()
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync("wallet/did/public");
            }
            catch (Exception ex)
            {
                // TODO log error but needs to have overload to so httpContent not required, or put null
                // await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to GET issuer DID: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                // TODO log error but needs to have overload to so httpContent not required, or put null
                // await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetIssuerDID");
            }

            JObject body = JObject.Parse(await response.Content.ReadAsStringAsync());
            return body.Value<string>("did");
        }

        public async Task<JObject> GetSchema(string schemaIssuerDid)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"schemas/created?schema_issuer_did={schemaIssuerDid}");
            }
            catch (Exception ex)
            {
                // TODO log error but needs to have overload to so httpContent not required, or put null
                // await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to GET schema: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                // TODO log error but needs to have overload to so httpContent not required, or put null
                // await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetSchema");
            }

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<JObject> GetCredentialDefinition(string schemaIssuerDid)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync($"credential-definitions/created?schema_issuer_did={schemaIssuerDid}");
            }
            catch (Exception ex)
            {
                // TODO log error but needs to have overload to so httpContent not required, or put null
                // await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException("Error occurred attempting to GET credential definition: ", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                // TODO log error but needs to have overload to so httpContent not required, or put null
                // await LogError(httpContent, response, ex);
                throw new VerifiableCredentialApiException($"Error code {response.StatusCode} was provided when calling VerifiableCredentialClient::GetCredentialDefinition");
            }

            return JObject.Parse(await response.Content.ReadAsStringAsync());
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

            // _logger.Error(exception, secondaryMessage, new Object[] { response, content });
            System.Console.WriteLine("ERROR_RESPONSE_AND_CONTENT ------------------------------------------------");
            System.Console.WriteLine(JsonConvert.SerializeObject(secondaryMessage));
            System.Console.WriteLine("---------------------------------------------------------------------------");
            System.Console.WriteLine(JsonConvert.SerializeObject(response));
            System.Console.WriteLine("---------------------------------------------------------------------------");
            System.Console.WriteLine(JsonConvert.SerializeObject(content));
            System.Console.WriteLine("END_ERROR_RESPONSE_AND_CONTENT --------------------------------------------");
        }

        private class SendCredentialParams
        {
            public string connection_id;
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
            public SendCredentialParams(string connection_id, string comment)
            {
                this.connection_id = connection_id;
                // issuer_did == public DID from (GET /wallet/DID/public)
                // TODO get DID from agent through api every time
                issuer_did = "QDaSxvduZroHDKkdXKV5gG";
                // TODO get schema_id from GET /schemas/created, or make constant
                this.schema_id = "QDaSxvduZroHDKkdXKV5gG:2:enrollee:1.1";
                // TODO get cred_def_id from GET /credential-definitions/created, or make constant
                this.cred_def_id = "QDaSxvduZroHDKkdXKV5gG:3:CL:113261:default";
                // set to the last segment of the schema_id, a three part version number that was randomly generated on startup of the Faber agent. Segments of the schema_id are separated by ":"s
                this.schema_version = "1.1";
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
                attribute.Add("mime-type", "text/plain");
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

