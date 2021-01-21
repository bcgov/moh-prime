using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Prime.Models;
using System.IO;
using Prime.HttpClients;
using DelegateDecompiler.EntityFrameworkCore;
using Prime.Services.Razor;

namespace Prime.Services
{
    public class EmailParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TokenUrl { get; set; }
        public string ProvisionerName { get; set; }

        public DateTimeOffset? RenewalDate { get; set; }
        public Site Site { get; set; }
        public string DocumentUrl { get; set; }
        public int MaxViews { get => EnrolmentCertificateAccessToken.MaxViews; }
        public int ExpiryDays { get => EnrolmentCertificateAccessToken.Lifespan.Days; }

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

        public EmailParams(string firstName, string lastName, DateTimeOffset expiryDate)
        {
            FirstName = firstName;
            LastName = lastName;
            RenewalDate = expiryDate;
        }

        public EmailParams(Site site, string documentUrl)
        {
            Site = site;
            DocumentUrl = documentUrl;
        }

        public EmailParams(Site site)
        {
            Site = site;
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
        private readonly IDocumentAccessTokenService _documentAccessTokenService;
        private readonly ISiteService _siteService;
        private readonly IAgreementService _agreementService;
        public EmailService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IRazorConverterService razorConverterService,
            IDocumentService documentService,
            IPdfService pdfService,
            IOrganizationService organizationService,
            IChesClient chesClient,
            ISmtpEmailClient smtpEmailClient,
            IDocumentManagerClient documentManagerClient,
            IDocumentAccessTokenService documentAccessTokenService,
            ISiteService siteService,
            IAgreementService agreementService)
            : base(context, httpContext)
        {
            _razorConverterService = razorConverterService;
            _documentService = documentService;
            _pdfService = pdfService;
            _organizationService = organizationService;
            _chesClient = chesClient;
            _documentManagerClient = documentManagerClient;
            _documentAccessTokenService = documentAccessTokenService;
            _smtpEmailClient = smtpEmailClient;
            _siteService = siteService;
            _agreementService = agreementService;
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

        public async Task SendReminderEmailAsync(int enrolleeId)
        {
            var enrolleeEmail = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .Select(e => e.Email)
                .SingleOrDefaultAsync();

            if (!IsValidEmail(enrolleeEmail))
            {
                // TODO Log invalid email, cannot send?
                return;
            }

            string subject = "PRIME Requires your Attention";
            string body = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.Reminder, new EmailParams());
            await Send(PRIME_EMAIL, enrolleeEmail, subject, body);
        }

