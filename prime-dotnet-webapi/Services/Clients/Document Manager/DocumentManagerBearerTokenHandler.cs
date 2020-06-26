using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Prime.Services.Clients
{
    public class DocumentManagerBearerTokenHandler : DelegatingHandler
    {
        private readonly IAccessTokenClient _tokenClient;
        private readonly DocumentManagerClientCredentials _credentials;

        public DocumentManagerBearerTokenHandler(IAccessTokenClient accessTokenClient, DocumentManagerClientCredentials credentials)
        {
            _tokenClient = accessTokenClient ?? throw new ArgumentNullException(nameof(accessTokenClient));
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // TODO: we could do access token caching/refreshing/etc. here
            var accessToken = await _tokenClient.GetAccessTokenAsync(_credentials);

            // TODO: Remove
            System.Console.WriteLine("-----------  DEBUG   ------------");
            System.Console.WriteLine($"access Token:{accessToken}");

            request.SetBearerToken(accessToken);

            System.Console.WriteLine("-----------  DEBUG   ------------");
            System.Console.WriteLine($"request:{request.ToString()}");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
