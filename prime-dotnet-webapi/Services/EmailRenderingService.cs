using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.Models;
using Prime.Services.Razor;
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

        public EmailRenderingService(IRazorConverterService razorConverterService)
        {
            _razorConverterService = razorConverterService;
        }

        public async Task<Email> RenderBusinessLicenceUploadedEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Site Business Licence Uploaded",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.BusinessLicenceUploaded, viewModel)
            );
        }

        public async Task<Email> RenderProvisionerLinkEmailAsync(IEnumerable<string> recipientEmails, string cc, CareSettingType careSetting, ProvisionerAccessEmailViewModel viewModel)
        {
            var template = careSetting switch
            {
                CareSettingType.CommunityPharmacy => RazorTemplates.Emails.CommunityPharmacyManager,
                CareSettingType.HealthAuthority => RazorTemplates.Emails.HealthAuthority,
                CareSettingType.CommunityPractice => RazorTemplates.Emails.CommunityPractice,
                _ => throw new ArgumentException($"Could not recognize CareSetting {careSetting} in {nameof(RenderProvisionerLinkEmailAsync)}")
            };

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmails,
                cc: cc,
                subject: "New Access Request",
                body: await _razorConverterService.RenderTemplateToStringAsync(template, viewModel)
            );
        }

        public async Task<Email> RenderReminderEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "PRIME Requires your Attention",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.Reminder, viewModel)
            );
        }

        public async Task<Email> RenderRemoteUserNotificationEmailAsync(string recipientEmail, RemoteUserNotificationEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Remote Practitioner Notification",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RemoteUserNotification, viewModel)
            );
        }

        public async Task<Email> RenderRemoteUsersUpdatedEmailAsync(RemoteUsersUpdatedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: "Remote Practitioners Added",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RemoteUsersUpdated, viewModel)
            );
        }

        public async Task<Email> RenderRenewalPassedEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Your PRIME Renewal Date Has Passed",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RenewalPassed, viewModel)
            );
        }

        public async Task<Email> RenderRenewalRequiredEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "PRIME Renewal Required",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RenewalRequired, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedHibcEmailAsync(SiteApprovalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedHibcEmailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedPharmaNetAdministratorEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedPharmaNetAdministratorEmailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedSigningAuthorityEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedSigningAuthorityEmailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderSiteRegistrationSubmissionEmailAsync(LinkedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: "PRIME Site Registration Submission",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteRegistrationSubmission, viewModel)
            );
        }
    }
}
