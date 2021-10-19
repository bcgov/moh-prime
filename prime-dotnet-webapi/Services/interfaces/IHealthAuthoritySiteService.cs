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
        Task<BusinessHoursViewModel> GetBusinessHours(int siteId);
        Task<RemoteUsersViewModel> GetRemoteUsers(int siteId);
        Task UpdateSiteAsync(int healthAuthorityId, int siteId, HealthAuthorityUpdateViewModel updateModel);
        Task SetSiteCompletedAsync(int siteId);
        Task SiteSubmissionAsync(int siteId);
    }
}
