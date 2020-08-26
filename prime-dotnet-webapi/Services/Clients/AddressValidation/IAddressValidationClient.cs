using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Prime.Services.Clients
{
    public interface IAddressValidationClient
    {
        Task<JObject> Find(string searchTerm, string lastId = null);
        Task<JObject> Retrieve(string Id);
    }
}
