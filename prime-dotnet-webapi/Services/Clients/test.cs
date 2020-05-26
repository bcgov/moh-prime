using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace Prime.Services.Clients
{
    public class ProtectedApiBearerTokenHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // request the access token
            var accessToken = "";

            // set the bearer token to the outgoing request
            //request.SetBearerToken(accessToken);

            // Proceed calling the inner handler, that will actually send the request
            // to our protected api
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