        public async Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, int careSettingCode)
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
            var ccEmails = new List<string>() { token.Enrollee.Email };

            string subject = "New Access Request";

            RazorTemplate<EmailParams> template = careSettingCode switch
            {
                (int)CareSettingType.CommunityPharmacy => RazorTemplates.Emails.CommunityPharmacyManager,
                (int)CareSettingType.HealthAuthority => RazorTemplates.Emails.HealthAuthority,
                _ => RazorTemplates.Emails.CommunityPractice,
            };
            string emailBody = await _razorConverterService.RenderTemplateToStringAsync(template, new EmailParams(token));
            await Send(PRIME_EMAIL, recipients, ccEmails, subject, emailBody, Enumerable.Empty<(string Filename, byte[] Content)>());
        }
        public async Task SendSiteRegistrationAsync(Site site)
        {
            var subject = "PRIME Site Registration Submission";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.SiteRegistrationSubmission,
                new EmailParams(site, await GetBusinessLicenceDownloadLink(site.Id)));

            string registrationReviewFilename = "SiteRegistrationReview.pdf";

            var attachments = await GetSiteRegistrationAttachments(site);

            await Send(PRIME_EMAIL, new[] { MOH_EMAIL, PRIME_SUPPORT_EMAIL }, subject, body, attachments);

            var siteRegReviewPdf = attachments.Single(a => a.Filename == registrationReviewFilename).Content;
            await SaveSiteRegistrationReview(site.Id, registrationReviewFilename, siteRegReviewPdf);
        }

        public async Task SendRemoteUsersUpdatedAsync(Site site)
        {
            var subject = "Remote Practioners Added";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
               RazorTemplates.Emails.UpdateRemoteUsers,
                new EmailParams(site, await GetBusinessLicenceDownloadLink(site.Id)));

            var attachments = await GetSiteRegistrationAttachments(site);

            await Send(PRIME_EMAIL, new[] { MOH_EMAIL, PRIME_SUPPORT_EMAIL }, subject, body, attachments);
        }

        public async Task SendRemoteUsersNotificationAsync(Site site, IEnumerable<RemoteUser> remoteUsers)
        {
            var subject = "Remote Practitioner Notification";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.RemoteUserNotification,
                new EmailParams(site));

            foreach (var remoteUser in remoteUsers)
            {
                await Send(PRIME_EMAIL, remoteUser.Email, subject, body);
            }

        }

        public async Task SendBusinessLicenceUploadedAsync(Site site)
        {
            var subject = "Site Business Licence Uploaded";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.BusinessLicenceUploaded,
                new EmailParams(site, await GetBusinessLicenceDownloadLink(site.Id)));

            await Send(PRIME_EMAIL, site.Adjudicator.Email, subject, body);
        }

        public async Task SendSiteApprovedPharmaNetAdministratorAsync(Site site)
        {
            var subject = "Site Registration Approved";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.SiteApprovedPharmaNetAdministratorEmailTemplate,
                new EmailParams(site));

            await Send(PRIME_EMAIL, site.AdministratorPharmaNet.Email, subject, body);
        }

        public async Task SendSiteApprovedSigningAuthorityAsync(Site site)
        {
            var subject = "Site Registration Approved";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.SiteApprovedSigningAuthorityEmailTemplate,
                new EmailParams(site));

            await Send(PRIME_EMAIL, site.Provisioner.Email, subject, body);
        }

        public async Task SendSiteApprovedHIBCAsync(Site site)
        {
            var subject = "Site Approval Notification";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.SiteApprovedHIBCEmailTemplate,
                new EmailParams(site));

            await Send(PRIME_EMAIL, MOH_EMAIL, subject, body);
        }

        private async Task<string> GetBusinessLicenceDownloadLink(int siteId)
        {
            var businessLicence = await _siteService.GetBusinessLicenceAsync(siteId);
            if (businessLicence.BusinessLicenceDocument == null)
            {
                return "";
            }
            var documentAccessToken = await _documentAccessTokenService.CreateDocumentAccessTokenAsync(businessLicence.BusinessLicenceDocument.DocumentGuid);
            return documentAccessToken.DownloadUrl;
        }

        private async Task<IEnumerable<(string Filename, byte[] Content)>> GetSiteRegistrationAttachments(Site site)
        {
            var organization = site.Organization;

            var organizationAgreementHtml = "";
            var organizationAgreementFilename = "OrganizationAgreement.pdf";
            var registrationReviewFilename = "SiteRegistrationReview.pdf";

            var siteRegistrationReviewHtml = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.SiteRegistrationReview, site);

            var signedOrganizationAgreementDocument = await _organizationService.GetLatestSignedAgreementAsync(organization.Id);
            if (signedOrganizationAgreementDocument != null)
            {
                var fileExt = signedOrganizationAgreementDocument.Filename.Split(".").Last();

                if (fileExt.Equals("pdf"))
                {
                    // If the file is already a pdf we can skip the conversion steps and return it.
                    var stream = await _documentService.GetStreamForLatestSignedAgreementDocument(organization.Id);
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    return new (string Filename, byte[] HtmlContent)[]
                    {
                        (organizationAgreementFilename, ms.ToArray()),
                        (registrationReviewFilename, _pdfService.Generate(siteRegistrationReviewHtml))
                    };
                }

                Document organizationAgreementDoc;
                RazorTemplate<Document> template = RazorTemplates.Document;
                try
                {
                    var stream = await _documentService.GetStreamForLatestSignedAgreementDocument(organization.Id);
                    var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    organizationAgreementDoc = new Document("SignedOrganizationAgreement.pdf", ms.ToArray());
                }
                catch (NullReferenceException)
                {
                    organizationAgreementDoc = new Document("SignedOrganizationAgreement.pdf", new byte[20]);
                    template = RazorTemplates.ApologyDocument;
                }

                organizationAgreementHtml = await _razorConverterService.RenderTemplateToStringAsync(template, organizationAgreementDoc);
            }
            else
            {
                var agreementType = _organizationService.OrgAgreementTypeForSiteSetting(site.CareSettingCode.Value);
                var agreementDate = await _context.Agreements
                    .AsNoTracking()
                    .Include(a => a.AgreementVersion)
                    .Where(a => a.OrganizationId == organization.Id
                        && a.AgreementVersion.AgreementType == agreementType
                        && a.AcceptedDate.HasValue)
                    .OrderByDescending(a => a.AcceptedDate)
                    .Select(a => a.AcceptedDate)
                    .FirstAsync();

                organizationAgreementHtml = await _agreementService.RenderOrgAgreementHtmlAsync(agreementType, organization.Name, agreementDate, true);
            }

            return new (string Filename, string HtmlContent)[]
            {
                (organizationAgreementFilename, organizationAgreementHtml),
                (registrationReviewFilename, siteRegistrationReviewHtml)
            }
            .Select(content => (content.Filename, Content: _pdfService.Generate(content.HtmlContent)));
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

        public async Task<bool> UpdateEmailLogStatuses()
        {
            var emailLogs = await _context.EmailLogs
                .Where(e => e.SendType == "CHES"
                    && e.MsgId != null
                    && e.LatestStatus != ChesStatus.Completed.Value)
                .ToListAsync();

            foreach (var email in emailLogs)
            {
                var status = await _chesClient.GetStatusAsync(email.MsgId.Value);
                if (status != null && email.LatestStatus != status)
                {
                    email.LatestStatus = status;
                }
            }

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task SendEnrolleeRenewalEmails()
        {
            var reminderEmailsIntervals = new List<double> { 14, 7, 3, 2, 1, 0 };

            var enrollees = await _context.Enrollees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    e.ExpiryDate
                })
                .Where(e => e.ExpiryDate != null)
                .DecompileAsync()
                .ToListAsync();

            foreach (var enrollee in enrollees)
            {
                var expiryDays = (DateTime.Now.Date - enrollee.ExpiryDate.Value.Date).TotalDays;
                if (reminderEmailsIntervals.Contains(expiryDays))
                {
                    await SendRenewalRequiredAsync(enrollee.Email, enrollee.FirstName, enrollee.LastName, enrollee.ExpiryDate.Value);
                }
                if (expiryDays == -1)
                {
                    await SendRenewalPassedAsync(enrollee.Email, enrollee.FirstName, enrollee.LastName, enrollee.ExpiryDate.Value);
                }
            }
        }

        private async Task SendRenewalRequiredAsync(string email, string firstName, string lastName, DateTimeOffset expiryDate)
        {
            var subject = "PRIME Renewal Required";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.RenewalRequired,
                new EmailParams(firstName, lastName, expiryDate));

            await Send(PRIME_EMAIL, email, subject, body);
        }

        private async Task SendRenewalPassedAsync(string email, string firstName, string lastName, DateTimeOffset expiryDate)
        {
            var subject = "Your PRIME Renewal Date Has Passed";
            var body = await _razorConverterService.RenderTemplateToStringAsync(
                RazorTemplates.Emails.RenewalPassed,
                new EmailParams(firstName, lastName, expiryDate));

            await Send(PRIME_EMAIL, email, subject, body);
        }

        private async Task Send(string from, string to, string subject, string body)
        {
            await Send(from, new[] { to }, new string[0], subject, body, Enumerable.Empty<(string Filename, byte[] Content)>());
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

            if (!PrimeEnvironment.IsProduction)
            {
                subject = $"THE FOLLOWING EMAIL IS A TEST: {subject}";
            }

            if (PrimeEnvironment.ChesApi.Enabled && await _chesClient.HealthCheckAsync())
            {
                var msgId = await SendChes(from, to, cc, subject, body, attachments);

                if (msgId == null)
                {
                    // Ches failed, send using smtp client
                    await SendSmtp(from, to, cc, subject, body, attachments);
                }
            }
            else
            {
                await SendSmtp(from, to, cc, subject, body, attachments);
            }
        }

        private async Task<Guid?> SendChes(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            var msgId = await _chesClient.SendAsync(from, to, cc, subject, body, attachments);

            var emailLog = new EmailLog
            {
                SentTo = string.Join(",", to),
                Cc = string.Join(",", cc),
                Subject = subject,
                Body = body,
                SendType = "CHES",
                MsgId = msgId,
                DateSent = DateTimeOffset.Now
            };

            await CreateEmailLog(emailLog);

            return msgId;
        }

        private async Task SendSmtp(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments)
        {
            await _smtpEmailClient.SendAsync(from, to, cc, subject, body, attachments);

            var emailLog = new EmailLog
            {
                SentTo = string.Join(",", to),
                Cc = string.Join(",", cc),
                Subject = subject,
                Body = body,
                SendType = "SMTP",
                DateSent = DateTimeOffset.Now
            };

            await CreateEmailLog(emailLog);
        }

        private async Task CreateEmailLog(EmailLog emailLog)
        {
            _context.EmailLogs.Add(emailLog);

            await _context.SaveChangesAsync();
        }
    }
}
