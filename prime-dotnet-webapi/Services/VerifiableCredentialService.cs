using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using Prime.Models;
using Prime.Services.Clients;

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

        public async Task<JObject> CreateConnection()
        {
            return await _verifiableCredentialClient.CreateInvitation();
        }

        public async Task<JObject> IssueCredential(string connectionId)
        {
            // TODO get the enrollee information for issuing a credential
            // TODO build out the request information for issuance
            return await _verifiableCredentialClient.IssueCredential(connectionId);
        }

        public async Task<bool> Webhook(JObject data, string topic)
        {
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

        private async Task<bool> handleConnection(JObject data)
        {
            var state = data.GetValue("state").ToString();

            // _logger.Information($"Connection state \"{response}\" for @JObject", data);
            System.Console.WriteLine($"Connection state \"{ConnectionStates.Response}\"");
            System.Console.WriteLine(JsonConvert.SerializeObject(data));

            switch (state)
            {
                case ConnectionStates.Response:
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
                default:
                    // _logger.Error($"Connection state {state} is not supported");
                    System.Console.WriteLine($"Connection state {state} is not supported");
                    return await Task.FromResult(false);
            }
        }

        private async Task<bool> handleIssueCredential(JObject data)
        {
            var state = data.GetValue("state").ToString();

            switch (state)
            {
                case CredentialExchangeStates.RequestReceived:
                    System.Console.WriteLine("CREDENTIAL_EXCHANGE_STATES_REQUEST_RECEIVED -------------------------------");
                    System.Console.WriteLine(JsonConvert.SerializeObject(data));
                    System.Console.WriteLine("END_CREDENTIAL_EXCHANGE_STATES_REQUEST_RECEIVED ---------------------------");

                    // string credential_exchange_id = data.GetValue("credential_exchange_id").ToString();
                    // var attributes = data.GetValue("attributes") as JArray;

                    // await _verifiableCredentialClient.IssueCredential(credential_exchange_id, attributes);
                    return await Task.FromResult(true);

                default:
                    // _logger.Error($"Credential exchange state {state} is not supported");
                    System.Console.WriteLine($"Credential exchange state {state} is not supported");
                    return await Task.FromResult(false);
            }
        }
    }
}
