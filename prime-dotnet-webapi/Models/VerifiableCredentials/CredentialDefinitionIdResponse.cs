using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class CredentialDefinitionIdResponse
    {
        public CredentialDefinitionIdResponse()
        {
            this.CredentialDefinitionIds = new List<string>();
        }

        [JsonProperty("credential_definition_ids")]
        public ICollection<string> CredentialDefinitionIds { get; set; }
    }
}
