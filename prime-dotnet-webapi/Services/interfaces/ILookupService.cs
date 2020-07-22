using System.Threading.Tasks;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ILookupService
    {
        Task<LookupEntity> GetLookupsAsync();
    }
}
