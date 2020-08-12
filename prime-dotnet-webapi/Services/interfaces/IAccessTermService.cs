using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface IAccessTermService
    {
        Task<AccessTerm> GetEnrolleeAccessTermAsync(int enrolleeId, int accessTermId, bool includeText = false);

        Task<IEnumerable<AccessTerm>> GetAccessTermsAsync(int enrolleeId, AccessTermFilters filters);

        Task CreateEnrolleeAccessTermAsync(Enrollee enrollee);

        Task AcceptCurrentAccessTermAsync(int enrolleeId);

        Task ExpireCurrentAccessTermAsync(int enrolleeId);
    }
}
