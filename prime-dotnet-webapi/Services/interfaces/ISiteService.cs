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
        Task<int> CreateSiteAsync(Party party);
        Task<int> UpdateSiteAsync(int siteId, Site site, bool isCompleted = false);
        Task DeleteSiteAsync(int siteId);
        Task<Site> SubmitRegistrationAsync(int siteId, Site site);
        Task<Site> GetSiteNoTrackingAsync(int siteId);
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId);
        Task AcceptCurrentOrganizationAgreementAsync(int signingAuthorityId);
        Task<Organization> GetOrganizationByPartyIdAsync(int partyId);
    }
}
