using System.Text.Json.Serialization;

namespace Prime.Models.VerifiableCredentials
{
    public class IssuerDidResponse
    {
        [JsonPropertyName("result")]
        public IssuerDidResult Result { get; set; }
    }

    public class IssuerDidResult
    {
        [JsonPropertyName("did")]
        public string Did { get; set; }
    }
}
