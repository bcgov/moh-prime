using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prime.Models.Api;

namespace Prime.HttpClients
{
    public interface ILdapClient
    {
        Task<LdapResponseKeys> GetUserAsync(string userId, string password);
    }
}
