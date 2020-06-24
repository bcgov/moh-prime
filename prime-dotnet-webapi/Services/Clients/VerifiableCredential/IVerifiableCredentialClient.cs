using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.Services.Clients
{
    public interface IVerifiableCredentialClient
    {
        Task<JObject> CreateInvitation();
        Task<JObject> ReceiveInvitation(string invitation);
        Task<JObject> AcceptInvitation(string connection_id);
        Task<JObject> AcceptRequest(string connection_id);
        Task<JObject> SendCredential(string requestContent);
        Task<JObject> IssueCredential(string credential_exchange_id, JArray attributes);
        Task<JObject> StoreCredential(string credential_exchange_id);
    }
}
