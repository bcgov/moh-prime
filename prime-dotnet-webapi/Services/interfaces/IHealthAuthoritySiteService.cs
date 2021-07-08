using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Services
{
    public interface IHealthAuthoritySiteService
    {
        Task<bool> SiteExistsAsync(int healthAuthorityId, int siteId);
        Task<HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, int vendorCode);
        Task<IEnumerable<HealthAuthoritySiteViewModel>> GetSitesAsync(int healthAuthorityId);
        Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId);
        // TODO should we use a relationship or direct data type?
        // Task UpdateVendorAsync(int siteId, int healthAuthorityVendorId);
        Task UpdateVendorAsync(int siteId, int vendorCode);
        Task UpdateSiteInfoAsync(int siteId, HealthAuthoritySiteInfoViewModel viewModel);
        // TODO should we use a relationship or direct data type?
        // Task UpdateCareTypeAsync(int siteId, int healthAuthorityCareTypeId);
        Task UpdateCareTypeAsync(int siteId, string careType);
        Task UpdatePhysicalAddressAsync(int siteId, AddressViewModel physicalAddress);
        Task UpdateHoursOperationAsync(int siteId, ICollection<BusinessDay> businessHours);
        Task UpdateRemoteUsersAsync(int siteId, ICollection<RemoteUser> remoteUsers);
        // TODO should we use a relationship or direct data type?
        // Task UpdateAdministratorAsync(int siteId, int healthAuthoritySiteAdministratorId);
        // Task UpdateAdministratorAsync(int siteId, int HealthAuthoritySiteAdministratorViewModel);

        Task SetSiteCompletedAsync(int siteId);
        Task FinalizeSubmissionAsync(int siteId);
    }
}
