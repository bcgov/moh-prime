using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prime.Services.Clients
{
    public interface IDocumentManagerClient
    {
        Task<HttpResponseMessage> InitializeFileUploadAsync(string filename, string fileSize, string destinationFolder);

        Task<string> CreateDownloadTokenAsync(Guid documentGuid);

        Task<Guid> SendFileAsync(Stream document, string filename, string destinationFolder);

        Task<Stream> GetFileAsync(Guid documentGuid);
    }
}
