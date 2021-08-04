using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class CredentialPayload
    {
        [JsonProperty("GPID")]
        public string GPID { get; set; }

        [JsonProperty("Renewal Date")]
        public string RenewalDate { get; set; }

        [JsonProperty("TOA Name")]
        public string TOAName { get; set; }

        [JsonProperty("Care Type Setting")]
        public string CareTypeSetting { get; set; }

        [JsonProperty("Remote User")]
        public string RemoteUser { get; set; }
    }
}
