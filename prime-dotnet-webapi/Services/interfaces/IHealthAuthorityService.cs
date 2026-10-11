using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
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
        Task<bool> UpdateCareTypesAsync(int healthAuthorityId, IEnumerable<string> careTypes);
        Task UpdateContactsAsync<T>(int healthAuthorityId, IEnumerable<IContactViewModel> contacts) where T : HealthAuthorityContact, new();
        Task UpdatePrivacyOfficeAsync(int healthAuthorityId, PrivacyOfficeViewModel privacyOffice);
        Task<bool> UpdateVendorsAsync(int healthAuthorityId, IEnumerable<int> vendorCodes);
        Task<bool> UpdateCareTypeVendorsAsync(int healthAuthorityId, IEnumerable<HealthAuthorityCareTypeVendorModel> careTypeVendors);
        Task<bool> ValidateSiteSelectionsAsync(int healthAuthorityId, HealthAuthoritySiteUpdateModel updateModel);
        Task<bool> ValidateSiteSelectionsAsync(int healthAuthorityId, int healthAuthoritySiteId);
        Task<bool> VendorExistsOnHealthAuthorityAsync(int healthAuthorityId, int healthAuthorityVendorId);
        Task<IEnumerable<int>> GetSitesByVendorAsync(int healthAuthorityId, int healthAuthorityVendorId);
        Task<IEnumerable<int>> GetSitesByCareTypeAsync(int healthAuthorityId, int healthAuthorityCareTypeId);
        Task<HealthAuthorityOrganizationAgreementDocument> AddOrReplaceBusinessLicenceDocumentAsync(int healthAuthorityId, Guid documentGuid);
    }
}
