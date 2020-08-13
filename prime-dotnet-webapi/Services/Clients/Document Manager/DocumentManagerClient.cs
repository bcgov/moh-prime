using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

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

            var content = UploadMetadata.AsHttpContent(filename, destinationFolder);
            return await _client.PostAsync("documents/uploads", content);
        }

        public async Task<string> CreateDownloadTokenAsync(Guid documentGuid)
        {
            var response = await _client.PostAsync($"documents/{documentGuid}/download-token", null);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var downloadToken = await response.Content.ReadAsAsync<DownloadToken>();
            return downloadToken?.token;
        }

        public async Task<Guid> SendFileAsync(Stream document, string filename, string destinationFolder)
        {
            var url = UploadMetadata.AsQueryStringUrl("documents", filename, destinationFolder);
            var content = new StreamContent(document);

            var response = await _client.PostAsync(url, content);
            var documentResponse = await response.Content.ReadAsAsync<DocumentResponse>();

            return documentResponse?.document_guid ?? Guid.Empty;
        }

        public async Task<HttpResponseMessage> GetFileAsync(Guid documentGuid)
        {
            return await _client.GetAsync($"documents/{documentGuid}");
        }

        public async Task<Stream> GetFileStreamAsync(Guid documentGuid)
        {
            var response = await this.GetFileAsync(documentGuid);
            return await response.Content.ReadAsStreamAsync();
        }

        private static class UploadMetadata
        {
            public static HttpContent AsHttpContent(string filename, string destinationFolder)
            {
                return new FormUrlEncodedContent(ToDictionary(filename, destinationFolder));
            }

            public static string AsQueryStringUrl(string baseUrl, string filename, string destinationFolder)
            {
                return QueryHelpers.AddQueryString(baseUrl, ToDictionary(filename, destinationFolder));
            }

            private static Dictionary<string, string> ToDictionary(string filename, string destinationFolder)
            {
                return new Dictionary<string, string>()
                {
                    { "filename", filename },
                    { "folder", destinationFolder },
                };
            }
        }

        private class DownloadToken
        {
            public string token { get; set; }
        }

        private class DocumentResponse
        {
            public Guid document_guid { get; set; }
        }
    }
}
