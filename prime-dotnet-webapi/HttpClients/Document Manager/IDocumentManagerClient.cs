using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prime.HttpClients
{
    public interface IDocumentManagerClient
    {
        Task<HttpResponseMessage> InitializeUploadAsync(string filename, string fileSize);

        Task<string> FinalizeUploadAsync(Guid documentGuid, string destinationFolder);

        Task<string> CreateDownloadTokenAsync(Guid documentGuid);

        Task<Guid> SendFileAsync(Stream document, string filename, string destinationFolder);

        Task<HttpResponseMessage> GetFileResponseAsync(Guid documentGuid);

        Task<byte[]> GetFileAsync(Guid documentGuid);
    }
}
