using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.ViewModels;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.Models.HealthAuthorities;
using System;

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
        Task<bool> HealthAuthorityVendorExistsOnHealthAuthorityAsync(int healthAuthorityId, int healthAuthorityVendorId);
    }
}
