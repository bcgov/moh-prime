using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prime.Models.VerifiableCredentials
{
    public class SchemaRequest
    {
        public SchemaRequest()
        {
            Attributes = new List<string>();
        }

        [JsonProperty("attributes")]
        public ICollection<string> Attributes { get; }


        [JsonProperty("schema_name")]
        public string SchemaName { get; set; }


        [JsonProperty("schema_version")]
        public string SchemaVersion { get; set; }
    }
}
