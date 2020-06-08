using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Prime.Services.Clients
{
    public class AccessTokenClient : IAccessTokenClient
    {
        private readonly HttpClient _client;

        public AccessTokenClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> GetAccessTokenAsync(ClientCredentialsTokenRequest request)
        {
            var response = await _client.RequestClientCredentialsTokenAsync(request);
            return response.AccessToken;
        }
    }
}
