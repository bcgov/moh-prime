using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.HttpClients.Mail;
using Prime.Models;
using Prime.ViewModels.Emails;

namespace Prime.Services.EmailInternal
{
    public class EmailRenderingService : IEmailRenderingService
    {
        private const string PrimeEmail = "no-reply-prime@gov.bc.ca";
        private const string PrimeSupportEmail = "primesupport@gov.bc.ca";
        private const string MohEmail = "HLTH.HnetConnection@gov.bc.ca";
        private const string ProviderEnrolmentTeamEmail = "Lori.Haggstrom@gov.bc.ca";

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
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.BusinessLicenceUpload, viewModel)
            );
        }

        public async Task<Email> RenderProvisionerLinkEmailAsync(string[] recipientEmails, string cc, CareSettingType careSetting, ProvisionerAccessEmailViewModel viewModel)
        {
            var emailTemplateType = careSetting switch
            {
                CareSettingType.CommunityPharmacy => EmailTemplateType.CommunityPharmacyNotification,
                CareSettingType.HealthAuthority => EmailTemplateType.HealthAuthorityNotification,
                CareSettingType.CommunityPractice => EmailTemplateType.CommunityPracticeNotification,
                CareSettingType.DeviceProvider => EmailTemplateType.DeviceProviderNotification,
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
                subject: "Remote Practitioners Changed",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.RemoteUserUpdatedNotification, viewModel)
            );
        }

        public async Task<Email> RenderRenewalPassedEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = $"{PrimeConfiguration.Current.FrontendUrl}/info";

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Your PRIME Renewal Date Has Passed",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.EnrolleeRenewalPassed, viewModel)
            );
        }

        public async Task<Email> RenderForcedRenewalPassedEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = $"{PrimeConfiguration.Current.FrontendUrl}/info";

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Your PRIME Renewal Date Has Passed",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.ForcedRenewalPassedNotification, viewModel)
            );
        }

        public async Task<Email> RenderRenewalRequiredEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = $"{PrimeConfiguration.Current.FrontendUrl}/info";

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "Renew Your PRIME Enrolment",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.EnrolleeRenewalRequired, viewModel)
            );
        }

        public async Task<Email> RenderForcedRenewalEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = $"{PrimeConfiguration.Current.FrontendUrl}/info";

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "PRIME Renewal Required",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.ForcedRenewalNotification, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedHibcEmailAsync(SiteApprovalEmailViewModel viewModel, int siteId)
        {
            return new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                subject: $"[{siteId}] Site Registration Approved",
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

        public async Task<Email> RenderSiteRegistrationSubmissionEmailAsync(LinkedEmailViewModel viewModel, CareSettingType careSettingCode, int siteId, bool isNew = false)
        {

            string careSetting = careSettingCode switch
            {
                CareSettingType.CommunityPharmacy => "Community Pharmacy",
                CareSettingType.HealthAuthority => "Health Authority",
                CareSettingType.CommunityPractice => "Community Practice",
                CareSettingType.DeviceProvider => "Device Provider",
                _ => ""
            };

            var isNewPrefix = isNew ? "Priority! New Pharmacy - " : "";

            return new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                cc: PrimeSupportEmail,
                subject: $"[{siteId}] {isNewPrefix} PRIME Site Registration Submission - {careSetting}",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.SiteRegistrationSubmission, viewModel)
            );
        }

        public async Task<Email> RenderSiteReviewedNotificationEmailAsync(SiteReviewedEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: ProviderEnrolmentTeamEmail,
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

        public async Task<Email> RenderSiteActiveBeforeRegistrationEmailAsync(string signingAuthorityEmail, SiteActiveBeforeRegistrationEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: signingAuthorityEmail,
                subject: "PRIME Site Registration Submission",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.SiteActiveBeforeRegistrationSubmission, viewModel)
            );
        }

        public async Task<Email> RenderPaperEnrolleeSubmissionEmail(string enrolleeEmail, PaperEnrolleeSubmissionEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: enrolleeEmail,
                subject: "Paper Enrolment Submission",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.PaperEnrolleeSubmission, viewModel)
            );
        }

        public async Task<Email> RenderUnsignedToaEmailAsync(string recipientEmail, EnrolleeUnsignedToaEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = PrimeConfiguration.Current.FrontendUrl;

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: "PharmaNet Terms of Access requires signing",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.EnrolleeUnsignedToa, viewModel)
            );
        }

        public async Task<Email> RenderEnrolleeAbsenceNotificationEmailAsync(string email, EnrolleeAbsenceNotificationEmailViewModel viewModel)
        {
            return new Email
            (
                from: PrimeEmail,
                to: email,
                subject: "PRIME Absence Notification",
                body: await _razorConverterService.RenderEmailTemplateToString(EmailTemplateType.EnrolleeAbsenceNotification, viewModel)
            );
        }
    }
}
