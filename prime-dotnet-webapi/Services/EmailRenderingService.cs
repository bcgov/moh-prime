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
        //private const string ProviderEnrolmentTeamEmail = "Lori.Haggstrom@gov.bc.ca";

        private readonly IRazorConverterService _razorConverterService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;

        public EmailRenderingService(IRazorConverterService razorConverterService,
            IEmailTemplateService emailTemplateService, IHealthAuthoritySiteService healthAuthoritySiteService)
        {
            _razorConverterService = razorConverterService;
            _emailTemplateService = emailTemplateService;
            _healthAuthoritySiteService = healthAuthoritySiteService;
        }

        public async Task<Email> RenderBusinessLicenceUploadedEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.BusinessLicenceUpload);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
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

            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(emailTemplateType);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmails,
                cc: cc,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderReminderEmailAsync(string recipientEmail, LinkedEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.EnrolleeStatusChange);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderRemoteUserNotificationEmailAsync(string recipientEmail, RemoteUserNotificationEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.RemoteUserNotification);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderRemoteUsersUpdatedEmailAsync(RemoteUsersUpdatedEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.RemoteUserUpdatedNotification);

            return new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderRenewalPassedEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = PrimeConfiguration.Current.FrontendUrl;
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.EnrolleeRenewalPassed);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderForcedRenewalPassedEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = PrimeConfiguration.Current.FrontendUrl;
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.ForcedRenewalPassedNotification);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderRenewalRequiredEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = PrimeConfiguration.Current.FrontendUrl;
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.EnrolleeRenewalRequired);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderForcedRenewalEmailAsync(string recipientEmail, EnrolleeRenewalEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = PrimeConfiguration.Current.FrontendUrl;
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.ForcedRenewalNotification);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedHibcEmailAsync(SiteApprovalEmailViewModel viewModel, int siteId)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.HIBCSiteSubmission);

            return new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                subject: emailTemplate.Subject.Replace("{siteId}", siteId.ToString()),
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderHealthAuthoritySiteApprovedEmailAsync(SiteApprovalEmailViewModel viewModel, int siteId)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.HASiteApproval);

            return new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                subject: emailTemplate.Subject.Replace("{siteId}", siteId.ToString()),
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }


        public async Task<Email> RenderSiteApprovedPharmaNetAdministratorEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.SiteApprovedPharmaNetAdministrator);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderSiteApprovedSigningAuthorityEmailAsync(string recipientEmail, SiteApprovalEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.SiteApprovedSigningAuthority);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
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

            // add Health Authority in subject line
            if (careSettingCode == CareSettingType.HealthAuthority)
            {
                var site = await _healthAuthoritySiteService.GetHealthAuthoritySiteAsync(siteId);
                careSetting = $"{careSetting} ({site.HealthAuthorityOrganization.Name})";
            }

            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.SiteRegistrationSubmission);
            var isNewPrefix = isNew ? "Priority! New Pharmacy - " : "";
            var subject = emailTemplate.Subject.Replace("{siteId}", siteId.ToString())
                .Replace("{isNewPrefix}", isNewPrefix).Replace("{careSetting}", careSetting);

            return new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                cc: PrimeSupportEmail,
                subject: subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderSiteReviewedNotificationEmailAsync(SiteReviewedEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.SiteReviewedNotification);

            return new Email
            (
                from: PrimeEmail,
                to: PrimeConfiguration.Current.ProviderEnrolmentTeam.EmailAddress.Split(";"),
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderOrgClaimApprovalNotificationEmailAsync(string newSigningAuthorityEmail, OrgClaimApprovalNotificationViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.OrganizationClaimApprovalNotification);

            return new Email
            (
                from: PrimeEmail,
                to: newSigningAuthorityEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderSiteActiveBeforeRegistrationEmailAsync(string signingAuthorityEmail, SiteActiveBeforeRegistrationEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.SiteActiveBeforeRegistrationSubmission);

            return new Email
            (
                from: PrimeEmail,
                to: signingAuthorityEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderPaperEnrolleeSubmissionEmail(string enrolleeEmail, PaperEnrolleeSubmissionEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.PaperEnrolleeSubmission);

            return new Email
            (
                from: PrimeEmail,
                to: enrolleeEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderUnsignedToaEmailAsync(string recipientEmail, EnrolleeUnsignedToaEmailViewModel viewModel)
        {
            viewModel.PrimeUrl = PrimeConfiguration.Current.FrontendUrl;
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.EnrolleeUnsignedToa);

            return new Email
            (
                from: PrimeEmail,
                to: recipientEmail,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }

        public async Task<Email> RenderEnrolleeAbsenceNotificationEmailAsync(string email, EnrolleeAbsenceNotificationEmailViewModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(EmailTemplateType.EnrolleeAbsenceNotification);

            return new Email
            (
                from: PrimeEmail,
                to: email,
                subject: emailTemplate.Subject,
                body: _razorConverterService.RenderEmailTemplateToString(emailTemplate, viewModel)
            );
        }
    }
}
