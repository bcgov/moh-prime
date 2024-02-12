using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.Models.Api;

namespace Prime.Services
{
    public interface IHealthAuthoritySiteService
    {
        Task<PermissionsRecord> GetPermissionsRecordAsync(int siteId);
        Task<bool> SiteExistsAsync(int healthAuthorityId, int siteId);
        Task<HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, HealthAuthoritySiteCreateModel createModel);
        Task<IEnumerable<HealthAuthoritySiteAdminListViewModel>> GetSitesAsync(int? healthAuthorityId = null, int? healthAuthoritySiteId = null);
        Task<IEnumerable<HealthAuthoritySiteAdminListViewModel>> GetSitesAsync(HealthAuthoritySiteSearchOptions searchOptions);
        Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId);
        Task<HealthAuthoritySiteAdminViewModel> GetAdminSiteAsync(int siteId);
        Task UpdateSiteAsync(int siteId, HealthAuthoritySiteUpdateModel updateModel);
        Task SetSiteCompletedAsync(int siteId);
        Task SiteSubmissionAsync(int siteId);
        Task<bool> SiteIsEditableAsync(int healthAuthorityId, int siteId);
    }
}
