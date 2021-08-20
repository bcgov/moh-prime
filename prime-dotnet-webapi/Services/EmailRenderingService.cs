using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.Models;
using Prime.HttpClients.Mail;
using Prime.ViewModels.Emails;
namespace Prime.Services.EmailInternal
{
    public class EmailRenderingService : IEmailRenderingService
    {
        private const string PrimeEmail = "no-reply-prime@gov.bc.ca";
        private const string PrimeSupportEmail = "primesupport@gov.bc.ca";
        private const string MohEmail = "HLTH.HnetConnection@gov.bc.ca";

        private readonly IRazorConverterService _razorConverterService;
        private readonly IEmailTemplateService _emailTemplateService;

        public EmailRenderingService(
            IRazorConverterService razorConverterService,
            IEmailTemplateService emailTemplateService
        )
        {
            _razorConverterService = razorConverterService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<Email> RenderBusinessLicenceUploadedEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Site Business Licence Uploaded",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.BusinessLicenceUpload, viewModel)
            );
        }

        public async Task<Email> RenderProvisionerLinkEmailAsync(IEnumerable<string> recipientEmails, string cc, CareSettingType careSetting, ProvisionerAccessEmailViewModel viewModel)
        {
            var emailTemplateType = careSetting switch
            {
                CareSettingType.CommunityPharmacy => EmailTemplateType.CommunityPharmacyNotification,
                CareSettingType.HealthAuthority => EmailTemplateType.HealthAuthorityNotification,
                CareSettingType.CommunityPractice => EmailTemplateType.CommunityPracticeNotification,
                _ => throw new ArgumentException($"Could not recognize CareSetting {careSetting} in {nameof(RenderProvisionerLinkEmailAsync)}")
            };

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmails,
                cc: cc,
                subject: "New Access Request",
                body: await _razorConverterService.RenderEmailTemplateToString(emailTemplateType, viewModel)
            );
        }

        public async Task<Email> RenderReminderEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "PRIME Requires your Attention",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.EnrolleeStatusChange, viewModel)
            );
        }

        public async Task<Email> RenderRemoteUserNotificationEmailAsync(string recipientEmail, RemoteUserNotificationEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Remote Practitioner Notification",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.RemoteUserNotification, viewModel)
            );
        }

        public async Task<Email> RenderRemoteUsersUpdatedEmailAsync(RemoteUsersUpdatedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: "Remote Practitioners Added",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.RemoteUserUpdatedNotification, viewModel)
            );
        }

        public async Task<Email> RenderRenewalPassedEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Your PRIME Renewal Date Has Passed",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.EnrolleeRenewalPassed, viewModel)
            );
        }

        public async Task<Email> RenderRenewalRequiredEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "PRIME Renewal Required",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.EnrolleeRenewalRequired, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedHibcEmailAsync(SiteApprovalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.HIBCSiteSubmission, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedPharmaNetAdministratorEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.SiteApprovedPharmaNetAdministrator, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedSigningAuthorityEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.SiteApprovedSigningAuthority, viewModel)
            );
        }

        public async Task<Email> RenderSiteRegistrationSubmissionEmailAsync(LinkedEmailViewModel viewModel, CareSettingType careSettingCode)
        {
            var recipientEmails = careSettingCode == CareSettingType.CommunityPharmacy
                ? new[] { PrimeSupportEmail }
                : new[] { MohEmail, PrimeSupportEmail };

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmails,
                subject: "PRIME Site Registration Submission",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.SiteRegistrationSubmission, viewModel)
            );
        }

        public async Task<Email> RenderSiteReviewedNotificationEmailAsync(string recipientEmail, SiteNotificationEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "PRIME Site Registration review complete",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.SiteReviewedNotification, viewModel)
            );
        }


        public async Task<Email> RenderOrgClaimApprovalNotificationEmailAsync(string newSigningAuthorityEmail, OrgClaimApprovalNotificationViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: newSigningAuthorityEmail,
                subject: "Organization Claim was Approved",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.OrganizationClaimApprovalNotification, viewModel)
            );
        }
    }
}
