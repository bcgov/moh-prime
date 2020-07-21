using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Prime.Services.Clients
{
    public class VerifiableCredentialApiKeyHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            System.Console.WriteLine($"request = {JsonConvert.SerializeObject(request)}");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
