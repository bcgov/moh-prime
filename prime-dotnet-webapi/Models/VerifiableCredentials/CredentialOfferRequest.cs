using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    /// <summary>
    /// A credential offer to send to aries agent.
    /// </summary>
    public class CredentialOfferRequest
    {
        /// <summary>
        /// Gets or sets the connectionId.
        /// </summary>
        [JsonProperty("connection_id")]
        public string ConnectionId { get; set; }

        [JsonProperty("issuer_did")]
        public string IssuerDid { get; set; }

        [JsonProperty("schema_id")]
        public string SchemaId { get; set; }

        [JsonProperty("schema_issuer_did")]
        public string SchemaIssuerDid { get; set; }

        [JsonProperty("schema_name")]
        public string SchemaName { get; set; }

        [JsonProperty("schema_version")]
        public string SchemaVersion { get; set; }

        [JsonProperty("cred_def_id")]
        public string CredentialDefinitionId { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("auto_remove")]
        public bool AutoRemove { get; } = false;

        [JsonProperty("trace")]
        public bool Trace { get; } = false;

        [JsonProperty("credential_proposal")]
        public CredentialProposal CredentialProposal { get; set; }
    }

    public class CredentialProposal
    {
        public CredentialProposal()
        {
            Attributes = new List<CredentialAttribute>();
        }

        [JsonProperty("@type")]
        public string Type { get; } = "issue-credential/1.0/credential-preview";

        [JsonProperty("attributes")]
        public ICollection<CredentialAttribute> Attributes { get; }
    }

    public class CredentialAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
