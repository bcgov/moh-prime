using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Services
{
    public interface IEnrolleePaperSubmissionService
    {
        Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel enrollee);
        Task UpdateEnrolleeCareSettingsById(int enrolleeId, PaperEnrolleeCareSettingViewModel update);
        Task UpdateEnrolleeDemographicsById(int enrolleeId, PaperEnrolleeDemographicViewModel updateModel);
        Task UpdateEnrolleeOboSitesById(int enrolleeId, PaperEnrolleeOboSiteViewModel updateModel);
        Task UpdateEnrolleeCertificationsById(int enrolleeId, PaperEnrolleeCertificationViewModel updateModel);
        Task UpdateEnrolleeSelfDeclarationsById(int enrolleeId, PaperEnrolleeSelfDeclarationViewModel updateModel);
        Task UpdateEnrolleeAgreementsById(int enrolleeId, PaperEnrolleeAgreementViewModel updateModel);
    }
}
