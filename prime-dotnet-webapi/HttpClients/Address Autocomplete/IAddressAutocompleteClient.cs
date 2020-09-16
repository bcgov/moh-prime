using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static Prime.HttpClients.AddressAutocompleteClient;

namespace Prime.HttpClients
{
    public interface IAddressAutocompleteClient
    {
        Task<IEnumerable<AddressAutocompleteFindResponse>> Find(string searchTerm, string lastId = null);
        Task<IEnumerable<AddressAutocompleteRetrieveResponse>> Retrieve(string Id);
    }
}
