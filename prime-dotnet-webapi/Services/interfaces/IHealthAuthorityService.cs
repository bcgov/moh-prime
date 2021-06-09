using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.Models.HealthAuthorities;

namespace Prime.Services
{
    public interface IHealthAuthorityService
    {
        Task<bool> HealthAuthorityExistsAsync(int healthAuthorityId);
        Task<IEnumerable<HealthAuthorityListViewModel>> GetHealthAuthoritiesAsync();
        Task<HealthAuthorityViewModel> GetHealthAuthorityAsync(int id);
        Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersByHealthAuthorityAsync(HealthAuthorityCode code);
        Task<IEnumerable<HealthAuthorityCode>> GetHealthAuthorityCodesWithUnderReviewAuthorizedUsersAsync();
        Task<int> UpdateCareTypesAsync(int healthAuthorityId, string[] careTypes);
        Task<int> UpdateVendorsAsync(int healthAuthorityId, int[] vendors);
        Task UpdateContacts<T>(int healthAuthorityOrganizationId, IEnumerable<HealthAuthorityContact> contacts) where T : HealthAuthorityContact, new();
    }
}
