using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISubmissionService
    {
        Task<Enrollee> PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action);

        Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);
    }
}
