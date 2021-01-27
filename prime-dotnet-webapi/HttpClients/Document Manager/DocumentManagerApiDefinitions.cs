using System;
using System.Net.Http;
using System.Collections.Generic;
using Prime.Extensions;

namespace Prime.HttpClients.DocumentManagerApiDefinitions
{
    public class FileMetadata
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

    public class DownloadToken
    {
        public string Token { get; set; }
    }

    public class DocumentResponse
    {
        public Guid Document_guid { get; set; }
    }
}
