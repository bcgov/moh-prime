using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.Services.Clients
{
    public interface IVerifiableCredentialClient
    {
        Task<JObject> CreateInvitation();
        Task<JObject> IssueCredential(string connectionId);
        Task<string> GetIssuerDID();
        Task<JObject> GetSchema(string schemaIssuerDid);
        Task<JObject> GetCredentialDefinition(string schemaIssuerDid);
    }
}
