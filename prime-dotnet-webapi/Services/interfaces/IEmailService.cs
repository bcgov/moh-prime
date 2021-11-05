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
        Task SendRemoteUserNotificationsAsync(CommunitySite site, IEnumerable<RemoteUser> remoteUsers);
        Task SendRemoteUsersUpdatedAsync(CommunitySite site);
        Task SendSiteApprovedHIBCAsync(CommunitySite site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(CommunitySite site);
        Task SendSiteApprovedSigningAuthorityAsync(CommunitySite site);
        Task SendSiteRegistrationSubmissionAsync(int siteId, int businessLicenceId, CareSettingType careSettingCode);
        Task SendSiteReviewedNotificationAsync(int siteId, string note);
        Task SendSiteActiveBeforeRegistrationAsync(int siteId, string signingAuthorityEmail);
        Task SendEnrolleeRenewalEmails();
        Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim);
        Task<int> UpdateEmailLogStatuses(int limit);
        Task SendPaperEnrolmentSubmissionEmailAsync(int enrolleeId);
    }
}
