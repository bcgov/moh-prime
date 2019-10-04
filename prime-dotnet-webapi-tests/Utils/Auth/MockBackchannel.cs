using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeTests.Utils.Auth
{
    public class MockBackchannel : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsoluteUri.Equals("https://inmemory.microsoft.com/common/.well-known/openid-configuration"))
            {
                return await EmbeddedResourceReader.GetOpenIdConfigurationAsResponseMessage("microsoft-openid-config.json");
            }
            if (request.RequestUri.AbsoluteUri.Equals("https://inmemory.microsoft.com/common/discovery/keys"))
            {
                return await EmbeddedResourceReader.GetOpenIdConfigurationAsResponseMessage("microsoft-wellknown-keys.json");
            }

            throw new NotImplementedException();
        }
    }
}