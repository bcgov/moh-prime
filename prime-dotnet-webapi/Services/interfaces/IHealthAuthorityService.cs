using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models.HealthAuthorities;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IHealthAuthorityService
    {
        Task<bool> HealthAuthorityExistsAsync(int healthAuthorityId);
        Task<IEnumerable<HealthAuthorityListViewModel>> GetHealthAuthoritiesAsync();
        Task<HealthAuthorityViewModel> GetHealthAuthorityAsync(int healthAuthorityId);
        Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersAsync(int healthAuthorityId);
        Task<bool> AuthorizedUserExistsOnHealthAuthorityAsync(int healthAuthorityId, int authorizedUserId);
        Task UpdateCareTypesAsync(int healthAuthorityId, IEnumerable<string> careTypes);
        Task UpdateContactsAsync<T>(int healthAuthorityId, IEnumerable<ContactViewModel> contacts) where T : HealthAuthorityContact, new();
        Task UpdatePrivacyOfficeAsync(int healthAuthorityId, PrivacyOfficeViewModel privacyOffice);
        Task UpdateVendorsAsync(int healthAuthorityId, IEnumerable<int> vendorCodes);
        Task<bool> ValidateSiteSelectionsAsync(int healthAuthorityId, HealthAuthoritySiteUpdateModel updateModel);
        Task<bool> ValidateSiteSelectionsAsync(int healthAuthorityId, int healthAuthoritySiteId);
        Task<bool> VendorExistsOnHealthAuthorityAsync(int healthAuthorityId, int healthAuthorityVendorId);
    }
}
