using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Prime.HttpClients
{
    public interface ILdapClient
    {
        Task<JObject> GetUserAsync(string userId, string password);
    }
}
