using System;
using Prime.Models;
using Prime.Models.Documents;
using Prime.ViewModels.Emails;
using Prime.ViewModels.Agreements;
using Prime.ViewModels.SiteRegistration;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Services.Razor
{
    public class RazorTemplate<TModel>
    {
        public string ViewPath { get; }

        public RazorTemplate(string viewPath)
        {
            ViewPath = viewPath;
        }
    }

    public static class RazorTemplates
    {
        public static readonly RazorTemplate<SiteRegistrationReviewViewModel> SiteRegistrationReview = new RazorTemplate<SiteRegistrationReviewViewModel>("/Views/SiteRegistrationReview.cshtml");
        public static readonly RazorTemplate<HealthAuthoritySiteSubmissionViewModel> HealthAuthoritySiteRegistrationReview = new RazorTemplate<HealthAuthoritySiteSubmissionViewModel>("/Views/HealthAuthoritySiteRegistrationReview.cshtml");

        public static readonly RazorTemplate<File> Document = new RazorTemplate<File>("/Views/Helpers/Document.cshtml");
        public static readonly RazorTemplate<File> ApologyDocument = new RazorTemplate<File>("/Views/Helpers/ApologyDocument.cshtml");

        public static class Agreements
        {
            public static readonly RazorTemplate<Agreement> Base = new RazorTemplate<Agreement>("/Views/Agreements/Agreement.cshtml");
            public static readonly RazorTemplate<Agreement> Pdf = new RazorTemplate<Agreement>("/Views/Agreements/AgreementPdf.cshtml");
        }

        public static class OrgAgreements
        {
            public static readonly RazorTemplate<OrgAgreementRazorViewModel> CommunityPharmacy = new RazorTemplate<OrgAgreementRazorViewModel>("/Views/Agreements/CommunityPharmacyOrganizationAgreement.cshtml");
            public static readonly RazorTemplate<OrgAgreementRazorViewModel> CommunityPharmacyPdf = new RazorTemplate<OrgAgreementRazorViewModel>("/Views/Agreements/CommunityPharmacyOrganizationAgreementPdf.cshtml");
            public static readonly RazorTemplate<OrgAgreementRazorViewModel> CommunityPractice = new RazorTemplate<OrgAgreementRazorViewModel>("/Views/Agreements/CommunityPracticeOrganizationAgreement.cshtml");
            public static readonly RazorTemplate<OrgAgreementRazorViewModel> CommunityPracticePdf = new RazorTemplate<OrgAgreementRazorViewModel>("/Views/Agreements/CommunityPracticeOrganizationAgreementPdf.cshtml");
            public static readonly RazorTemplate<OrgAgreementRazorViewModel> DeviceProvider = new RazorTemplate<OrgAgreementRazorViewModel>("/Views/Agreements/DeviceProviderOrganizationAgreement.cshtml");
            public static readonly RazorTemplate<OrgAgreementRazorViewModel> DeviceProviderPdf = new RazorTemplate<OrgAgreementRazorViewModel>("/Views/Agreements/DeviceProviderOrganizationAgreementPdf.cshtml");
        }

        public static class Emails
        {
            public static readonly RazorTemplate<LinkedEmailViewModel> BusinessLicenceUploaded = new RazorTemplate<LinkedEmailViewModel>("/Views/Emails/BusinessLicenceUploadedEmail.cshtml");
            public static readonly RazorTemplate<ProvisionerAccessEmailViewModel> CommunityPharmacyManager = new RazorTemplate<ProvisionerAccessEmailViewModel>("/Views/Emails/CommunityPharmacyManagerEmail.cshtml");
            public static readonly RazorTemplate<ProvisionerAccessEmailViewModel> CommunityPractice = new RazorTemplate<ProvisionerAccessEmailViewModel>("/Views/Emails/CommunityPracticeEmail.cshtml");
            public static readonly RazorTemplate<ProvisionerAccessEmailViewModel> HealthAuthority = new RazorTemplate<ProvisionerAccessEmailViewModel>("/Views/Emails/HealthAuthorityEmail.cshtml");
            public static readonly RazorTemplate<LinkedEmailViewModel> Reminder = new RazorTemplate<LinkedEmailViewModel>("/Views/Emails/ReminderEmail.cshtml");
            public static readonly RazorTemplate<RemoteUserNotificationEmailViewModel> RemoteUserNotification = new RazorTemplate<RemoteUserNotificationEmailViewModel>("/Views/Emails/RemoteUserNotificationEmail.cshtml");
            public static readonly RazorTemplate<RemoteUsersUpdatedEmailViewModel> RemoteUsersUpdated = new RazorTemplate<RemoteUsersUpdatedEmailViewModel>("/Views/Emails/RemoteUsersUpdatedEmail.cshtml");
            public static readonly RazorTemplate<EnrolleeRenewalEmailViewModel> RenewalPassed = new RazorTemplate<EnrolleeRenewalEmailViewModel>("/Views/Emails/RenewalPassedEmail.cshtml");
            public static readonly RazorTemplate<EnrolleeRenewalEmailViewModel> RenewalRequired = new RazorTemplate<EnrolleeRenewalEmailViewModel>("/Views/Emails/RenewalRequiredEmail.cshtml");
            public static readonly RazorTemplate<SiteApprovalEmailViewModel> SiteApprovedPharmaNetAdministratorEmailTemplate = new RazorTemplate<SiteApprovalEmailViewModel>("Views/Emails/SiteApprovedPharmaNetAdministratorEmail.cshtml");
            public static readonly RazorTemplate<SiteApprovalEmailViewModel> SiteApprovedSigningAuthorityEmailTemplate = new RazorTemplate<SiteApprovalEmailViewModel>("Views/Emails/SiteApprovedSigningAuthorityEmailTemplate.cshtml");
            public static readonly RazorTemplate<SiteApprovalEmailViewModel> SiteApprovedHibcEmailTemplate = new RazorTemplate<SiteApprovalEmailViewModel>("Views/Emails/SiteApprovedHIBCEmailTemplate.cshtml");
            public static readonly RazorTemplate<LinkedEmailViewModel> SiteRegistrationSubmission = new RazorTemplate<LinkedEmailViewModel>("/Views/Emails/SiteRegistrationSubmissionEmail.cshtml");
        }
    }
}
