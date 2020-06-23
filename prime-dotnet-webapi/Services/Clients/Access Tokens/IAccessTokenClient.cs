using System.Threading.Tasks;
using IdentityModel.Client;

namespace Prime.Services.Clients
{
    public interface IAccessTokenClient
    {
        Task<string> GetAccessTokenAsync(ClientCredentialsTokenRequest request);
    }
}
