using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prime.Services.Clients
{
    public interface IDocumentManagerClient
    {
        Task<HttpResponseMessage> InitializeFileUploadAsync(HttpRequestMessage request);

        Task<Stream> GetFileAsync(Guid documentGuid);
    }
}
