using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.HttpClients
{
    public interface ILdapClient
    {
        Task<JObject> GetUserAsync(string userId, string password);
    }
}
