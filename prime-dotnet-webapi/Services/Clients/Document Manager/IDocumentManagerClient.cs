using System.Threading.Tasks;
using System.Net.Http;

namespace Prime.Services.Clients
{
    public interface IDocumentManagerClient
    {
        Task<HttpResponseMessage> InitializeFileUploadAsync();
    }
}
