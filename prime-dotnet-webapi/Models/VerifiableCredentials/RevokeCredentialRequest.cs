using System;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class RevokeCredentialRequest
    {

        [JsonProperty("cred_ex_id")]
        public Guid CredentialExchangeId { get; set; }


        [JsonProperty("publish")]
        public bool Publish { get; } = true;
    }
}
