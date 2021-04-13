using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPlrProviderService
    {
        // public Task<int> CreateOrUpdatePlrProviderAsync(PlrProvider dataObject);

        public int CreateOrUpdatePlrProvider(PlrProvider dataObject, bool expectExists = false);
    }
}
