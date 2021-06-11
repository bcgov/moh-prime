using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;

namespace Prime.Services
{
    public interface IHealthAuthorityService
    {
        Task<bool> HealthAuthorityExistsAsync(int healthAuthorityId);
        Task<IEnumerable<HealthAuthorityListViewModel>> GetHealthAuthoritiesAsync();
        Task<HealthAuthorityViewModel> GetHealthAuthorityAsync(int healthAuthorityId);
        Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersAsync(int healthAuthorityId);
        Task UpdateCareTypesAsync(int healthAuthorityId, IEnumerable<string> careTypes);
        Task UpdateContactsAsync<T>(int healthAuthorityId, IEnumerable<HealthAuthorityContactViewModel> contacts) where T : HealthAuthorityContact, new();
        Task UpdateVendorsAsync(int healthAuthorityId, IEnumerable<int> vendorCodes);
    }
}
