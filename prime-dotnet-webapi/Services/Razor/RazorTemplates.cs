using System;
using Prime.Models;

namespace Prime.Services.Razor
{
    public class RazorTemplate<TModel>
    {
        public string ViewPath { get; set; }
    }

    public static class RazorTemplates
    {
        public static readonly RazorTemplate<Site> SiteRegistrationReview = new RazorTemplate<Site> { ViewPath = "/Views/SiteRegistrationReview.cshtml" };
        public static readonly RazorTemplate<Document> Document = new RazorTemplate<Document> { ViewPath = "/Views/Helpers/Document.cshtml" };
        public static readonly RazorTemplate<Document> ApologyDocument = new RazorTemplate<Document> { ViewPath = "/Views/Helpers/ApologyDocument.cshtml" };

        public static class Agreements
        {
            public static readonly RazorTemplate<Agreement> Base = new RazorTemplate<Agreement> { ViewPath = "/Views/Agreements/Agreement.cshtml" };
            public static readonly RazorTemplate<Agreement> Pdf = new RazorTemplate<Agreement> { ViewPath = "/Views/Agreements/AgreementPdf.cshtml" };
        }

        public static class OrgAgreements
        {
            public static readonly RazorTemplate<Tuple<string, DateTimeOffset>> CommunityPharmacy = new RazorTemplate<Tuple<string, DateTimeOffset>> { ViewPath = "/Views/Agreements/CommunityPharmacyOrganizationAgreement.cshtml" };
            public static readonly RazorTemplate<Tuple<string, DateTimeOffset>> CommunityPharmacyPdf = new RazorTemplate<Tuple<string, DateTimeOffset>> { ViewPath = "/Views/Agreements/CommunityPharmacyOrganizationAgreementPdf.cshtml" };
            public static readonly RazorTemplate<Tuple<string, DateTimeOffset>> CommunityPractice = new RazorTemplate<Tuple<string, DateTimeOffset>> { ViewPath = "/Views/Agreements/CommunityPracticeOrganizationAgreement.cshtml" };
            public static readonly RazorTemplate<Tuple<string, DateTimeOffset>> CommunityPracticePdf = new RazorTemplate<Tuple<string, DateTimeOffset>> { ViewPath = "/Views/Agreements/CommunityPracticeOrganizationAgreementPdf.cshtml" };
        }

        public static class Emails
        {
            public static readonly RazorTemplate<EmailParams> BusinessLicenceUploaded = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/BusinessLicenceUploadedEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> CommunityPharmacyManager = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/CommunityPharmacyManagerEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> CommunityPractice = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/CommunityPracticeEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> HealthAuthority = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/HealthAuthorityEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> Reminder = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/ReminderEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> RemoteUserNotification = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/RemoteUserNotificationEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> RenewalPassed = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/RenewalPassedEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> RenewalRequired = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/RenewalRequiredEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> SiteRegistrationSubmission = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/SiteRegistrationSubmissionEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> UpdateRemoteUsers = new RazorTemplate<EmailParams> { ViewPath = "/Views/Emails/UpdateRemoteUsersEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> SiteApprovedPharmaNetAdministratorEmailTemplate = new RazorTemplate<EmailParams> { ViewPath = "Views/Emails/SiteApprovedPharmaNetAdministratorEmail.cshtml" };
            public static readonly RazorTemplate<EmailParams> SiteApprovedSigningAuthorityEmailTemplate = new RazorTemplate<EmailParams> { ViewPath = "Views/Emails/SiteApprovedSigningAuthorityEmailTemplate.cshtml" };
            public static readonly RazorTemplate<EmailParams> SiteApprovedHIBCEmailTemplate = new RazorTemplate<EmailParams> { ViewPath = "Views/Emails/SiteApprovedHIBCEmailTemplate.cshtml" };
        }
    }
}
