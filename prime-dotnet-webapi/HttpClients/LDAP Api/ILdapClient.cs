using System.Threading.Tasks;
using Prime.Models.Api;

namespace Prime.HttpClients
{
    public interface ILdapClient
    {
        Task<GisUserRepresentation> GetUserAsync(string userId, string password);
    }
}
