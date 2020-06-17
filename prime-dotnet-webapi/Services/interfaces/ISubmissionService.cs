using System.Threading.Tasks;
using Prime.ViewModels;
using Prime.Models.Api;
using Prime.Models;

namespace Prime.Services
{
    public interface ISubmissionService
    {
        Task SubmitApplicationAsync(int enrolleeId, EnrolleeProfileViewModel enrolleProfile);

        Task PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action, bool isAdmin);

        Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);

        Task UpdateNonCompliantGPIDs();
    }
}
