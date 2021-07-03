using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Services
{
    public interface IHealthAuthoritySiteService
    {
        Task<bool> SiteExistsAsync(int siteId);
        Task<HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, int vendorCode);
        Task<IEnumerable<HealthAuthoritySiteViewModel>> GetSitesAsync(int healthAuthorityId);
        Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId);
        Task UpdateVendorAsync(int siteId, int vendorCode);
        // Task UpdateSiteInfoAsync(int siteId, HealthAuthoritySiteInfoViewModel viewModel);
        // Task UpdateCareTypeAsync(int siteId, int healthAuthorityCareTypeId);
        // Task UpdatePhysicalAddressAsync(int siteId, PhysicalAddress physicalAddress);
        // Task UpdateHoursOperationAsync(int siteId, HealthAuthoritySiteHoursOperationViewModel viewModel);
        // Task UpdateRemoteUsersAsync(int siteId, HealthAuthoritySiteRemoteUsersViewModel viewModel);
        // Task UpdateAdministratorAsync(int siteId, HealthAuthoritySiteAdministratorViewModel viewModel);
        Task SetSiteCompletedAsync(int siteId);
        Task FinalizeSubmissionAsync(int siteId);
    }
}
