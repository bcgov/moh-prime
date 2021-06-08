using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;

namespace Prime.Services
{
    public interface IHealthAuthorityService
    {
        Task<IEnumerable<HealthAuthorityListViewModel>> GetHealthAuthoritiesAsync();
        Task<HealthAuthorityViewModel> GetHealthAuthorityAsync(int id);
        Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersByHealthAuthorityAsync(HealthAuthorityCode code);
        Task<IEnumerable<HealthAuthorityCode>> GetHealthAuthorityCodesWithUnderReviewAuthorizedUsersAsync();
    }
}
