using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QRCoder;

using Prime.Models;
using Prime.Services.Clients;

// TODO should implement a queue when using webhooks
namespace Prime.Services
{
    public class WebhookTopic
    {
        public const string Connections = "connections";
        public const string IssueCredential = "issue_credential";
        public const string PresentProof = "present_proof";
    }

    public class ConnectionStates
    {
        public const string Invitation = "invitation";
        public const string Request = "request";
        public const string Response = "response";
        public const string Active = "active";
    }

    public class CredentialExchangeStates
    {
        public const string OfferSent = "offer_sent";
        public const string RequestReceived = "request_received";
        public const string CredentialIssued = "credential_issued";
    }

    public class PresentProofStates
    {
        public const string RequestSent = "request_sent";
        public const string PresentationReceived = "presentation_received";
        public const string Verified = "verified";
    }


    public class VerifiableCredentialService : BaseService, IVerifiableCredentialService
    {
        // dev agent schema id
        // private static readonly string SCHEMA_ID = "QDaSxvduZroHDKkdXKV5gG:2:enrollee:2.0";

        // test agent schema id
        private static readonly string SCHEMA_ID = "TVmQfMZwLFWWK3z1RLgFBR:2:enrollee:1.0";

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
            var invitation = await _verifiableCredentialClient.CreateInvitationAsync(alias);
            var invitationUrl = invitation.Value<string>("invitation_url");
            var credentialDefinitionId = await _verifiableCredentialClient.GetCredentialDefinitionIdAsync(SCHEMA_ID);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(invitationUrl, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20, "#003366", "#ffffff");

            enrollee.Credential = new Credential
            {
                SchemaId = SCHEMA_ID,
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

        // Create an invitation to establish a connection between the agents.
        public async Task SendProofRequest(Enrollee enrollee)
        {
            // TODO add a new schema with:
            // - POS vendor
            // - Legal Name of Organization, and/or BC Registration ID
            // - Status of Approval
            // - Name of Site (Location?)
            // - Address of Site
            // TODO create a credential definition

            // TODO create proof request JObject model
            // - Populate model with enrollee data and API queried values

            // TODO send a proof request, and test on mobile the states
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
                case WebhookTopic.PresentProof:
                    return await HandlePresentProofAsync(data);
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
                case ConnectionStates.Invitation:
                case ConnectionStates.Request:
                    return true;
                case ConnectionStates.Response:
                    var connectionId = data.Value<string>("connection_id");
                    var alias = data.Value<int>("alias");

                    _logger.LogInformation("Issuing a credential with this connection_id: {connectionId}", connectionId);

                    // Assumed that when a connection invitation has been sent and accepted
                    // the enrollee has been approved, and has a GPID for issuing a credential
                    // TODO should be queued and managed outside of webhook callback
                    var issueCredentialResponse = await IssueCredential(connectionId, alias);

                    _logger.LogInformation("Credential has been issued for connection_id: {connectionId} with response {@JObject}", connectionId, JsonConvert.SerializeObject(issueCredentialResponse));

                    return true;
                case ConnectionStates.Active:
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
                case CredentialExchangeStates.OfferSent:
                case CredentialExchangeStates.RequestReceived:
                    return true;
                case CredentialExchangeStates.CredentialIssued:
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
            var schema = (await _verifiableCredentialClient.GetSchema(SCHEMA_ID)).Value<JObject>("schema");
            var credentialDefinitionId = await _verifiableCredentialClient.GetCredentialDefinitionIdAsync(SCHEMA_ID);

            JObject credentialOffer = new JObject
                {
                    { "connection_id", connectionId },
                    { "issuer_did", issuerDid },
                    { "schema_id", SCHEMA_ID },
                    { "schema_issuer_did", issuerDid },
                    { "schema_name", schema.Value<string>("name") },
                    { "schema_version", schema.Value<string>("version") },
                    { "cred_def_id", credentialDefinitionId },
                    { "comment", "PharmaNet GPID" },
                    { "auto_remove", true },
                    { "revoc_reg_id", null },
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
            var enrollee = await _enrolleeService.GetEnrolleeAsync(enrolleeId);

            var organizationType = _context.OrganizationTypes.SingleOrDefault(t => t.Code == enrollee.EnrolleeOrganizationTypes.FirstOrDefault().OrganizationTypeCode);

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
                    { "value", organizationType.Name }
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

        private async Task<bool> HandlePresentProofAsync(JObject data)
        {
            var state = data.Value<string>("state");

            _logger.LogInformation("Present proof state \"{state}\" for {@JObject}", JsonConvert.SerializeObject(data));

            switch (state)
            {
                case PresentProofStates.RequestSent:
                    var presentationExchangeId = data.Value<string>("presentation_exchange_id");
                    await _verifiableCredentialClient.GetPresentationProof(presentationExchangeId);
                    // TODO log that the proof request was received for auditing by checking that state is verified
                    return true;
                case PresentProofStates.PresentationReceived:
                    return true;
                case PresentProofStates.Verified:
                    // TODO proof positive: state=verified and verified=true
                    // TODO perform business logic to inform Medinet
                    return true;
                default:
                    _logger.LogError($"Present proof state {state} is not supported");
                    return false;
            }
        }
    }
}
