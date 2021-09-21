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
        Task<IEnumerable<HealthAuthoritySiteViewModel>> GetAllSitesAsync();
        Task<IEnumerable<HealthAuthoritySiteViewModel>> GetSitesAsync(int healthAuthorityId);
        Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId);
        // TODO should we use a relationship but issues around deletion
        // Task UpdateVendorAsync(int siteId, int healthAuthorityVendorId);
        Task UpdateVendorAsync(int siteId, int vendorCode);
        Task UpdateSiteInfoAsync(int siteId, HealthAuthoritySiteInfoViewModel viewModel);
        // TODO should we use a relationship but issues around deletion
        // Task UpdateCareTypeAsync(int siteId, int healthAuthorityCareTypeId);
        Task UpdateCareTypeAsync(int siteId, string careType);
        Task UpdatePhysicalAddressAsync(int siteId, AddressViewModel physicalAddress);
        Task UpdateHoursOperationAsync(int siteId, ICollection<BusinessDay> businessHours);
        Task UpdateRemoteUsersAsync(int siteId, ICollection<RemoteUser> remoteUsers);
        Task UpdatePharmanetAdministratorAsync(int siteId, int healthAuthoritySiteAdministratorId);
        Task UpdatePecAsync(int siteId, HealthAuthoritySitePecViewModel viewModel);
        Task UpdateTechnicalSupportAsync(int siteId, int technicalSupportId);
        Task SetSiteCompletedAsync(int siteId);
        Task SiteSubmissionAsync(int siteId);
        Task<HealthAuthoritySiteViewModel> ApproveSiteAsync(int siteId);
        Task<HealthAuthoritySiteViewModel> DeclineSiteAsync(int siteId);
        Task CreateSiteNoteAsync(int siteId, string note, int adminId);
    }
}
