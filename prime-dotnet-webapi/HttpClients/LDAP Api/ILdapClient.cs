using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Prime.HttpClients
{
    public interface ILdapClient
    {
        Task<string> GetUserAsync(string userId, string password);
    }
}
