using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Sites;

namespace Prime.Services
{
    public interface IHealthAuthoritySiteService
    {
        Task<bool> SiteExistsAsync(int healthAuthorityId, int siteId);
        Task<HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, int vendorCode);
        Task<IEnumerable<HealthAuthoritySiteViewModel>> GetAllSitesAsync();
        Task<IEnumerable<V2HealthAuthoritySiteViewModel>> GetSitesAsync(int healthAuthorityId);
        Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId);
        Task<IEnumerable<BusinessDayViewModel>> GetBusinessHours(int siteId);
        Task<IEnumerable<RemoteUserViewModel>> GetRemoteUsers(int siteId);
        Task UpdateSiteAsync(int healthAuthorityId, int siteId, HealthAuthoritySiteUpdateModel updateModel);
        Task SetSiteCompletedAsync(int siteId);
        Task SiteSubmissionAsync(int siteId);

        // New Line
        Task<bool> SiteIsEditableAsync(int healthAuthorityId, int siteId);
    }
}
