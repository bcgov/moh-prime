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
            var invitationResponse = await _verifiableCredentialClient.CreateInvitation();
            var invitation = invitationResponse.GetValue("invitation");
            var invitationURL = invitationResponse.GetValue("invitation_url").ToString();

            if (invitation == null)
            {
                return invitationResponse;
            }

            // QRCodeGenerator qrGenerator = new QRCodeGenerator();
            // QRCodeData qrCodeData = qrGenerator.CreateQrCode(invitationURL, QRCodeGenerator.ECCLevel.Q);
            // Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            // string qrCodeImageAsBase64 = qrCode.GetGraphic(20);

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

        public async Task<bool> Create(JObject data, string topic)
        {
            System.Console.WriteLine($"DATA ${topic}");
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

        private async Task<bool> handleConnection(JObject data)
        {
            var state = data.GetValue("state").ToString();

            if (state == "response")
            {
                var connection_id = data.GetValue("connection_id").ToString();
                Console.WriteLine($"About to issue a credential with this connection_id: " + connection_id);
                // var response = await _verifiableCredentialClient.AcceptRequest(connection_id);

                // Connection successful, issue credential
                // if (response != null)
                // {
                var credResponse = await SendCredential(connection_id);
                // }
            }



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
                    await _verifiableCredentialClient.IssueCredential(credential_exchange_id, attributes);
                    return await Task.FromResult(true);

                case CredentialExchangeStates.Issued:
                    // Store a received credential
                    credential_exchange_id = data.GetValue("credential_exchange_id").ToString();
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
