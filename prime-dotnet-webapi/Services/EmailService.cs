using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DelegateDecompiler.EntityFrameworkCore;

using Prime.Models;
using Prime.HttpClients;
using Prime.Services.Razor;
using Prime.Services.EmailInternal;
using Prime.HttpClients.Mail;
using Prime.ViewModels.Emails;
using Prime.HttpClients.Mail.ChesApiDefinitions;
using Prime.Models.Documents;

namespace Prime.Services
{
    public class EmailService : BaseService, IEmailService
    {
        private static class SendType
        {
            public const string Ches = "CHES";
            public const string Smtp = "SMTP";
        }

        private readonly IEmailDocumentsService _emailDocumentService;
        private readonly IEmailRenderingService _emailRenderingService;
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
            IEmailDocumentsService emailDocumentService,
            IEmailRenderingService emailRenderingService,
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
            _emailDocumentService = emailDocumentService;
            _emailRenderingService = emailRenderingService;
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

        public async Task SendReminderEmailAsync(int enrolleeId)
        {
            var enrolleeEmail = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .Select(e => e.Email)
                .SingleOrDefaultAsync();

            var email = await _emailRenderingService.RenderReminderEmailAsync(enrolleeEmail, new LinkedEmailViewModel { Url = PrimeEnvironment.FrontendUrl });
            await Send(email);
        }

        public async Task SendProvisionerLinkAsync(IEnumerable<string> emails, EnrolmentCertificateAccessToken token, int careSettingCode)
        {
            var enrolleeDto = await _context.Enrollees
                .Where(e => e.Id == token.EnrolleeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Email
                })
                .SingleAsync();

            var viewModel = new ProvisionerAccessEmailViewModel
            {
                EnrolleeFullName = $"{enrolleeDto.FirstName} {enrolleeDto.LastName}",
                TokenUrl = token.FrontendUrl,
                ExpiresInDays = EnrolmentCertificateAccessToken.Lifespan.Days
            };

            var email = await _emailRenderingService.RenderProvisionerLinkEmailAsync(emails, enrolleeDto.Email, (CareSettingType)careSettingCode, viewModel);
            await Send(email);
        }

        public async Task SendSiteRegistrationSubmissionAsync(int siteId)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(siteId);

            var email = new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: "PRIME Site Registration Submission",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteRegistrationSubmission, new SiteRegistrationSubmissionEmailViewModel { DocumentUrl = downloadUrl }),
                attachments: await _emailRenderingService.GenerateSiteRegistrationAttachmentsAsync(siteId)
            );
            await Send(email);

            var siteRegReviewPdf = email.Attachments.Single(a => a.Filename == "SiteRegistrationReview.pdf");
            await SaveSiteRegistrationReview(siteId, siteRegReviewPdf);
        }

        public async Task SendRemoteUsersUpdatedAsync(Site site)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(site.Id);

            var email = new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: "Remote Practioners Added",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RemoteUsersUpdated, new EmailParams(site, downloadUrl)),
                attachments: await _emailRenderingService.GenerateSiteRegistrationAttachmentsAsync(site.Id)
            );
            await Send(email);
        }

        public async Task SendRemoteUserNotificationsAsync(Site site, IEnumerable<RemoteUser> remoteUsers)
        {
            var body = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RemoteUserNotification, new EmailParams(site));

            foreach (var remoteUser in remoteUsers)
            {
                var email = new Email
                (
                    from: PrimeEmail,
                    to: remoteUser.Email,
                    subject: "Remote Practitioner Notification",
                    body: body
                );
                await Send(email);
            }
        }

        public async Task SendBusinessLicenceUploadedAsync(Site site)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(site.Id);

            var email = site.Adjudicator.Email
            await Send(email);
        }

        public async Task SendSiteApprovedPharmaNetAdministratorAsync(Site site)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: site.AdministratorPharmaNet.Email,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedPharmaNetAdministratorEmailTemplate, new EmailParams(site))
            );
            await Send(email);
        }

        public async Task SendSiteApprovedSigningAuthorityAsync(Site site)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: site.Provisioner.Email,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedSigningAuthorityEmailTemplate, new EmailParams(site))
            );
            await Send(email);
        }

        public async Task SendSiteApprovedHIBCAsync(Site site)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedHibcEmailTemplate, new EmailParams(site))
            );
            await Send(email);
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

        private async Task SaveSiteRegistrationReview(int siteId, Pdf pdf)
        {
            var documentGuid = await _documentManagerClient.SendFileAsync(new MemoryStream(pdf.Data), pdf.Filename, $"sites/{siteId}/site_registration_reviews");

            _context.SiteRegistrationReviewDocuments.Add(new SiteRegistrationReviewDocument(siteId, documentGuid, pdf.Filename));
            await _context.SaveChangesAsync();
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

        private async Task SendRenewalRequiredAsync(string enrolleeEmail, string firstName, string lastName, DateTimeOffset expiryDate)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: enrolleeEmail,
                subject: "PRIME Renewal Required",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RenewalRequired, new EmailParams(firstName, lastName, expiryDate))
            );
            await Send(email);
        }

        private async Task SendRenewalPassedAsync(string enrolleeEmail, string firstName, string lastName, DateTimeOffset expiryDate)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: enrolleeEmail,
                subject: "Your PRIME Renewal Date Has Passed",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RenewalPassed, new EmailParams(firstName, lastName, expiryDate))
            );
            await Send(email);
        }

        public async Task<bool> UpdateEmailLogStatuses()
        {
            var emailLogs = await _context.EmailLogs
                .Where(e => e.SendType == SendType.Ches
                    && e.MsgId != null
                    && e.LatestStatus != ChesStatus.Completed)
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

        private async Task Send(Email email)
        {
            if (!PrimeEnvironment.IsProduction)
            {
                email.Subject = $"THE FOLLOWING EMAIL IS A TEST: {email.Subject}";
            }

            if (PrimeEnvironment.ChesApi.Enabled && await _chesClient.HealthCheckAsync())
            {
                var msgId = await _chesClient.SendAsync(email);
                await CreateEmailLog(email, SendType.Ches, msgId);

                if (msgId != null)
                {
                    return;
                }
            }

            // Allways fall back to smtp
            await _smtpEmailClient.SendAsync(email);
            await CreateEmailLog(email, SendType.Smtp);
        }

        private async Task CreateEmailLog(Email email, string sendType, Guid? msgId = null)
        {
            _context.EmailLogs.Add(EmailLog.FromEmail(email, sendType, msgId));

            await _context.SaveChangesAsync();
        }
    }
}
