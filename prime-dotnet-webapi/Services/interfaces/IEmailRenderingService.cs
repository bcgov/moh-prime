using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.HttpClients.Mail;
using Prime.ViewModels.Emails;

namespace Prime.Services.EmailInternal
{
    public interface IEmailRenderingService
    {
        Task<Email> RenderBusinessLicenceUploadedEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel);
        Task<Email> RenderProvisionerLinkEmailAsync(IEnumerable<string> recipientEmails, string cc, CareSettingType careSetting, ProvisionerAccessEmailViewModel viewModel);
        Task<Email> RenderReminderEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel);
        Task<Email> RenderRemoteUserNotificationEmailAsync(string recipientEmail, RemoteUserNotificationEmailViewModel viewModel);
        Task<Email> RenderRemoteUsersUpdatedEmailAsync(RemoteUsersUpdatedEmailViewModel viewModel);
        Task<Email> RenderRenewalPassedEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel);
        Task<Email> RenderRenewalRequiredEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel);
        Task<Email> RenderSiteApprovedHibcEmailAsync(SiteApprovalEmailViewModel viewModel);
        Task<Email> RenderSiteApprovedPharmaNetAdministratorEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel);
        Task<Email> RenderSiteApprovedSigningAuthorityEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel);
        Task<Email> RenderSiteRegistrationSubmissionEmailAsync(LinkedEmailViewModel viewModel, CareSettingType careSettingCode);
        Task<Email> RenderSiteReviewedNotificationEmailAsync(SiteReviewedEmailViewModel viewModel);
        Task<Email> RenderOrgClaimApprovalNotificationEmailAsync(string newSigningAuthorityEmail, OrgClaimApprovalNotificationViewModel viewModel);
        Task<Email> RenderSiteActiveBeforeRegistrationEmailAsync(string newSigningAuthorityEmail, SiteActiveBeforeRegistrationEmailViewModel viewModel);
    }
}
