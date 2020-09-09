using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QRCoder;

using Prime.Models;
using Prime.HttpClients;

// TODO should implement a queue when using webhooks
namespace Prime.Services
{
    public class WebhookTopic
    {
        public const string Connections = "connections";
        public const string IssueCredential = "issue_credential";
    }

    public class ConnectionState
    {
        public const string Invitation = "invitation";
        public const string Request = "request";
        public const string Response = "response";
        public const string Active = "active";
    }

    public class CredentialExchangeState
    {
        public const string OfferSent = "offer_sent";
        public const string RequestReceived = "request_received";
        public const string CredentialIssued = "credential_issued";
    }
    public class VerifiableCredentialService : BaseService, IVerifiableCredentialService
    {
        private readonly IVerifiableCredentialClient _verifiableCredentialClient;
        private readonly IEnrolleeService _enrolleeService;
        private readonly ILogger _logger;

        public VerifiableCredentialService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IVerifiableCredentialClient verifiableCredentialClient,
            IEnrolleeService enrolleeService,
            ILogger<VerifiableCredentialService> logger)
            : base(context, httpContext)
        {
            _verifiableCredentialClient = verifiableCredentialClient;
            _enrolleeService = enrolleeService;
            _logger = logger;
        }

        // Create an invitation to establish a connection between the agents.
        public async Task<JObject> CreateConnectionAsync(Enrollee enrollee)
        {
            var alias = enrollee.Id.ToString();
            var issuerDid = await _verifiableCredentialClient.GetIssuerDidAsync();
            var schemaId = await _verifiableCredentialClient.GetSchemaId(issuerDid);
            var invitation = await _verifiableCredentialClient.CreateInvitationAsync(alias);
            var invitationUrl = invitation.Value<string>("invitation_url");
            var credentialDefinitionId = await _verifiableCredentialClient.GetCredentialDefinitionIdAsync(schemaId);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(invitationUrl, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20, "#003366", "#ffffff");

            enrollee.Credential = new Credential
            {
                SchemaId = schemaId,
                CredentialDefinitionId = credentialDefinitionId,
                Alias = alias,
                Base64QRCode = qrCodeImageAsBase64
            };

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not store connection invitation.");
            }

