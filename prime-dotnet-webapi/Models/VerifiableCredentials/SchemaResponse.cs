using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class SchemaResponse
    {
        [JsonProperty("schema_id")]
        public string SchemaId { get; set; }
    }
}
