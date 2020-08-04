using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetSitesAsync();
        Task<IEnumerable<Site>> GetSitesAsync(int organizationId);
        Task<Site> GetSiteAsync(int siteId);
        Task<int> CreateSiteAsync(int organizationId);
        Task<int> UpdateSiteAsync(int siteId, Site updatedSite, bool isCompleted = false);
        Task<int> UpdateSiteCompletedAsync(int siteId);
        Task<Site> UpdatePecCode(int siteId, string pecCode);
        Task DeleteSiteAsync(int siteId);
        Task<Site> SubmitRegistrationAsync(int siteId);
        Task<Site> GetSiteNoTrackingAsync(int siteId);
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId);
        Task<BusinessLicenceDocument> AddBusinessLicenceAsync(int siteId, Guid documentGuid, string filename);
        Task<IEnumerable<BusinessLicenceDocument>> GetBusinessLicencesAsync(int siteId);
        Task<BusinessLicenceDocument> GetLatestBusinessLicenceAsync(int siteId);
    }
}
