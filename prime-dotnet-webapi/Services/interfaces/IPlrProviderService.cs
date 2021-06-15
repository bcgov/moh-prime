using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPlrProviderService
    {
        public Task<int> CreateOrUpdatePlrProviderAsync(PlrProvider dataObject, bool expectExists = false);
    }
}
