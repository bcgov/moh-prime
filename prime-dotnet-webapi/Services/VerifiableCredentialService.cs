using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Prime.Services.Clients;

// TODO should implement a queue when using webhooks
namespace Prime.Services
{
    public class WebhookTopic
    {
        public const string Connections = "connections";
        public const string IssueCredential = "issue_credential";
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
        public const string Issued = "issued";
    }

    public class VerifiableCredentialService : BaseService, IVerifiableCredentialService
    {
        private readonly IVerifiableCredentialClient _verifiableCredentialClient;
        private readonly IEnrolleeService _enrolleeService;

        public VerifiableCredentialService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IVerifiableCredentialClient verifiableCredentialClient,
            IEnrolleeService enrolleeService)
            : base(context, httpContext)
        {
            _verifiableCredentialClient = verifiableCredentialClient;
        }

        // Create an invitation to establish a connection between the agents.
        public async Task<JObject> CreateConnection()
        {
            return await _verifiableCredentialClient.CreateInvitation();
        }

        // Handle webhook events pushed by the issuing agent.
        public async Task<bool> Webhook(JObject data, string topic)
        {
            // _logger.Information($"Webhook topic \"{topic}\" for @JObject", data);
            System.Console.WriteLine($"Webhook topic \"{topic}\"");
            System.Console.WriteLine(JsonConvert.SerializeObject(data));

            switch (topic)
            {
                case WebhookTopic.Connections:
                    return await handleConnection(data);
                case WebhookTopic.IssueCredential:
                    return await handleIssueCredential(data);
                default:
                    // _logger.Error($"Webhook {topic} is not supported");
                    System.Console.WriteLine($"Webhook {topic} is not supported");
                    return false;
            };
        }

        // Handle webhook events for connection states.
        private async Task<bool> handleConnection(JObject data)
        {
            var state = data.GetValue("state").ToString();

            // _logger.Information($"Connection state \"{state}\" for @JObject", data);
            System.Console.WriteLine($"Connection state \"{state}\"");
            System.Console.WriteLine(JsonConvert.SerializeObject(data));

            switch (state)
            {
                case ConnectionStates.Invitation:
                case ConnectionStates.Request:
                    return await Task.FromResult(true);
                case ConnectionStates.Response:
                    // TODO store the connection ID for checking whether it is active in the future
                    var connection_id = data.GetValue("connection_id").ToString();

                    // _logger.Information($"Issuing a credential with this connection_id: {connection_id}");
                    Console.WriteLine($"Issuing a credential with this connection_id: {connection_id}");

                    // Assumed that when a connection invitation has been sent and accepted
                    // the enrollee has been approved, and has a GPID for issuing a credential
                    var issueCredentialResponse = await IssueCredential(connection_id);

                    // _logger.Information($"Credential has been issued for connection_id: {connection_id} with response @JObject", issueCredentialResponse);
                    Console.WriteLine($"Credential has been issued for connection_id: {connection_id}");
                    System.Console.WriteLine(JsonConvert.SerializeObject(issueCredentialResponse));

                    return await Task.FromResult(true);

                case ConnectionStates.Active:
                    // TODO store the connection ID for checking whether it is active in the future
                    return await Task.FromResult(true);
                default:
                    // _logger.Error($"Connection state {state} is not supported");
                    System.Console.WriteLine($"Connection state {state} is not supported");
                    return await Task.FromResult(false);
            }
        }

        // Issue a credential to an agent.
        private async Task<JObject> IssueCredential(string connectionId)
        {
            // TODO get the enrollee information for creating a claim for issuing a credential
            // TODO build out the request information for issuance
            return await _verifiableCredentialClient.IssueCredential(connectionId);
        }

        // Handle webhook events for issue credential topics.
        private async Task<bool> handleIssueCredential(JObject data)
        {
            var state = data.GetValue("state").ToString();

            // _logger.Information($"Issue credential state \"{state}\" for @JObject", data);
            System.Console.WriteLine($"Issue credential state \"{state}\"");
            System.Console.WriteLine(JsonConvert.SerializeObject(data));

            switch (state)
            {
                case CredentialExchangeStates.OfferSent:
                case CredentialExchangeStates.RequestReceived:
                // TODO store that the credential has been accepted
                case CredentialExchangeStates.Issued:
                    return await Task.FromResult(true);
                default:
                    // _logger.Error($"Credential exchange state {state} is not supported");
                    System.Console.WriteLine($"Credential exchange state {state} is not supported");
                    return await Task.FromResult(false);
            }
        }
    }
}
