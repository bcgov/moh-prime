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
        /// Returns true if the Enrollee 1) exits 2) was created from a paper submission and 3) is currently "Under Review".
        /// </summary>
        /// <param name="enrolleeId"></param>
        Task<bool> PaperSubmissionIsEditableAsync(int enrolleeId);
        Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel enrollee);
        Task UpdateCareSettingsAsync(int enrolleeId, PaperEnrolleeCareSettingViewModel update);
        Task UpdateDemographicsAsync(int enrolleeId, PaperEnrolleeDemographicViewModel viewModel);
        Task UpdateOboSitesAsync(int enrolleeId, IEnumerable<PaperEnrolleeOboSiteViewModel> viewModels);
        Task UpdateCertificationsAsync(int enrolleeId, IEnumerable<PaperEnrolleeCertificationViewModel> viewModels);
        Task UpdateSelfDeclarationsAsync(int enrolleeId, IEnumerable<PaperEnrolleeSelfDeclarationViewModel> viewModel);
        Task UpdateAgreementAsync(int enrolleeId, PaperEnrolleeAgreementViewModel viewModel);
        Task SetProfileCompletedAsync(int enrolleeId);
        Task FinailizeSubmissionAsync(int enrolleeId);
        Task AddEnrolleeAdjudicationDocumentsAsync(int enrolleeId, int adminId, IEnumerable<Guid> documents);
        Task<IEnumerable<EnrolleeAdjudicationDocument>> GetEnrolleeAdjudicationDocumentsAsync(int enrolleeId);
    }
}
