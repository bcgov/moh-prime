using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prime.Services.Clients
{
    public class DocumentManagerClient : IDocumentManagerClient
    {
        private readonly HttpClient _client;

        public DocumentManagerClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> InitializeFileUploadAsync()
        {
            await _client.GetAsync("");
            return (HttpResponseMessage)null;
        }
    }
}
