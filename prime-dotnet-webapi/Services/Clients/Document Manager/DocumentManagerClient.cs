using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Prime.Services.Clients
{
    public class DocumentManagerClient : IDocumentManagerClient
    {
        private readonly HttpClient _client;

        public DocumentManagerClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public Task<HttpResponseMessage> InitializeFileUploadAsync(HttpRequestMessage request)
        {
            var response = _client.SendAsync(request);
            throw new NotImplementedException();
        }

        public async Task<Stream> GetFileAsync(Guid documentGuid)
        {
            var response = await _client.GetAsync($"documents/{documentGuid}");
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
