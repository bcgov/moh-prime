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

        Task AcceptCurrentAccessTermAsync(Enrollee enrollee);

        Task ExpireCurrentAccessTermAsync(Enrollee enrollee);

        Task<bool> AccessTermExistsOnEnrolleeAsync(int accessTermId, int enrolleeId);

        Task<bool> IsCurrentByEnrolleeAsync(Enrollee enrollee);

        Task<string> GetCurrentTOAStatusAsync(Enrollee enrollee);
    }
}
