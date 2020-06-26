using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.Services.Clients
{
    public interface IVerifiableCredentialClient
    {
        Task<JObject> CreateInvitation();
        Task<JObject> SendCredential(string requestContent);
        Task<JObject> IssueCredential(string credential_exchange_id, JArray attributes);
    }
}
