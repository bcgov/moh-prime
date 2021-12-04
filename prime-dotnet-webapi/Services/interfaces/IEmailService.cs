using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Contracts;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendBusinessLicenceUploadedAsync(SendSiteEmail site);
        Task SendProvisionerLinkAsync(SendProvisionerLinkEmail model);
        Task SendReminderEmailAsync(int enrolleeId);
        Task SendRemoteUserNotificationsAsync(SendSiteEmail site);
        Task SendRemoteUsersUpdatedAsync(SendSiteEmail site);
        Task SendSiteApprovedHIBCAsync(SendSiteEmail site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(SendSiteEmail site);
        Task SendSiteApprovedSigningAuthorityAsync(SendSiteEmail site);
        Task SendSiteRegistrationSubmissionAsync(SendSiteEmail site);
        Task SendHealthAuthoritySiteRegistrationSubmissionAsync(int siteId);
        Task SendSiteReviewedNotificationAsync(SendSiteEmail site);
        Task SendSiteActiveBeforeRegistrationAsync(SendSiteEmail site);
        Task SendEnrolleeRenewalEmails();
        Task SendOrgClaimApprovalNotificationAsync(SendOrgClaimApprovalNotificationEmail orgClaim);
        Task<int> UpdateEmailLogStatuses(int limit);
        Task SendPaperEnrolmentSubmissionEmailAsync(int enrolleeId);
        Task SendEnrolleeUnsignedToaReminderEmails();
    }
}
