using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prime.Services.Clients
{
    public interface IDocumentManagerClient
    {
        Task<HttpResponseMessage> InitializeFileUploadAsync(string filename, string fileSize, string destinationFolder);

        Task<Stream> GetFileAsync(Guid documentGuid);

        Task<string> CreateDownloadTokenAsync(Guid documentGuid);
    }
}
