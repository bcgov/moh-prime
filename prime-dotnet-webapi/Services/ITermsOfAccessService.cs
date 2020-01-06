using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ITermsOfAccessService
    {
        Task<bool> SetEnrolleeTermsOfAccessAsync(Enrollee enrollee);

        Task<TermsOfAccess> GetEnrolleeTermsOfAccessAsync(int enrolleeId);
    }
}
