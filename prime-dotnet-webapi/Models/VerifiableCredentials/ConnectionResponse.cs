using System;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class ConnectionResponse
    {
        [JsonProperty("connection_id")]
        public Guid ConnectionId { get; set; }

        [JsonProperty("invitation_url")]
        public Uri InvitationUrl { get; set; }
    }
}
