using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.SpecialAuthorityTransformation;

namespace Prime.Services
{
    public interface ISatEnrolmentService
    {
        Task<Party> CreateEnrolleeAsync(SatEnrolleeDemographicViewModel enrollee);

        Task<Party> GetEnrolleeAsync(int satId);

        Task UpdateDemographicsAsync(int satId, SatEnrolleeDemographicViewModel viewModel);

        Task UpdateCertificationsAsync(int satId, IEnumerable<SatEnrolleeCertificationViewModel> viewModels);
        Task FinalizeEnrolleeAsync(int enrolleeId);
    }
}
