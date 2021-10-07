using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models.VerifiableCredentials;

namespace Prime.HttpClients
{
    public interface IVerifiableCredentialClient
    {
        Task<ConnectionResponse> CreateInvitationAsync(string alias);
        Task<CredentialResponse> IssueCredentialAsync(CredentialOfferRequest credentialOffer);
        Task<bool> RevokeCredentialAsync(Credential credential);
        Task<string> GetSchemaId(string did);
        Task<string> CreateSchemaAsync();
        Task<string> GetIssuerDidAsync();
        Task<string> GetCredentialDefinitionIdAsync(string schemaId);
        Task<string> CreateCredentialDefinitionAsync(string schemaId);
        Task<bool> DeleteCredentialAsync(Credential credential);
        Task<bool> SendMessageAsync(string connectionId, string content);
    }
}
