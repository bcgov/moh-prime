using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static Prime.Services.Clients.AddressAutocompleteClient;

namespace Prime.Services.Clients
{
    public interface IAddressAutocompleteClient
    {
        Task<IEnumerable<AddressAutocompleteFindResponse>> Find(string searchTerm, string lastId = null);
        Task<IEnumerable<AddressAutocompleteRetrieveResponse>> Retrieve(string Id);
    }
}
