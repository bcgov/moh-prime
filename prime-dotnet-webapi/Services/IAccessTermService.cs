using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IAccessTermService
    {
        Task CreateEnrolleeTermsOfAccessAsync(Enrollee enrollee);

        Task SetAcceptedDateForTermsOfAccessAsync(Enrollee enrollee);

        Task<AccessTerm> GetEnrolleeAccessTermsAsync(int enrolleeId);
    }
}
