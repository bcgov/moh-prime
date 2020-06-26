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
            System.Console.WriteLine("CREATE_CONNECTION 1%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");

            var invitationResponse = await _verifiableCredentialClient.CreateInvitation();
            var invitation = invitationResponse.GetValue("invitation");
            var invitationURL = invitationResponse.GetValue("invitation_url").ToString();

            if (invitation == null)
            {
                return invitationResponse;
            }

            System.Console.WriteLine("CREATE_CONNECTION 2%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");

            var receiveResponse = await _verifiableCredentialClient.ReceiveInvitation(invitation.ToString());
            var connection_id = receiveResponse.GetValue("connection_id").ToString();

            if (connection_id == null)
            {
                return receiveResponse;
            }

            System.Console.WriteLine("CREATE_CONNECTION 3%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");

            var acceptResponse = await _verifiableCredentialClient.AcceptInvitation(connection_id);
            return acceptResponse;
        }

        public async Task<JObject> SendCredential(String gpid)
        {
            var sendResponse = await _verifiableCredentialClient.SendCredential(gpid);
            return sendResponse;
        }

        public async Task<bool> Create(JObject data, string topic)
        {
            switch (topic)
            {
                case WebhookTopic.Connections:
                    System.Console.WriteLine("CREATE_CONNECTIONS@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(data));
                    System.Console.WriteLine("END_CREATE_CONNECTIONS@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

                    return await handleConnection(data);
                case WebhookTopic.IssueCredential:
                    System.Console.WriteLine("ISSUE_CREDENTIAL@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(data));
                    System.Console.WriteLine("END_ISSUE_CREDENTIAL@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

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

            System.Console.WriteLine("HANDLE_CONNECTION@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            System.Console.WriteLine($"{state}");
            System.Console.WriteLine("END_HANDLE_CONNECTION@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

            if (state == "response")
            {
                var connection_id = data.GetValue("connection_id").ToString();
                Console.WriteLine($"About to issue a credential with this connection_id: {connection_id}");
                // var response = await _verifiableCredentialClient.AcceptRequest(connection_id);

                // Connection successful, issue credential
                // if (response != null)
                // {
                var credResponse = await SendCredential(connection_id);
                // }

                System.Console.WriteLine("HANDLE_CONNECTION_RESPONSE@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                System.Console.WriteLine(JsonConvert.SerializeObject(credResponse));
                System.Console.WriteLine("END_HANDLE_CONNECTION_RESPONSE@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            }

            // TODO remove results since they provide no context
            return await Task.FromResult(true);
        }

        private async Task<bool> handleIssueCredential(JObject data)
        {
            var state = data.GetValue("state").ToString();
            string credential_exchange_id;
            switch (state)
            {
                case CredentialExchangeStates.RequestReceived:
                    // Call aries agent to create credential
                    credential_exchange_id = data.GetValue("credential_exchange_id").ToString();
                    var attributes = data.GetValue("attributes") as JArray;

                    System.Console.WriteLine("CREDENTIAL_EXCHANGE_STATES_REQUEST_RECEIVED@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(credential_exchange_id));
                    System.Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(attributes));
                    System.Console.WriteLine("END_CREDENTIAL_EXCHANGE_STATES_REQUEST_RECEIVED@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

                    await _verifiableCredentialClient.IssueCredential(credential_exchange_id, attributes);
                    return await Task.FromResult(true);

                case CredentialExchangeStates.Issued:
                    // Store a received credential
                    credential_exchange_id = data.GetValue("credential_exchange_id").ToString();

                    System.Console.WriteLine("CREDENTIAL_EXCHANGE_STATES_ISSUED@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
                    System.Console.WriteLine(JsonConvert.SerializeObject(credential_exchange_id));
                    System.Console.WriteLine("END_CREDENTIAL_EXCHANGE_STATES_ISSUED@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

                    await _verifiableCredentialClient.StoreCredential(credential_exchange_id);
                    // TODO store credential in our database
                    return await Task.FromResult(true);

                default:
                    // _logger.Error($"Received unexpected state {state} for CredentialExchangeState ${state}");
                    System.Console.WriteLine($"Received unexpected state {state} for CredentialExchangeState ${state}");
                    return await Task.FromResult(false);
            }
        }
    }
}
