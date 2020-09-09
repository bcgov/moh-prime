using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prime.HttpClients
{
    public interface IDocumentManagerClient
    {
        Task<HttpResponseMessage> InitializeFileUploadAsync(string filename, string fileSize, string destinationFolder);

        Task<string> CreateDownloadTokenAsync(Guid documentGuid);

        Task<Guid> SendFileAsync(Stream document, string filename, string destinationFolder);

        Task<HttpResponseMessage> GetFileAsync(Guid documentGuid);

        Task<Stream> GetFileStreamAsync(Guid documentGuid);
    }
}
