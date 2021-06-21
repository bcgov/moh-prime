using System;
using System.Threading.Tasks;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface ISubmissionService
    {
        Task SubmitApplicationAsync(int enrolleeId, EnrolleeUpdateModel updatedProfile);
        Task<bool> PerformEnrolleeStatusActionAsync(int enrolleeId, EnrolleeStatusAction action, object additionalParameters = null);
        Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);
        Task ConfirmLatestSubmissionAsync(int enrolleeId);
    }
}
