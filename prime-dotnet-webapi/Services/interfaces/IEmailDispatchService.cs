using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEmailDispatchService
    {
        Task SendBusinessLicenceUploadedAsync(CommunitySite site);
        Task SendRemoteUserNotificationsAsync(CommunitySite site, IEnumerable<RemoteUser> remoteUsers);
        Task SendRemoteUsersUpdatedAsync(CommunitySite site);
        Task SendSiteApprovedHIBCAsync(CommunitySite site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(CommunitySite site);
        Task SendSiteApprovedSigningAuthorityAsync(CommunitySite site);
        Task SendSiteReviewedNotificationAsync(int siteId, string note);
        Task SendSiteActiveBeforeRegistrationAsync(int siteId, string signingAuthorityEmail);
        Task SendSiteRegistrationSubmissionAsync(int siteId, int businessLicenceId, CareSettingType careSettingCode);

        Task SendReminderEmailAsync(int enrolleeId);
        Task SendEnrolleeRenewalEmails();
        Task SendEnrolleeUnsignedToaReminderEmails();
        Task SendPaperEnrolmentSubmissionEmailAsync(int enrolleeId);

        Task SendProvisionerLinkAsync(IEnumerable<string> recipientEmails, EnrolmentCertificateAccessToken token, int careSettingCode);

        Task SendHealthAuthoritySiteRegistrationSubmissionAsync(int siteId);

        Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim);
    }
}
