using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Prime.Models;
using System.IO;
using Prime.Services.Clients;

namespace Prime.Services
{
    public class EmailParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TokenUrl { get; set; }
        public int MaxViews { get => EnrolmentCertificateAccessToken.MaxViews; }
        public int ExpiryDays { get => EnrolmentCertificateAccessToken.Lifespan.Days; }
        public string ProvisionerName { get; set; }
        public byte[] BusinessLicenceDoc { get; set; }

        public EmailParams()
        {

        }

        public EmailParams(EnrolmentCertificateAccessToken token, string provisionerName = null)
        {
            FirstName = token.Enrollee.FirstName;
            LastName = token.Enrollee.LastName;
            TokenUrl = token.FrontendUrl;
            ProvisionerName = provisionerName;
        }

        public EmailParams(Site site)
        {
            // TODO what does the email body need?
            // TODO split out into specific email params for different emails
            // TODO if only used to render razor views then rename
        }
    }

    public class EmailService : BaseService, IEmailService
    {
        private const string PRIME_EMAIL = "no-reply-prime@gov.bc.ca";
        private const string PRIME_SUPPORT_EMAIL = "primesupport@gov.bc.ca";
        private const string MOH_EMAIL = "HLTH.HnetConnection@gov.bc.ca";
        private readonly IRazorConverterService _razorConverterService;
        private readonly IDocumentService _documentService;
        private readonly IPdfService _pdfService;
        private readonly IOrganizationService _organizationService;
        private readonly IChesClient _chesClient;

        private readonly ISmtpEmailClient _smtpEmailClient;

        private readonly IDocumentManagerClient _documentManagerClient;

        public EmailService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IRazorConverterService razorConverterService,
            IDocumentService documentService,
            IPdfService pdfService,
            IOrganizationService organizationService,
            IChesClient chesClient,
            ISmtpEmailClient smtpEmailClient,
            IDocumentManagerClient documentManagerClient)
            : base(context, httpContext)
        {
            _razorConverterService = razorConverterService;
            _documentService = documentService;
            _pdfService = pdfService;
            _organizationService = organizationService;
            _chesClient = chesClient;
            _documentManagerClient = documentManagerClient;
            _smtpEmailClient = smtpEmailClient;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool AreValidEmails(string[] emails)
        {
            return emails.All(e => IsValidEmail(e));
        }

        public async Task SendReminderEmailAsync(Enrollee enrollee)
        {
            if (!IsValidEmail(enrollee.ContactEmail))
            {
                // TODO Log invalid email, cannot send?
                return;
            }

            string subject = "PRIME Requires your Attention";
            string body = await _razorConverterService.RenderViewToStringAsync("/Views/Emails/ReminderEmail.cshtml", new EmailParams());
            await Send(PRIME_EMAIL, enrollee.ContactEmail, subject, body);
        }

        public async Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null)
        {
            if (!AreValidEmails(recipients))
            {
                // TODO Log invalid email, cannot send
                throw new ArgumentException("Cannot send provisioner link, supplied email address(es) are invalid.");
            }

            if (token.Enrollee == null)
            {
                await _context.Entry(token).Reference(t => t.Enrollee).LoadAsync();
            }

            // Always send a copy to the enrollee
            var ccEmails = new List<string>() { token.Enrollee.ContactEmail };

            string subject = "New Access Request";
            string viewName = string.IsNullOrEmpty(provisionerName)
                ? "/Views/Emails/OfficeManagerEmail.cshtml"
                : "/Views/Emails/VendorEmail.cshtml";
            string emailBody = await _razorConverterService.RenderViewToStringAsync(viewName, new EmailParams(token, provisionerName));
            await Send(PRIME_EMAIL, recipients, ccEmails, subject, emailBody, Enumerable.Empty<(string Filename, byte[] Content)>());
        }

        // TODO currently the front-end restricts uploads to images, but when that changes to include PDF uploads
        // this method needs to be refactored to check for mimetype (PDF vs image) to skip PDF generation
        public async Task SendSiteRegistrationAsync(Site site)
        {
            var subject = "PRIME Site Registration Submission";
            var body = await _razorConverterService.RenderViewToStringAsync("/Views/Emails/SiteRegistrationSubmissionEmail.cshtml", new EmailParams(site));

            Document businessLicenceDoc = null;
            string businessLicenceTemplate = "/Views/Helpers/Document.cshtml";
            try
            {
                var stream = await _documentService.GetStreamForLatestBusinessLicenceDocument(site.Id);
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                businessLicenceDoc = new Document("BusinessLicence.pdf", ms.ToArray());
            }
            catch (NullReferenceException)
            {
                businessLicenceDoc = new Document("BusinessLicence.pdf", new byte[20]);
                businessLicenceTemplate = "/Views/Helpers/ApologyDocument.cshtml";
            }

            var organization = site.Organization;
            var organizationAgreementHtml = "";
            if (await _organizationService.GetLatestSignedAgreementAsync(organization.Id) != null)
            {
                Document organizationAgreementDoc = null;
                string organizationAgreementTemplate = "/Views/Helpers/Document.cshtml";
                try
                {
                    var stream = await _documentService.GetStreamForLatestSignedAgreementDocument(organization.Id);
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);
                    organizationAgreementDoc = new Document("SignedOrganizationAgreement.pdf", ms.ToArray());
                }
                catch (NullReferenceException)
                {
                    organizationAgreementDoc = new Document("SignedOrganizationAgreement.pdf", new byte[20]);
                    organizationAgreementTemplate = "/Views/Helpers/ApologyDocument.cshtml";
                }

                organizationAgreementHtml = await _razorConverterService.RenderViewToStringAsync(organizationAgreementTemplate, organizationAgreementDoc);
            }
            else
            {
                organizationAgreementHtml = await _razorConverterService.RenderViewToStringAsync("/Views/OrganizationAgreementPdf.cshtml", organization);
            }

            string registrationReviewFilename = "SiteRegistrationReview.pdf";

            var attachments = new (string Filename, string HtmlContent)[]
            {
                ("OrganizationAgreement.pdf", organizationAgreementHtml),
                (registrationReviewFilename, await _razorConverterService.RenderViewToStringAsync("/Views/SiteRegistrationReview.cshtml", site)),
                ("BusinessLicence.pdf", await _razorConverterService.RenderViewToStringAsync(businessLicenceTemplate, businessLicenceDoc))
            }
            .Select(content => (Filename: content.Filename, Content: _pdfService.Generate(content.HtmlContent)));

            await Send(PRIME_EMAIL, new[] { MOH_EMAIL, PRIME_SUPPORT_EMAIL }, subject, body, attachments);

            var siteRegReviewPdf = attachments.Single(a => a.Filename == registrationReviewFilename).Content;
            await SaveSiteRegistrationReview(site.Id, registrationReviewFilename, siteRegReviewPdf);
        }

        private async Task SaveSiteRegistrationReview(int siteId, string filename, byte[] pdf)
        {
            var documentGuid = await _documentManagerClient.SendFileAsync(new MemoryStream(pdf), filename, $"sites/{siteId}/site_registration_reviews");

            _context.SiteRegistrationReviewDocuments.Add(new SiteRegistrationReviewDocument(siteId, documentGuid, filename));
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetPharmaNetProvisionerEmailAsync(string provisionerName)
        {
            var vendor = await _context.Vendors
                .SingleOrDefaultAsync(v => v.Name == provisionerName);

            return vendor?.Email;
        }

        public async Task<IEnumerable<string>> GetPharmaNetProvisionerNamesAsync()
        {
            return await _context.Vendors
                .Select(v => v.Name)
                .ToListAsync();
        }

        private async Task Send(string from, string to, string subject, string body)
        {
            await Send(from, new[] { to }, new string[0], subject, body, Enumerable.Empty<(string Filename, byte[] Content)>());
        }

        private async Task Send(string from, string to, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            await Send(from, new[] { to }, new string[0], subject, body, attachments);
        }

        private async Task Send(string from, IEnumerable<string> to, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            await Send(from, to, new string[0], subject, body, attachments);
        }

        private async Task Send(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            if (!to.Any())
            {
                throw new ArgumentException("Must specify at least one \"To\" email address.");
            }

            if (PrimeConstants.ENVIRONMENT_NAME != "prod")
            {
                subject = $"THE FOLLOWING EMAIL IS A TEST: {subject}";
            }

            // If CHES Email Service is running and CHES_ENABLED = true, else send through smtp
            if (PrimeConstants.CHES_ENABLED == "true" && await _chesClient.HealthCheckAsync())
            {
                await _chesClient.SendAsync(from, to, cc, subject, body, attachments);
            }
            else
            {
                await _smtpEmailClient.SendAsync(from, to, cc, subject, body, attachments);
            }
        }
    }
}
