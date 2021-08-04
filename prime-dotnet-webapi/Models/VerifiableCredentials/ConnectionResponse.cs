using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class ConnectionResponse
    {
        [JsonProperty("connection_id")]
        public string ConnectionId { get; set; }

        [JsonProperty("invitation_url")]
        public System.Uri InvitationUrl { get; set; }
    }
}
