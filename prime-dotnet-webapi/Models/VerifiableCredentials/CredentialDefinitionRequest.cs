using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class CredentialDefinitionRequest
    {
        [JsonProperty("schema_id")]
        public string SchemaId { get; set; }

        [JsonProperty("support_revocation")]
        public bool SupportRevocation { get; } = true;

        [JsonProperty("tag")]
        public string Tag { get; set; }
    }

    public class CredentialDefinitionResponse
    {
        [JsonProperty("credential_definition_id")]
        public string CredentialDefinitionId { get; set; }
    }
}
