using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.Sites;

namespace Prime.Services
{
    public interface ISiteService
    {
        Task<bool> SiteExistsAsync(int siteId);
        Task<SiteStatusType> GetSiteStatusAsync(int siteId);
        Task<bool> PecAssignableAsync(int siteId, string pec);
        Task UpdateCompletedAsync(int siteId, bool completed);
        Task<Site> UpdateSiteAdjudicator(int siteId, int? adminId = null);
        Task<Site> UpdatePecCode(int siteId, string pecCode);
        Task DeleteSiteAsync(int siteId);
        Task<Site> ApproveSite(int siteId);
        Task<Site> DeclineSite(int siteId);
        Task<Site> UnrejectSite(int siteId);
        Task<Site> EnableEditingSite(int siteId);
        Task<Site> SubmitRegistrationAsync(int siteId);
        Task<IEnumerable<BusinessDayViewModel>> GetBusinessHoursAsync(int siteId);
        Task<IEnumerable<RemoteUserViewModel>> GetRemoteUsersAsync(int siteId);
        Task<IEnumerable<RemoteAccessSearchViewModel>> GetRemoteUserInfoAsync(IEnumerable<CertSearchViewModel> certs);
        /// <summary>
        /// Save the fact that the given <c>notifiedUsers</c> were notified by email.
        /// </summary>
        Task MarkUsersAsNotifiedAsync(IEnumerable<RemoteUser> notifiedUsers);
        Task<SiteRegistrationNote> CreateSiteRegistrationNoteAsync(int siteId, string note, int adminId);
        Task<IEnumerable<SiteRegistrationNoteViewModel>> GetSiteRegistrationNotesAsync(int siteId);
        Task<SiteRegistrationNoteViewModel> GetSiteRegistrationNoteAsync(int siteId, int siteRegistrationNoteId);

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
        Task<SiteAdjudicationDocument> AddSiteAdjudicationDocumentAsync(int siteId, Guid documentGuid, int adminId);
        Task<IEnumerable<SiteAdjudicationDocument>> GetSiteAdjudicationDocumentsAsync(int siteId);
        Task<string> GetSitePecAsync(int siteId);
    }
}
