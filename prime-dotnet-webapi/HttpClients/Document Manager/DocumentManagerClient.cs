using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.Extensions;

namespace Prime.HttpClients
{
    public class DocumentManagerClient : IDocumentManagerClient
    {
        private readonly HttpClient _client;

        public DocumentManagerClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> InitializeUploadAsync(string filename, string fileSize)
        {
            _client.DefaultRequestHeaders.Add("Tus-Resumable", "1.0.0");
            _client.DefaultRequestHeaders.Add("Upload-Length", fileSize);

            var content = new FileMetadata(filename: filename).AsHttpContent();
            return await _client.PostAsync("documents/uploads", content);
        }

        /// <summary>
        /// Moves a temporary file upload to its final destination and marks it as "submitted".
        /// Returns the file's name if the operation was successful.
        /// </summary>
        public async Task<string> FinalizeUploadAsync(Guid documentGuid, string destinationFolder)
        {
            var content = new FileMetadata(destinationFolder: destinationFolder).AsHttpContent();
            var response = await _client.PostAsync($"documents/uploads/{documentGuid}/submit", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateDownloadTokenAsync(Guid documentGuid)
        {
            var response = await _client.PostAsync($"documents/{documentGuid}/download-token", null);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var downloadToken = await response.Content.ReadAsAsync<DownloadToken>();
            return downloadToken?.Token;
        }

        public async Task<Guid> SendFileAsync(Stream document, string filename, string destinationFolder)
        {
            var url = new FileMetadata(filename, destinationFolder).AsQueryStringUrl("documents");
            var content = new StreamContent(document);

            var response = await _client.PostAsync(url, content);
            var documentResponse = await response.Content.ReadAsAsync<DocumentResponse>();

            return documentResponse?.Document_guid ?? Guid.Empty;
        }

        public async Task<HttpResponseMessage> GetFileAsync(Guid documentGuid)
        {
            return await _client.GetAsync($"documents/{documentGuid}");
        }

        public async Task<Stream> GetFileStreamAsync(Guid documentGuid)
        {
            var response = await GetFileAsync(documentGuid);
            return await response.Content.ReadAsStreamAsync();
        }

        private class FileMetadata
        {
            private readonly Dictionary<string, string> _metadata;

            public FileMetadata(string filename = null, string destinationFolder = null)
            {
                _metadata = new Dictionary<string, string>
                {
                    { "filename", filename },
                    { "folder", destinationFolder }
                }
                .RemoveNullValues();
            }

            public HttpContent AsHttpContent()
            {
                return new FormUrlEncodedContent(_metadata);
            }

            public string AsQueryStringUrl(string baseUrl)
            {
                return _metadata.ToQueryStringUrl(baseUrl, false);
            }
        }

        private class DownloadToken
        {
            public string Token { get; set; }
        }

        private class DocumentResponse
        {
            public Guid Document_guid { get; set; }
        }
    }
}
