using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;
using System.Security.Claims;
namespace Prime.Services
{
    public interface ICommunitySiteService
    {
        Task<IEnumerable<CommunitySite>> GetSitesAsync(int? organizationId = null);
        Task<CommunitySite> GetSiteAsync(int siteId);
        Task<int> CreateSiteAsync(int organizationId);
        Task UpdateSiteAsync(int siteId, CommunitySiteUpdateModel updatedSite);
        Task<Site> GetSiteNoTrackingAsync(int siteId);
        Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, BusinessLicence businessLicence, Guid documentGuid);
        Task<BusinessLicence> UpdateBusinessLicenceAsync(int businessLicenceId, BusinessLicence updateBusinessLicence);
        Task<IEnumerable<BusinessLicence>> GetBusinessLicencesAsync(int siteId);
        Task<BusinessLicence> GetLatestBusinessLicenceAsync(int siteId);
        Task<BusinessLicenceDocument> AddOrReplaceBusinessLicenceDocumentAsync(int businessLicenceId, Guid documentGuid);
        Task DeleteBusinessLicenceDocumentAsync(int businessLicenceId);
        Task<SiteAdjudicationDocument> AddSiteAdjudicationDocumentAsync(int siteId, Guid documentGuid, int adminId);
        Task<IEnumerable<SiteAdjudicationDocument>> GetSiteAdjudicationDocumentsAsync(int siteId);

        /// <summary>
        /// Returns business events related to a site or to the organization that site belongs to.
        /// </summary>
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEventsAsync(int siteId, IEnumerable<int> businessEventTypeCodes);

        Task<SiteAdjudicationDocument> GetSiteAdjudicationDocumentAsync(int documentId);
        Task DeleteSiteAdjudicationDocumentAsync(int documentId);
        Task<SiteNotification> CreateSiteNotificationAsync(int siteRegistrationNoteId, int adminId, int assineeId);
        Task RemoveSiteNotificationAsync(int siteNotificationId);
        Task<IEnumerable<SiteRegistrationNoteViewModel>> GetNotificationsAsync(int siteId, int adminId);
        Task RemoveNotificationsAsync(int siteId);
        Task UpdateSiteFlag(int siteId, bool flagged);
        Task<IEnumerable<int>> GetNotifiedSiteIdsForAdminAsync(ClaimsPrincipal user);
        Task<bool> SiteExists(int siteId);
        Task<PermissionsRecord> GetPermissionsRecordAsync(int siteId);
    }
}
