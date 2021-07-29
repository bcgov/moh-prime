using System;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class WebhookData
    {
        [JsonProperty("credential_exchange_id")]
        public Guid CredentialExchangeId { get; set; }

        public string State { get; set; }

        public int Alias { get; set; }


        [JsonProperty("connection_id")]
        public string ConnectionId { get; set; }

        [JsonProperty("revoc_reg_id")]
        public string RevocationRegistryId { get; set; }

        [JsonProperty("revocation_id")]
        public string RevocationId { get; set; }
    }
}
