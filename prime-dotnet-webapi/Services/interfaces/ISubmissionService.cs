using System.Threading.Tasks;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISubmissionService
    {
        Task SubmitApplicationAsync(int enrolleeId, EnrolleeProfileViewModel updatedProfile);

        Task PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action, bool isAdmin);

        Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);
    }
}
