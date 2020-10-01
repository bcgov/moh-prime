using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface IAccessTermService
    {
        Task<Agreement> GetEnrolleeAccessTermAsync(int enrolleeId, int accessTermId, bool includeText = false);

        Task<IEnumerable<Agreement>> GetAccessTermsAsync(int enrolleeId, AccessTermFilters filters);

        Task CreateEnrolleeAccessTermAsync(int enrolleeId);

        Task AcceptCurrentAccessTermAsync(int enrolleeId);

        Task ExpireCurrentAccessTermAsync(int enrolleeId);
    }
}
