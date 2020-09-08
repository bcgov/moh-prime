using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Prime.Services.Clients
{
    public class BearerTokenHandler<T> : DelegatingHandler where T : ClientCredentialsTokenRequest
    {
        private readonly IAccessTokenClient _tokenClient;
        private readonly ClientCredentialsTokenRequest _credentials;

        public BearerTokenHandler(IAccessTokenClient accessTokenClient, T credentials)
        {
            _tokenClient = accessTokenClient ?? throw new ArgumentNullException(nameof(accessTokenClient));
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // TODO: we could do access token caching/refreshing/etc. here
            var accessToken = await _tokenClient.GetAccessTokenAsync(_credentials);

            request.SetBearerToken(accessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
