using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetSitesAsync(int partyId);
        Task<Site> GetSiteAsync(int siteId);
        Task<int> CreateSiteAsync(Site site);
        Task<int> UpdateSiteAsync(int siteId, Site site, bool isCompleted = false);
        Task DeleteSiteAsync(int siteId);
        Task<Site> GetSiteNoTrackingAsync(int siteId);
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId);
        Task AcceptCurrentOrganizationAgreementAsync(int signingAuthorityId);
    }
}
