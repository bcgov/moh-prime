using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.ViewModels.HealthAuthorities;
using Prime.Models.HealthAuthorities;

namespace Prime.Services
{
    public interface IHealthAuthoritySiteService
    {
        Task<bool> SiteExistsAsync(int siteId);
        Task<IEnumerable<HealthAuthoritySite>> GetSitesAsync(int healthAuthorityId);
        Task<HealthAuthoritySite> GetSiteAsync(int siteId);
        Task UpdateCareTypeAsync(int siteId, string careType);
        Task UpdateVendorAsync(int siteId, int healthAuthorityId, int vendorCode);
    }
}
