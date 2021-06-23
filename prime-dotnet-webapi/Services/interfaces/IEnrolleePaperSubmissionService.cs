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
        Task UpdateEnrolleeCareSettingsById(int enrolleeId, PaperEnrolleeCareSettingViewModel update);
        Task UpdateEnrolleeDemographicsById(int enrolleeId, PaperEnrolleeDemographicViewModel updateModel);
        Task UpdateEnrolleeOboSitesById(int enrolleeId, PaperEnrolleeOboSiteViewModel updateModel);
        Task UpdateEnrolleeCertificationsById(int enrolleeId, ICollection<PaperEnrolleeCertificationViewModel> updateModel);
        Task UpdateEnrolleeSelfDeclarationsById(int enrolleeId, PaperEnrolleeSelfDeclarationViewModel updateModel);
        Task UpdateEnrolleeAgreementsById(int enrolleeId, PaperEnrolleeAgreementViewModel updateModel);
    }
}
