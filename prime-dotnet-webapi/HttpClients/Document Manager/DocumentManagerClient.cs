using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Flurl;

using Prime.Extensions;
using Prime.HttpClients.DocumentManagerApiDefinitions;

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

            return await _client.PostAsync("documents/uploads", FileMetadataContent(filename: filename));
        }

        public async Task<string> FinalizeUploadAsync(Guid documentGuid, string destinationFolder)
        {
            var response = await _client.PostAsync($"documents/uploads/{documentGuid}/submit", FileMetadataContent(destinationFolder: destinationFolder));
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
            var url = "documents".SetQueryParams(new
            {
                filename,
                folder = destinationFolder
            });

            var response = await _client.PostAsync(url, new StreamContent(document));
            var documentResponse = await response.Content.ReadAsAsync<DocumentGuidResponse>();

            return documentResponse?.Document_guid ?? Guid.Empty;
        }

        public async Task<HttpContent> GetDocumentAsync(Guid documentGuid)
        {
            var response = await _client.GetAsync($"documents/{documentGuid}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return response.Content;
        }

        private HttpContent FileMetadataContent(string filename = null, string destinationFolder = null)
        {
            var metadata = new Dictionary<string, string>
            {
                { "filename", filename },
                { "folder", destinationFolder }
            }
            .RemoveNullValues();

            return new FormUrlEncodedContent(metadata);
        }
    }
}
