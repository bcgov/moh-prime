using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendBusinessLicenceUploadedAsync(CommunitySite site);
        Task SendProvisionerLinkAsync(IEnumerable<string> recipientEmails, EnrolmentCertificateAccessToken token, int careSettingCode);
        Task SendReminderEmailAsync(int enrolleeId);

        /// <summary>
        /// Notify applicable remote users of given site, returning a list of remote users that were notified (may be empty list).
        /// </summary>
        /// <param name="siteId"></param>
        Task<IEnumerable<RemoteUser>> SendRemoteUserNotificationsAsync(int siteId);

        Task SendRemoteUsersUpdatedAsync(CommunitySite site);
        Task SendSiteApprovedHIBCAsync(Site site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(string doingBusinessAs, string pec, string administratorPharmaNetEmail);
        Task SendSiteApprovedSigningAuthorityAsync(string doingBusinessAs, string pec, string provisionerEmail);
        Task SendSiteRegistrationSubmissionAsync(int siteId, int businessLicenceId, CareSettingType careSettingCode);
        Task SendSiteReviewedNotificationAsync(int siteId, string note);
        Task SendSiteActiveBeforeRegistrationAsync(int siteId);

        Task SendEnrolleeRenewalEmails();
        Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim);

        Task<int> UpdateEmailLogStatuses(int limit);
    }
}
