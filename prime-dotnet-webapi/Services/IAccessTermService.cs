using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IAccessTermService
    {
        Task CreateEnrolleeAccessTermAsync(Enrollee enrollee);

        Task SetAcceptedDateForTermsOfAccessAsync(Enrollee enrollee);

        Task<AccessTerm> GetEnrolleeAccessTermsAsync(int enrolleeId);
    }
}
