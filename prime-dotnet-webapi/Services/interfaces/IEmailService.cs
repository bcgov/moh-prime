using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendBusinessLicenceUploadedAsync(CommunitySite site);
        Task SendProvisionerLinkAsync(IEnumerable<string> emails, EnrolmentCertificateAccessToken token, int careSettingCode);
        Task SendReminderEmailAsync(int enrolleeId);
        Task SendRemoteUserNotificationsAsync(CommunitySite site);
        Task SendRemoteUsersUpdatedAsync(CommunitySite site);
        Task SendSiteApprovedHIBCAsync(CommunitySite site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(CommunitySite site);
        Task SendSiteApprovedSigningAuthorityAsync(CommunitySite site);
        Task SendSiteRegistrationSubmissionAsync(CommunitySite site);
        Task SendHealthAuthoritySiteRegistrationSubmissionAsync(int siteId);
        Task SendSiteReviewedNotificationAsync(int siteId, string note);
        Task SendSiteActiveBeforeRegistrationAsync(CommunitySite site);
        Task SendEnrolleeRenewalEmails();
        Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim);
        Task<int> UpdateEmailLogStatuses(int limit);
        Task SendPaperEnrolmentSubmissionEmailAsync(int enrolleeId);
    }
}
