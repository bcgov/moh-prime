using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISubmissionService
    {
        Task PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action, bool isAdmin);

        Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);
    }
}
