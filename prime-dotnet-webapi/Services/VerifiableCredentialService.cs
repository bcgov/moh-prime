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
using Microsoft.EntityFrameworkCore;

// TODO should implement a queue when using webhooks
namespace Prime.Services
{
    public class WebhookTopic
    {
        public const string Connections = "connections";
        public const string IssueCredential = "issue_credential";
        public const string RevocationRegistry = "revocation_registry";
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
                case WebhookTopic.RevocationRegistry:
                    _logger.LogInformation("Revocation Registry data: for {@JObject}", JsonConvert.SerializeObject(data));
                    return true;
                default:
                    _logger.LogError("Webhook {topic} is not supported", topic);
                    return false;
            }
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

            var credential = new Credential
            {
                SchemaId = schemaId,
                CredentialDefinitionId = credentialDefinitionId,
                Alias = alias,
                Base64QRCode = qrCodeImageAsBase64
            };
            _context.Credentials.Add(credential);
            await _context.SaveChangesAsync();

            var enrolleeCredential = new EnrolleeCredential
            {
                EnrolleeId = enrollee.Id,
                CredentialId = credential.Id
            };

            _context.EnrolleeCredentials.Add(enrolleeCredential);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not store connection invitation.");
            }

            // TODO after testing don't need to pass back the invitation
            return invitation;
        }

        public async Task<bool> RevokeCredentialsAsync(int enrolleeId)
        {
            var enrolleeCredentials = await _context.EnrolleeCredentials
                .Include(ec => ec.Credential)
                .Where(ec => ec.EnrolleeId == enrolleeId)
                .Where(ec => ec.Credential.CredentialExchangeId != null)
                .Where(ec => ec.Credential.RevokedCredentialDate == null)
                .Select(ec => ec.Credential)
                .ToListAsync();

            foreach (var credential in enrolleeCredentials)
            {
                var success = credential.AcceptedCredentialDate == null
                    ? await _verifiableCredentialClient.DeleteCredentialAsync(credential)
                    : await _verifiableCredentialClient.RevokeCredentialAsync(credential);

                if (success)
                {
                    credential.RevokedCredentialDate = DateTimeOffset.Now;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // Handle webhook events for connection states.
        private async Task<bool> HandleConnectionAsync(JObject data)
        {
            var state = data.Value<string>("state");
            string connectionId;

            _logger.LogInformation("Connection state \"{state}\" for {@JObject}", state, JsonConvert.SerializeObject(data));

            switch (state)
            {
                case ConnectionState.Invitation:
                    // Enrollee Id stored as alias on invitation
                    await UpdateCredentialConnectionId(data.Value<int>("alias"), data.Value<string>("connection_id"));
                    return true;

                case ConnectionState.Request:
                    return true;

                case ConnectionState.Response:
                    var alias = data.Value<int>("alias");
                    connectionId = data.Value<string>("connection_id");

                    _logger.LogInformation("Issuing a credential with this connection_id: {connectionId}", connectionId);

                    // Assumed that when a connection invitation has been sent and accepted
                    // the enrollee has been approved, and has a GPID for issuing a credential
                    // TODO should be queued and managed outside of webhook callback
                    await IssueCredential(connectionId, alias);
                    _logger.LogInformation("Credential has been issued for connection_id: {connectionId}", connectionId);

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
                    await UpdateCredentialAfterIssued(data);
                    return true;
                default:
                    _logger.LogError("Credential exchange state {state} is not supported", state);
                    return false;
            }
        }

        private async Task<int> UpdateCredentialAfterIssued(JObject data)
        {
            var connection_id = (string)data.SelectToken("connection_id");

            var credential = GetCredentialByConnectionIdAsync(connection_id);

            if (credential != null)
            {
                credential.AcceptedCredentialDate = DateTimeOffset.Now;
            }

            return await _context.SaveChangesAsync();
        }

        private async Task<int> UpdateCredentialConnectionId(int enrolleeId, string connection_id)
        {
            // Add ConnectionId to Enrollee's newest credential
            var credential = await _context.EnrolleeCredentials
                .Include(ec => ec.Credential)
                .Where(ec => ec.EnrolleeId == enrolleeId)
                .OrderByDescending(ec => ec.Id)
                .Select(ec => ec.Credential)
                .FirstOrDefaultAsync();

            if (credential != null)
            {
                _logger.LogInformation("Updating this credential's (Id = {id}) connectionId to {connection_id}", credential.Id, connection_id);

                credential.ConnectionId = connection_id;
                _context.Credentials.Update(credential);
            }

            return await _context.SaveChangesAsync();
        }

        private Credential GetCredentialByConnectionIdAsync(string connectionId)
        {
            return _context.Credentials
                    .SingleOrDefault(c => c.ConnectionId == connectionId);
        }

        // Issue a credential to an active connection.
        private async Task<JObject> IssueCredential(string connectionId, int enrolleeId)
        {
            var enrollee = _context.Enrollees
                .SingleOrDefault(e => e.Id == enrolleeId);

            var credential = GetCredentialByConnectionIdAsync(connectionId);

            if (credential == null || credential.AcceptedCredentialDate != null)
            {
                _logger.LogInformation("Cannot issue credential, credential with this connectionId:{connectionId} from database is null, or a credential has already been accepted.", connectionId);
                return null;
            }

            var credentialAttributes = await CreateCredentialAttributesAsync(enrolleeId);
            var credentialOffer = await CreateCredentialOfferAsync(connectionId, credentialAttributes);
            var issueCredentialResponse = await _verifiableCredentialClient.IssueCredentialAsync(credentialOffer);

            // Set credentials CredentialExchangeId from issue credential response
            credential.CredentialExchangeId = (string)issueCredentialResponse.SelectToken("credential_exchange_id");
            _context.Credentials.Update(credential);

            await _context.SaveChangesAsync();

            return issueCredentialResponse;
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
                { "auto_remove", false },
                { "trace", false },
                {
                    "credential_proposal",
                    new JObject
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

            foreach (var careSetting in enrollee.EnrolleeCareSettings)
            {
                await _context.Entry(careSetting).Reference(o => o.CareSetting).LoadAsync();
            }

            JArray attributes = new JArray
            {
                new JObject
                {
                    { "name", "GPID" },
                    { "value", enrollee.GPID }
                },
                new JObject
                {
                    { "name", "Renewal Date" },
                    { "value", enrollee.ExpiryDate.Value.Date.ToShortDateString() }
                },
                new JObject
                {
                    { "name", "TOA Name" },
                    { "value", enrollee.AssignedTOAType.Value.ToString() }
                },
                new JObject
                {
                    { "name", "Care Type Setting" },
                    { "value", string.Join(',', enrollee.EnrolleeCareSettings.Select(ecs => ecs.CareSetting.Name)) }
                },
                new JObject
                {
                    { "name", "Remote User" },
                    { "value", enrollee.EnrolleeRemoteUsers.Count > 0 ? "true" : "false"}
                }
            };

            _logger.LogInformation("Credential offer attributes for {@JObject}", JsonConvert.SerializeObject(attributes));

            return attributes;
        }
    }
}
