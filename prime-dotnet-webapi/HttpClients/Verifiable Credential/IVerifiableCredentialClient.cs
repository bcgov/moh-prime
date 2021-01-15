using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.HttpClients
{
    public interface IVerifiableCredentialClient
    {
        Task<JObject> CreateInvitationAsync(string alias);
        Task<JObject> IssueCredentialAsync(JObject credentialOffer);
        Task<JObject> RevokeCredentialAsync(Credential credential);
        Task<string> GetSchemaId(string did);
        Task<JObject> GetSchema(string schemaId);
        Task<string> GetIssuerDidAsync();
        Task<string> GetCredentialDefinitionIdAsync(string schemaId);
        Task<string> GetRevocationRegistryIdAsync(string credentialDefinitionId);
        Task<JObject> GetPresentationProof(string presentationExchangeId);
    }
}
