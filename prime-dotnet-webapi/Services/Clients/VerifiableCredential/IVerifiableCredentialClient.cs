using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.Services.Clients
{
    public interface IVerifiableCredentialClient
    {
        Task<JObject> CreateInvitationAsync(string alias);
        Task<JObject> IssueCredentialAsync(JObject credentialOffer);
        Task<JObject> GetSchema(string schemaId);
        Task<string> GetIssuerDidAsync();
        Task<string> GetCredentialDefinitionIdAsync(string schemaId);
    }
}
