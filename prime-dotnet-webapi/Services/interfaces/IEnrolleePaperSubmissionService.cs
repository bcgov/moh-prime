using System;
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
        Task UpdateDemographicsAsync(int enrolleeId, PaperEnrolleeDemographicViewModel viewModel);
        Task UpdateOboSitesAsync(int enrolleeId, IEnumerable<PaperEnrolleeOboSiteViewModel> viewModels);
        Task UpdateCertificationsAsync(int enrolleeId, IEnumerable<PaperEnrolleeCertificationViewModel> viewModels);
        Task UpdateSelfDeclarationsAsync(int enrolleeId, IEnumerable<PaperEnrolleeSelfDeclarationViewModel> viewModel);
        Task UpdateAgreementsAsync(int enrolleeId, PaperEnrolleeAgreementViewModel viewModel);
        Task FinailizeSubmissionAsync(int enrolleeId);
        Task AddEnrolleeAdjudicationDocumentsAsync(int enrolleeId, int adminId, IEnumerable<Guid> documents);
        Task<IEnumerable<EnrolleeAdjudicationDocument>> GetEnrolleeAdjudicationDocumentsAsync(int enrolleeId);
    }
}