            // TODO after testing don't need to pass back the invitation
            return invitation;
        }

        // Handle webhook events pushed by the issuing agent.
        public async Task<bool> WebhookAsync(JObject data, string topic)
        {
            _logger.LogInformation("Webhook topic \"{topic}\"", topic);

            switch (topic)
            {
                case WebhookTopic.Connections:
                    return await HandleConnectionAsync(data);
                case WebhookTopic.IssueCredential:
                    return await HandleIssueCredentialAsync(data);
                default:
                    _logger.LogError("Webhook {topic} is not supported", topic);
                    return false;
            }
        }

        // Handle webhook events for connection states.
        private async Task<bool> HandleConnectionAsync(JObject data)
        {
            var state = data.Value<string>("state");

            _logger.LogInformation("Connection state \"{state}\" for {@JObject}", state, JsonConvert.SerializeObject(data));

            switch (state)
            {
                case ConnectionState.Invitation:
                case ConnectionState.Request:
                    return true;
                case ConnectionState.Response:
                    var connectionId = data.Value<string>("connection_id");
                    var alias = data.Value<int>("alias");

                    _logger.LogInformation("Issuing a credential with this connection_id: {connectionId}", connectionId);

                    // Assumed that when a connection invitation has been sent and accepted
                    // the enrollee has been approved, and has a GPID for issuing a credential
                    // TODO should be queued and managed outside of webhook callback
                    var issueCredentialResponse = await IssueCredential(connectionId, alias);

                    _logger.LogInformation("Credential has been issued for connection_id: {connectionId} with response {@JObject}", connectionId, JsonConvert.SerializeObject(issueCredentialResponse));

                    return true;
                case ConnectionState.Active:
                    return true;
                default:
                    _logger.LogError("Connection state {state} is not supported", state);
                    return false;
            }
        }

        // Handle webhook events for issue credential topics.
        private async Task<bool> HandleIssueCredentialAsync(JObject data)
        {
            var state = data.Value<string>("state");

            _logger.LogInformation("Issue credential state \"{state}\" for {@JObject}", state, JsonConvert.SerializeObject(data));

            switch (state)
            {
                case CredentialExchangeState.OfferSent:
                case CredentialExchangeState.RequestReceived:
                    return true;
                case CredentialExchangeState.CredentialIssued:
                    await UpdateAcceptedCredentialDate(data);
                    return true;
                default:
                    _logger.LogError("Credential exchange state {state} is not supported", state);
                    return false;
            }
        }

        private async Task<int> UpdateAcceptedCredentialDate(JObject data)
        {
            var gpid = (string)data.SelectToken("credential_proposal_dict.credential_proposal.attributes[?(@.name == 'gpid')].value");
            var enrollee = _context.Enrollees
                .SingleOrDefault(e => e.GPID == gpid);

            if (enrollee != null)
            {
                var credential = GetCredentialByIdAsync((int)enrollee.CredentialId);
                credential.AcceptedCredentialDate = DateTimeOffset.Now;
            }

            return await _context.SaveChangesAsync();
        }

        private Credential GetCredentialByIdAsync(int credentialId)
        {
            return _context.Credentials
                    .SingleOrDefault(c => c.Id == credentialId);
        }

        // Issue a credential to an active connection.
        private async Task<JObject> IssueCredential(string connectionId, int enrolleeId)
        {
            var enrollee = _context.Enrollees
                .SingleOrDefault(e => e.Id == enrolleeId);

            var credential = GetCredentialByIdAsync((int)enrollee.CredentialId);

            if (credential.AcceptedCredentialDate != null)
            {
                return null;
            }

            var credentialAttributes = await CreateCredentialAttributesAsync(enrolleeId);
            var credentialOffer = await CreateCredentialOfferAsync(connectionId, credentialAttributes);
            return await _verifiableCredentialClient.IssueCredentialAsync(credentialOffer);
        }

        // Create the credential offer.
        private async Task<JObject> CreateCredentialOfferAsync(string connectionId, JArray attributes)
        {
            var issuerDid = await _verifiableCredentialClient.GetIssuerDidAsync();
            var schemaId = await _verifiableCredentialClient.GetSchemaId(issuerDid);
            var schema = (await _verifiableCredentialClient.GetSchema(schemaId)).Value<JObject>("schema");
            var credentialDefinitionId = await _verifiableCredentialClient.GetCredentialDefinitionIdAsync(schemaId);

            JObject credentialOffer = new JObject
                {
                    { "connection_id", connectionId },
                    { "issuer_did", issuerDid },
                    { "schema_id", schemaId },
                    { "schema_issuer_did", issuerDid },
                    { "schema_name", schema.Value<string>("name") },
                    { "schema_version", schema.Value<string>("version") },
                    { "cred_def_id", credentialDefinitionId },
                    { "comment", "PharmaNet GPID" },
                    { "auto_remove", true },
                    { "trace", false },
                    { "credential_proposal", new JObject
                        {
                            { "@type", "did:sov:BzCbsNYhMrjHiqZDTUASHg;spec/issue-credential/1.0/credential-preview" },
                            { "attributes", attributes }
                        }
                    }
                };

            _logger.LogInformation("Credential offer for connection ID \"{connectionId}\" for {@JObject}", connectionId, JsonConvert.SerializeObject(credentialOffer));

            return credentialOffer;
        }

        // Create the credential proposal attributes.
        private async Task<JArray> CreateCredentialAttributesAsync(int enrolleeId)
        {
            // TODO Update schema to rename organization_type to care_setting
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);
            var enrolleeCareSetting = enrollee.EnrolleeCareSettings.Single();
            await _context.Entry(enrolleeCareSetting).Reference(o => o.CareSetting).LoadAsync();

            JArray attributes = new JArray
            {
                new JObject
                {
                    { "name", "gpid" },
                    { "value", enrollee.GPID }
                },
                new JObject
                {
                    { "name", "renewal_date" },
                    { "value", enrollee.ExpiryDate }
                },
                new JObject
                {
                    { "name", "organization_type" },
                    { "value", enrolleeCareSetting.CareSetting.Name }
                },
                new JObject
                {
                    { "name", "user_class" },
                    { "value", enrollee.IsRegulatedUser() ? "RU" : "OBO" }
                },
                new JObject
                {
                    { "name", "remote_access" },
                    { "value", enrollee.RequestingRemoteAccess ? "true" : "false"}
                }
            };

            _logger.LogInformation("Credential offer attributes for {@JObject}", JsonConvert.SerializeObject(attributes));

            return attributes;
        }
    }
}
