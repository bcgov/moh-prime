using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ITermsOfAccessService
    {
        Task SetEnrolleeTermsOfAccessAsync(Enrollee enrollee);

        Task<TermsOfAccess> GetEnrolleeTermsOfAccessAsync(int enrolleeId);
    }
}
