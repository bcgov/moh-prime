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

        /// <summary>
        /// Re-runs the adjudication rules for all Enrollees that:
        /// 1. Are Under Review
        /// 2. Have no assigned Adjudicator
        /// 3. Have one or more Status Reasons relating to the Parmanet College Validation API
        /// </summary>
        Task BulkRerunRulesAsync();
    }
}
