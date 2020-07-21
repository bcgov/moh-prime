using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Prime.Services.Clients
{
    public interface IVerifiableCredentialClient
    {
        Task<JObject> CreateInvitationAsync(string alias);
        Task<JObject> IssueCredentialAsync(JObject credentialOffer);
        Task<string> GetSchemaId(string did);
        Task<JObject> GetSchema(string schemaId);
        Task<string> GetIssuerDidAsync();
        Task<string> GetCredentialDefinitionIdAsync(string schemaId);
        Task<JObject> GetPresentationProof(string presentationExchangeId);
    }
}
