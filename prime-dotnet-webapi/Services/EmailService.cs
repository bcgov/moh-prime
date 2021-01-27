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
        private readonly IChesClient _chesClient;
        private readonly ISmtpEmailClient _smtpEmailClient;
        private readonly IDocumentManagerClient _documentManagerClient;
        private readonly IDocumentAccessTokenService _documentAccessTokenService;
        private readonly ISiteService _siteService;

        public EmailService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IEmailDocumentsService emailDocumentService,
            IEmailRenderingService emailRenderingService,
            IChesClient chesClient,
            ISmtpEmailClient smtpEmailClient,
            IDocumentManagerClient documentManagerClient,
            IDocumentAccessTokenService documentAccessTokenService,
            ISiteService siteService)
            : base(context, httpContext)
        {
            _emailDocumentService = emailDocumentService;
            _emailRenderingService = emailRenderingService;
            _chesClient = chesClient;
            _documentManagerClient = documentManagerClient;
            _documentAccessTokenService = documentAccessTokenService;
            _smtpEmailClient = smtpEmailClient;
            _siteService = siteService;
        }

        public async Task SendReminderEmailAsync(int enrolleeId)
        {
            var enrolleeEmail = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .Select(e => e.Email)
                .SingleOrDefaultAsync();

            var email = await _emailRenderingService.RenderReminderEmailAsync(enrolleeEmail, new LinkedEmailViewModel(PrimeEnvironment.FrontendUrl));
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

            var email = await _emailRenderingService.RenderSiteRegistrationSubmissionEmailAsync(new LinkedEmailViewModel(downloadUrl));
            email.Attachments = await _emailDocumentService.GenerateSiteRegistrationAttachmentsAsync(siteId);
            await Send(email);

            var siteRegReviewPdf = email.Attachments.Single(a => a.Filename == "SiteRegistrationReview.pdf");
            await SaveSiteRegistrationReview(siteId, siteRegReviewPdf);
        }

        public async Task SendRemoteUsersUpdatedAsync(Site site)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(site.Id);
            var viewModel = new RemoteUsersUpdatedEmailViewModel
            {
                SiteStreetAddress = site.PhysicalAddress.Street,
                OrganizationName = site.Organization.Name,
                SitePec = site.PEC,
                RemoteUserNames = site.RemoteUsers.Select(ru => $"{ru.FirstName} {ru.LastName}"),
                DocumentUrl = downloadUrl
            };

            var email = await _emailRenderingService.RenderRemoteUsersUpdatedEmailAsync(viewModel);
            email.Attachments = await _emailDocumentService.GenerateSiteRegistrationAttachmentsAsync(site.Id);
            await Send(email);
        }

        public async Task SendRemoteUserNotificationsAsync(Site site, IEnumerable<RemoteUser> remoteUsers)
        {
            var recipients = remoteUsers.Select(ru => ru.Email);
            var viewModel = new RemoteUserNotificationEmailViewModel
            {
                OrganizationName = site.Organization.Name,
                SiteStreetAddress = site.PhysicalAddress.Street,
                SiteCity = site.PhysicalAddress.City,
                PrimeUrl = PrimeEnvironment.FrontendUrl
            };

            var email = await _emailRenderingService.RenderRemoteUserNotificationEmailAsync(recipients.First(), viewModel);
            await Send(email);

            foreach (var recipient in recipients.Skip(1))
            {
                email.To = new[] { recipient };
                await Send(email);
            }
        }

        public async Task SendBusinessLicenceUploadedAsync(Site site)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(site.Id);

            var email = await _emailRenderingService.RenderBusinessLicenceUploadedEmailAsync(site.Adjudicator.Email, new LinkedEmailViewModel(downloadUrl));
            await Send(email);
        }

        public async Task SendSiteApprovedPharmaNetAdministratorAsync(Site site)
        {
            var viewModel = new SiteApprovalEmailViewModel
            {
                DoingBusinessAs = site.DoingBusinessAs,
                Pec = site.PEC
            };

            var email = await _emailRenderingService.RenderSiteApprovedPharmaNetAdministratorEmailAsync(site.AdministratorPharmaNet.Email, viewModel);
            await Send(email);
        }

        public async Task SendSiteApprovedSigningAuthorityAsync(Site site)
        {
            var viewModel = new SiteApprovalEmailViewModel
            {
                DoingBusinessAs = site.DoingBusinessAs,
                Pec = site.PEC
            };

            var email = await _emailRenderingService.RenderSiteApprovedSigningAuthorityEmailAsync(site.Provisioner.Email, viewModel);
            await Send(email);
        }

        public async Task SendSiteApprovedHIBCAsync(Site site)
        {
            var viewModel = new SiteApprovalEmailViewModel
            {
                DoingBusinessAs = site.DoingBusinessAs,
                Pec = site.PEC
            };

            var email = await _emailRenderingService.RenderSiteApprovedHibcEmailAsync(viewModel);
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
                    var email = await _emailRenderingService.RenderRenewalRequiredEmailAsync(enrollee.Email, new EnrolleeRenewalEmailViewModel(enrollee.FirstName, enrollee.LastName, enrollee.ExpiryDate.Value));
                    await Send(email);
                }
                if (expiryDays == -1)
                {
                    var email = await _emailRenderingService.RenderRenewalPassedEmailAsync(enrollee.Email, new EnrolleeRenewalEmailViewModel(enrollee.FirstName, enrollee.LastName, enrollee.ExpiryDate.Value));
                    await Send(email);
                }
            }
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
