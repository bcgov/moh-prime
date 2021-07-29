using System;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class CredentialResponse
    {
        [JsonProperty("credential_exchange_id")]
        public string ExchangeId { get; set; }
    }
}
