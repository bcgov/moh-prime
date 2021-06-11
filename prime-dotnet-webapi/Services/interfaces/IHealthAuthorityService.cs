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
        Task<HealthAuthorityViewModel> GetHealthAuthorityAsync(int id);
        Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersByHealthAuthorityAsync(HealthAuthorityCode code);
        Task UpdateCareTypesAsync(int healthAuthorityId, IEnumerable<string> careTypes);
        Task UpdateContactsAsync<T>(int healthAuthorityOrganizationId, IEnumerable<HealthAuthorityContactViewModel> contacts) where T : HealthAuthorityContact, new();
        Task UpdateVendorsAsync(int healthAuthorityId, IEnumerable<int> vendorCodes);
    }
}
