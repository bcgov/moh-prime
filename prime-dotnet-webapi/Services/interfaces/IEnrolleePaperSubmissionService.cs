using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Services
{
    public interface IEnrolleePaperSubmissionService
    {
        /// <summary>
        /// Returns true if the Enrollee both 1) exits and 2) was created from a paper submission.
        /// </summary>
        /// <param name="enrolleeId"></param>
        Task<bool> PaperSubmissionExistsAsync(int enrolleeId);
        Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel enrollee);
        Task UpdateCareSettingsAsync(int enrolleeId, PaperEnrolleeCareSettingViewModel update);
        Task UpdateDemographicsAsync(int enrolleeId, PaperEnrolleeDemographicViewModel updateModel);
        Task UpdateOboSitesAsync(int enrolleeId, PaperEnrolleeOboSiteViewModel updateModel);
        Task UpdateCertificationsAsync(int enrolleeId, ICollection<PaperEnrolleeCertificationsViewModel> updateModels);
        Task UpdateSelfDeclarationsAsync(int enrolleeId, PaperEnrolleeSelfDeclarationViewModel updateModel);
        Task UpdateAgreementsAsync(int enrolleeId, PaperEnrolleeAgreementViewModel updateModel);
    }
}
