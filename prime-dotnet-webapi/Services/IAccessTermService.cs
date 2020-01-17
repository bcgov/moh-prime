using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IAccessTermService
    {
        Task SetEnrolleeAccessTermsAsync(Enrollee enrollee);

        Task<AccessTerm> GetEnrolleeAccessTermsAsync(int enrolleeId);
    }
}
