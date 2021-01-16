using System;
using Prime.Models;

namespace Prime.Services.Razor
{
    // Agreements
    public class AgreementTemplate : RazorTemplate<Agreement>
    {
        public override string ViewPath => "/Views/Agreements/Agreement.cshtml";
    }

    public class AgreementPdfTemplate : RazorTemplate<Agreement>
    {
        public override string ViewPath => "/Views/Agreements/AgreementPdf.cshtml";
    }

    public class CommunityPracticeOrganizationAgreementTemplate : RazorTemplate<Tuple<string, DateTimeOffset>>
    {
        public override string ViewPath => "/Views/Agreements/CommunityPracticeOrganizationAgreement.cshtml";
    }

    public class CommunityPracticeOrganizationAgreementPdfTemplate : RazorTemplate<Tuple<string, DateTimeOffset>>
    {
        public override string ViewPath => "/Views/Agreements/CommunityPracticeOrganizationAgreementPdf.cshtml";
    }

    public class CommunityPharmacyOrganizationAgreementTemplate : RazorTemplate<Tuple<string, DateTimeOffset>>
    {
        public override string ViewPath => "/Views/Agreements/CommunityPharmacyOrganizationAgreement.cshtml";
    }

    public class CommunityPharmacyOrganizationAgreementPdfTemplate : RazorTemplate<Tuple<string, DateTimeOffset>>
    {
        public override string ViewPath => "/Views/Agreements/CommunityPharmacyOrganizationAgreementPdf.cshtml";
    }

    // Emails
    public class CommunityPharmacyManagerEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/CommunityPharmacyManagerEmail.cshtml";
    }

    public class CommunityPracticeEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/CommunityPracticeEmail.cshtml";
    }

    public class HealthAuthorityEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/HealthAuthorityEmail.cshtml";
    }

    public class ReminderEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/ReminderEmail.cshtml";
    }

    public class RemoteUserNotificationEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/RemoteUserNotificationEmail.cshtml";
    }

    public class RenewalPassedEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/RenewalPassedEmail.cshtml";
    }

    public class RenewalRequiredEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/RenewalRequiredEmail.cshtml";
    }

    public class SiteRegistrationSubmissionEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/SiteRegistrationSubmissionEmail.cshtml";
    }

    public class UpdateRemoteUsersEmailTemplate : RazorTemplate<EmailParams>
    {
        public override string ViewPath => "/Views/Emails/UpdateRemoteUsersEmail.cshtml";
    }

    // Others
    public class SiteRegistrationReviewTemplate : RazorTemplate<Site>
    {
        public override string ViewPath => "/Views/SiteRegistrationReview.cshtml";
    }

    public class DocumentTemplate : RazorTemplate<Document>
    {
        public override string ViewPath => "/Views/Helpers/Document.cshtml";
    }

    public class ApologyDocumentTemplate : RazorTemplate<Document>
    {
        public override string ViewPath => "/Views/Helpers/ApologyDocument.cshtml";
    }
}
