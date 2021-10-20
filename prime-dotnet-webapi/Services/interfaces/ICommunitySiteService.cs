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
        Task<int> UpdateCompletedAsync(int siteId, bool completed);
        Task<Site> UpdateSiteAdjudicator(int siteId, int? adminId = null);
        Task<Site> UpdatePecCode(int siteId, string pecCode);
        Task DeleteSiteAsync(int siteId);
        Task<Site> ApproveSite(int siteId);
        Task<Site> DeclineSite(int siteId);
        Task<Site> UnrejectSite(int siteId);
        Task<Site> EnableEditingSite(int siteId);
        Task<Site> SubmitRegistrationAsync(int siteId);
        Task<Site> GetSiteNoTrackingAsync(int siteId);
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId);
        Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, BusinessLicence businessLicence, Guid documentGuid);
        Task<BusinessLicence> UpdateBusinessLicenceAsync(int businessLicenceId, BusinessLicence updateBusinessLicence);
        Task<IEnumerable<BusinessLicence>> GetBusinessLicencesAsync(int siteId);
        Task<BusinessLicence> GetLatestBusinessLicenceAsync(int siteId);
        Task<BusinessLicenceDocument> AddOrReplaceBusinessLicenceDocumentAsync(int businessLicenceId, Guid documentGuid);
        Task DeleteBusinessLicenceDocumentAsync(int businessLicenceId);
        Task<SiteAdjudicationDocument> AddSiteAdjudicationDocumentAsync(int siteId, Guid documentGuid, int adminId);
        Task<IEnumerable<SiteAdjudicationDocument>> GetSiteAdjudicationDocumentsAsync(int siteId);
        Task<SiteRegistrationNote> CreateSiteRegistrationNoteAsync(int siteId, string note, int adminId);
        Task<IEnumerable<RemoteAccessSearchViewModel>> GetRemoteUserInfoAsync(IEnumerable<CertSearchViewModel> certs);
        Task<IEnumerable<SiteRegistrationNoteViewModel>> GetSiteRegistrationNotesAsync(Site site);

        /// <summary>
        /// Returns business events related to a site or to the organization that site belongs to.
        /// </summary>
        Task<IEnumerable<BusinessEvent>> GetSiteBusinessEventsAsync(int siteId, IEnumerable<int> businessEventTypeCodes);

        Task<SiteAdjudicationDocument> GetSiteAdjudicationDocumentAsync(int documentId);
        Task DeleteSiteAdjudicationDocumentAsync(int documentId);
        Task<SiteNotification> CreateSiteNotificationAsync(int siteRegistrationNoteId, int adminId, int assineeId);
        Task RemoveSiteNotificationAsync(int siteNotificationId);
        Task<IEnumerable<SiteRegistrationNoteViewModel>> GetNotificationsAsync(int siteId, int adminId);
        Task<SiteNotification> GetSiteNotificationAsync(int siteNotificationId);
        Task RemoveNotificationsAsync(int siteId);
        Task UpdateSiteFlag(int siteId, bool flagged);
        Task<SiteRegistrationNoteViewModel> GetSiteRegistrationNoteAsync(int siteId, int siteRegistrationNoteId);
        Task<IEnumerable<int>> GetNotifiedSiteIdsForAdminAsync(ClaimsPrincipal user);
        Task<bool> SiteExists(int siteId);
        Task<bool> PecAssignableAsync(string pec);
        Task<PermissionsRecord> GetPermissionsRecordAsync(int siteId);
    }
}
