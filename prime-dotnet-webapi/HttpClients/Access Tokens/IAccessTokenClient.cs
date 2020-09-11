using System.Threading.Tasks;
using IdentityModel.Client;

namespace Prime.HttpClients
{
    public interface IAccessTokenClient
    {
        Task<string> GetAccessTokenAsync(ClientCredentialsTokenRequest request);
    }
}
