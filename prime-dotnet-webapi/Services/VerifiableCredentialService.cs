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

        public async Task<JObject> SendCredential(String gpid)
        {
            return await _verifiableCredentialClient.SendCredential(gpid); ;
        }

        public async Task<bool> Create(JObject data, string topic)
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

            switch (state)
            {
                case ConnectionStates.Response:
                    System.Console.WriteLine("HANDLE_CONNECTION_RESPONSE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(data));

                    var connection_id = data.GetValue("connection_id").ToString();

                    Console.WriteLine($"About to issue a credential with this connection_id: {connection_id}");

                    var credResponse = await SendCredential(connection_id);

                    System.Console.WriteLine(JsonConvert.SerializeObject(credResponse));
                    System.Console.WriteLine("END_HANDLE_CONNECTION_RESPONSE @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

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
            string credential_exchange_id;
            switch (state)
            {
                case CredentialExchangeStates.RequestReceived:
                    System.Console.WriteLine("CREDENTIAL_EXCHANGE_STATES_REQUEST_RECEIVED @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(data));

                    credential_exchange_id = data.GetValue("credential_exchange_id").ToString();
                    var attributes = data.GetValue("attributes") as JArray;

                    System.Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(attributes));
                    System.Console.WriteLine("END_CREDENTIAL_EXCHANGE_STATES_REQUEST_RECEIVED @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

                    await _verifiableCredentialClient.IssueCredential(credential_exchange_id, attributes);
                    return await Task.FromResult(true);

                default:
                    // _logger.Error($"Credential exchange state {state} is not supported");
                    System.Console.WriteLine($"Credential exchange state {state} is not supported");
                    return await Task.FromResult(false);
            }
        }
    }
}
