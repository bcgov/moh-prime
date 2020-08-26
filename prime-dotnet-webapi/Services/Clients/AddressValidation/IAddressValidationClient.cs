using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static Prime.Services.Clients.AddressValidationClient;

namespace Prime.Services.Clients
{
    public interface IAddressValidationClient
    {
        Task<JObject> Find(string searchTerm, string lastId = null);
        Task<IEnumerable<AddressAutocompleteRetrieveResponse>> Retrieve(string Id);
    }
}
