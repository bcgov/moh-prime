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
        Task<V2HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, HealthAuthoritySiteCreateModel createModel);
        Task<IEnumerable<V2HealthAuthoritySiteViewModel>> GetSitesAsync(int? healthAuthorityId = null);
        Task<V2HealthAuthoritySiteViewModel> GetSiteAsync(int siteId);
        Task<IEnumerable<BusinessDayViewModel>> GetBusinessHoursAsync(int siteId);
        Task<IEnumerable<RemoteUserViewModel>> GetRemoteUsersAsync(int siteId);
        Task UpdateSiteAsync(int siteId, HealthAuthoritySiteUpdateModel updateModel);
        Task SetSiteCompletedAsync(int siteId);
        Task SiteSubmissionAsync(int siteId);

        // New Line
        Task<bool> SiteIsEditableAsync(int healthAuthorityId, int siteId);
    }
}
