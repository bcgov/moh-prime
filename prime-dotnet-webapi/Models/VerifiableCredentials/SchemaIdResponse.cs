using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class SchemaIdResponse
    {
        [JsonProperty("schema_ids")]
        public ICollection<string> SchemaIds { get; set; } = new List<string>();
    }
}
