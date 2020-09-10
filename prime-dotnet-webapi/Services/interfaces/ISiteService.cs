using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface ISiteService
    {
        Task<IEnumerable<Site>> GetSitesAsync(int? organizationId = null);
        Task<Site> GetSiteAsync(int siteId);
        Task<int> CreateSiteAsync(int organizationId);
        Task<int> UpdateSiteAsync(int siteId, SiteUpdateModel updatedSite);
        Task<int> UpdateCompletedAsync(int siteId);
        Task<Site> UpdateSiteAdjudicator(int siteId, Nullable<int> adminId = null);
        Task<Site> UpdatePecCode(int siteId, string pecCode);
        Task DeleteSiteAsync(int siteId);
        Task<Site> ApproveSite(int siteId);
        Task<Site> SubmitRegistrationAsync(int siteId);
        Task<Site> GetSiteNoTrackingAsync(int siteId);
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId);
        Task<BusinessLicenceDocument> AddBusinessLicenceAsync(int siteId, Guid documentGuid, string filename);
        Task<IEnumerable<BusinessLicenceDocument>> GetBusinessLicencesAsync(int siteId);
        Task<BusinessLicenceDocument> GetLatestBusinessLicenceAsync(int siteId);
        Task<SiteRegistrationNote> CreateSiteRegistrationNoteAsync(int siteId, string note, int adminId);
    }
}
