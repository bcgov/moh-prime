using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.HttpClients.Mail;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendBusinessLicenceUploadedAsync(Site site);
        Task SendProvisionerLinkAsync(IEnumerable<string> recipientEmails, EnrolmentCertificateAccessToken token, int careSettingCode);
        Task SendReminderEmailAsync(int enrolleeId);
        Task SendRemoteUserNotificationsAsync(Site site, IEnumerable<RemoteUser> remoteUsers);
        Task SendRemoteUsersUpdatedAsync(Site site);
        Task SendSiteApprovedHIBCAsync(Site site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(Site site);
        Task SendSiteApprovedSigningAuthorityAsync(Site site);
        Task SendSiteRegistrationSubmissionAsync(int siteId, int businessLicenceId);

        Task SendEnrolleeRenewalEmails();
        Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim);

        Task<int> UpdateEmailLogStatuses(int limit);
    }
}
