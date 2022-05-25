
using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface ISiteClaimService
    {
        Task<SiteClaim> GetSiteClaimBySiteIdAsync(int siteId);

        Task<int> CreateCommunitySiteClaimAsync(SiteClaimViewModel claimSite, CommunitySite communitySite, int newOrganizationId);

        Task<SiteClaim> GetSiteClaimAsync(int claimId);

        Task UpdateSiteOrganizationAsync(SiteClaim siteClaim);

        Task DeleteClaimAsync(int claimId);
    }
}
