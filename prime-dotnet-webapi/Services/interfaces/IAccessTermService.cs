using System.Threading.Tasks;
using Prime.Models;
using System.Collections.Generic;

namespace Prime.Services
{
    public interface IAccessTermService
    {

        Task<AccessTerm> GetMostRecentNotAcceptedEnrolleesAccessTermAsync(int enrolleeId);

        Task<AccessTerm> GetMostRecentAcceptedEnrolleesAccessTermAsync(int enrolleeId);

        Task<AccessTerm> GetEnrolleesAccessTermAsync(int enrolleeId, int accessTermId);

        Task<IEnumerable<AccessTerm>> GetAcceptedAccessTerms(int enrolleeId);

        Task CreateEnrolleeAccessTermAsync(Enrollee enrollee);

        Task AcceptCurrentAccessTermAsync(Enrollee enrollee);

        Task<bool> AccessTermExistsAsync(int accessTermId);
    }
}
