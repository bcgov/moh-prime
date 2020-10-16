using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;

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

            var content = FileMetadata.AsHttpContent(filename: filename);
            return await _client.PostAsync("documents/uploads", content);
        }

        /// <summary>
        /// Moves a temporary file upload to its final destination and marks it as "submitted".
        /// Returns the file's name if the operation was successful.
        /// </summary>
        public async Task<string> FinalizeUploadAsync(Guid documentGuid, string destinationFolder)
        {
            var content = FileMetadata.AsHttpContent(destinationFolder: destinationFolder);
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
            return downloadToken?.token;
        }

        public async Task<Guid> SendFileAsync(Stream document, string filename, string destinationFolder)
        {
            var url = FileMetadata.AsQueryStringUrl("documents", filename, destinationFolder);
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

        private static class FileMetadata
        {
            public static HttpContent AsHttpContent(string filename = null, string destinationFolder = null)
            {
                return new FormUrlEncodedContent(ToDictionary(filename, destinationFolder));
            }

            public static string AsQueryStringUrl(string baseUrl, string filename, string destinationFolder)
            {
                return QueryHelpers.AddQueryString(baseUrl, ToDictionary(filename, destinationFolder));
            }

            private static Dictionary<string, string> ToDictionary(string filename, string destinationFolder)
            {
                var dict = new Dictionary<string, string>();

                if (!string.IsNullOrWhiteSpace(filename))
                {
                    dict.Add("filename", filename);
                }
                if (!string.IsNullOrWhiteSpace(destinationFolder))
                {
                    dict.Add("folder", destinationFolder);
                }

                return dict;
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
