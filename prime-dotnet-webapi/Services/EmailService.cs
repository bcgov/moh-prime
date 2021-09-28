using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Prime.HttpClients.Mail;
using Prime.HttpClients.Mail.ChesApiDefinitions;
using Prime.Models;
using Prime.Services.EmailInternal;
using Prime.ViewModels.Emails;

namespace Prime.Services
{
    public class EmailService : BaseService, IEmailService
    {
        private static class SendType
        {
            public const string Ches = "CHES";
            public const string Smtp = "SMTP";
        }

        private readonly IChesClient _chesClient;
        private readonly IEmailDocumentsService _emailDocumentService;
        private readonly IEmailRenderingService _emailRenderingService;
        private readonly ISmtpEmailClient _smtpEmailClient;

        public EmailService(
            ApiDbContext context,
            ILogger<EmailService> logger,
            IChesClient chesClient,
            IEmailDocumentsService emailDocumentService,
            IEmailRenderingService emailRenderingService,
            ISmtpEmailClient smtpEmailClient)
            : base(context, logger)
        {
            _chesClient = chesClient;
            _emailDocumentService = emailDocumentService;
            _emailRenderingService = emailRenderingService;
            _smtpEmailClient = smtpEmailClient;
        }

        public async Task SendReminderEmailAsync(int enrolleeId)
        {
            var enrolleeEmail = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .Select(e => e.Email)
                .SingleOrDefaultAsync();

            var email = await _emailRenderingService.RenderReminderEmailAsync(enrolleeEmail, new LinkedEmailViewModel(PrimeEnvironment.Current.FrontendUrl));
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

        public async Task SendSiteRegistrationSubmissionAsync(int siteId, int businessLicenceId, CareSettingType careSettingCode)
        {
            var downloadUrl = await _emailDocumentService.GetBusinessLicenceDownloadLink(businessLicenceId);

            var email = await _emailRenderingService.RenderSiteRegistrationSubmissionEmailAsync(new LinkedEmailViewModel(downloadUrl), careSettingCode);
            email.Attachments = await _emailDocumentService.GenerateSiteRegistrationSubmissionAttachmentsAsync(siteId);
            await Send(email);

            var siteRegReviewPdf = email.Attachments.Single(a => a.Filename == "SiteRegistrationReview.pdf");
            await _emailDocumentService.SaveSiteRegistrationReview(siteId, siteRegReviewPdf);
        }

        public async Task SendSiteReviewedNotificationAsync(int siteId, string note)
        {

            var viewModel = await _context.Sites
                .Where(s => s.Id == siteId)
                .Select(s => new SiteReviewedEmailViewModel
                {
                    Note = note,
                    Pec = s.PEC
                })
                .SingleAsync();

            var email = await _emailRenderingService.RenderSiteReviewedNotificationEmailAsync(viewModel);
            await Send(email);
        }

        public async Task SendRemoteUsersUpdatedAsync(Site site)
        {
            var downloadUrl = await _emailDocumentService.GetBusinessLicenceDownloadLink(site.Id);
            var viewModel = new RemoteUsersUpdatedEmailViewModel
            {
                SiteStreetAddress = site.PhysicalAddress.Street,
                OrganizationName = site.Organization.Name,
                SitePec = site.PEC,
                RemoteUserNames = site.RemoteUsers.Select(ru => $"{ru.FirstName} {ru.LastName}"),
                DocumentUrl = downloadUrl
            };

            var email = await _emailRenderingService.RenderRemoteUsersUpdatedEmailAsync(viewModel);
            email.Attachments = await _emailDocumentService.GenerateSiteRegistrationSubmissionAttachmentsAsync(site.Id);
            await Send(email);
        }

        public async Task SendRemoteUserNotificationsAsync(Site site, IEnumerable<RemoteUser> remoteUsers)
        {
            if (!remoteUsers.Any())
            {
                return;
            }

            var recipients = remoteUsers.Select(ru => ru.Email);
            var viewModel = new RemoteUserNotificationEmailViewModel
            {
                OrganizationName = site.Organization.Name,
                SiteStreetAddress = site.PhysicalAddress.Street,
                SiteCity = site.PhysicalAddress.City,
                PrimeUrl = PrimeEnvironment.Current.FrontendUrl
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
            var downloadUrl = await _emailDocumentService.GetBusinessLicenceDownloadLink(site.BusinessLicence.Id);

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
                var expiryDays = (enrollee.ExpiryDate.Value.Date - DateTime.Now.Date).TotalDays;
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

        public async Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim)
        {
            var orgName = await _context.Organizations
                .Where(o => o.Id == organizationClaim.OrganizationId)
                .Select(o => o.Name)
                .SingleAsync();

            var newSigningAuthorityEmail = await _context.Parties
                .Where(p => p.Id == organizationClaim.NewSigningAuthorityId)
                .Select(p => p.Email)
                .SingleAsync();

            var viewModel = new OrgClaimApprovalNotificationViewModel
            {
                OrganizationName = orgName,
                ProvidedSiteId = organizationClaim.ProvidedSiteId
            };

            var email = await _emailRenderingService.RenderOrgClaimApprovalNotificationEmailAsync(newSigningAuthorityEmail, viewModel);
            await Send(email);
        }

        public async Task<int> UpdateEmailLogStatuses(int limit)
        {
            Expression<Func<EmailLog, bool>> predicate = log =>
                log.SendType == SendType.Ches
                && log.MsgId != null
                && log.LatestStatus != ChesStatus.Completed;

            var totalCount = await _context.EmailLogs
                .Where(predicate)
                .CountAsync();

            var emailLogs = await _context.EmailLogs
                .Where(predicate)
                .OrderBy(e => e.UpdatedTimeStamp)
                .Take(limit)
                .ToListAsync();

            foreach (var email in emailLogs)
            {
                var status = await _chesClient.GetStatusAsync(email.MsgId.Value);
                if (status != null && email.LatestStatus != status)
                {
                    email.LatestStatus = status;
                }
            }
            await _context.SaveChangesAsync();

            return totalCount;
        }

        private async Task Send(Email email)
        {
            if (!PrimeEnvironment.Current.IsProduction)
            {
                email.Subject = $"THE FOLLOWING EMAIL IS A TEST: {email.Subject}";
            }

            if (PrimeEnvironment.Current.ChesApi.Enabled && await _chesClient.HealthCheckAsync())
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
