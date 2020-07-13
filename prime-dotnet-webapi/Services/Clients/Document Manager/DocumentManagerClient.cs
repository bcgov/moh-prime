using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public async Task<HttpResponseMessage> InitializeFileUploadAsync(string filename, string fileSize, string destinationFolder)
        {
            _client.DefaultRequestHeaders.Add("Tus-Resumable", "1.0.0");
            _client.DefaultRequestHeaders.Add("Upload-Length", fileSize);

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "filename", filename },
                { "folder", destinationFolder },
            });

            return await _client.PostAsync("documents/uploads", content);
        }

        public async Task<Stream> GetFileAsync(Guid documentGuid)
        {
            var response = await _client.GetAsync($"documents/{documentGuid}");
            return await response.Content.ReadAsStreamAsync();
        }

        public async Task<string> PostFileAsync(Stream document, string filename)
        {
            // Build Uri with query param
            var uriBuilder = new UriBuilder("documents");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["filename"] = filename;
            uriBuilder.Query = query.ToString();

            var content = new StreamContent(document);
            var response = await _client.PostAsync(uriBuilder.Uri, content);
            var documentToken = await response.Content.ReadAsAsync<DocumentToken>();

            return documentToken?.token;
        }

        public async Task<string> CreateDownloadTokenAsync(Guid documentGuid)
        {
            var response = await _client.PostAsync($"documents/{documentGuid}/download-token", null);
            var downloadToken = await response.Content.ReadAsAsync<DownloadToken>();

            return downloadToken?.token;
        }

        private class DownloadToken
        {
            public string token { get; set; }
        }

        private class DocumentToken
        {
            public string token { get; set; }
        }
    }
}
