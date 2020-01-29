using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IAccessTermService
    {
        Task<AccessTerm> GetAccessTermAsync(Enrollee enrollee);

        Task<AccessTerm> GetEnrolleeAccessTermsAsync(int enrolleeId);

        Task CreateEnrolleeAccessTermAsync(Enrollee enrollee);

        Task AcceptCurrentAccessTermAsync(Enrollee enrollee);
    }
}
