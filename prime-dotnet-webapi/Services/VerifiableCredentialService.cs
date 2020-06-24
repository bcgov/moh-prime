using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Prime.Models;
using Prime.Services.Clients;
using Newtonsoft.Json.Linq;

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
        public VerifiableCredentialService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IVerifiableCredentialClient verifiableCredentialClient)
            : base(context, httpContext)
        {
            _verifiableCredentialClient = verifiableCredentialClient;
        }

        public async Task<JObject> CreateConnection()
        {
            var invitationResponse = await _verifiableCredentialClient.CreateInvitation();
            var invitation = invitationResponse.GetValue("invitation");

            if (invitation == null)
            {
                return invitationResponse;
            }

            var receiveResponse = await _verifiableCredentialClient.ReceiveInvitation(invitation.ToString());
            var connection_id = receiveResponse.GetValue("connection_id").ToString();

            if (connection_id == null)
            {
                return receiveResponse;
            }

            var acceptResponse = await _verifiableCredentialClient.AcceptInvitation(connection_id);

            return acceptResponse;
        }

        public async Task<JObject> SendCredential(String gpid)
        {

            var sendResponse = await _verifiableCredentialClient.SendCredential(gpid);
            return sendResponse;
        }


        // TODO temporary data object provided, and return type
        // @see https://github.com/esune/issuer-kit/blob/api-refactor/api/src/services/webhooks/webhooks.class.ts#L30
        public async Task<bool> create(JObject data, string topic)
        {
            switch (topic)
            {
                case WebhookTopic.Connections:
                    return await handleConnection(data);
                case WebhookTopic.IssueCredential:
                    return await handleIssueCredential(data);
                default:
                    // TODO log a message $"Webhook {topic} is not supported";
                    return false;
            };
        }

        // TODO temporary data object provided, and return type
        // @see https://github.com/esune/issuer-kit/blob/api-refactor/api/src/services/webhooks/webhooks.class.ts#L44
        private async Task<bool> handleConnection(JObject data)
        {
            var connection_id = data.GetValue("connection_id").ToString();
            // TODO implement connection webhook logic
            var response = await _verifiableCredentialClient.AcceptRequest(connection_id);

            // connection successful, issue credential
            if (response != null)
            {
                var credResponse = await SendCredential(connection_id);
            }

            return await Task.FromResult(true);
        }

        // TODO temporary data object provided, and return type
        // @see https://github.com/esune/issuer-kit/blob/api-refactor/api/src/services/webhooks/webhooks.class.ts#L48
        private async Task<bool> handleIssueCredential(JObject data)
        {
            var state = data.GetValue("state").ToString();
            string credential_exchange_id;
            // TODO switch statement based on the credential exchange state
            switch (state)
            {
                case "request_sent":
                    // call aries agent to create credential
                    credential_exchange_id = data.GetValue("credential_exchange_id").ToString();
                    var attributes = data.GetValue("attributes") as JArray;
                    await _verifiableCredentialClient.IssueCredential(credential_exchange_id, attributes);
                    return await Task.FromResult(true);
                case "credential_received":
                    // store a received credential
                    credential_exchange_id = data.GetValue("credential_exchange_id").ToString();
                    await _verifiableCredentialClient.StoreCredential(credential_exchange_id);
                    return await Task.FromResult(true);
                default:
                    // TODO log a message $"Received unexpected state {state} for CredentialExchangeState ${state.id}"
                    Console.WriteLine($"Received unexpected state {state} for CredentialExchangeState ${state}");
                    return await Task.FromResult(false);
            }
        }
    }
}
