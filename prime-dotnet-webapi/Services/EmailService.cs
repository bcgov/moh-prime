using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Prime.Contracts;
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

            var email = await _emailRenderingService.RenderReminderEmailAsync(enrolleeEmail, new LinkedEmailViewModel(PrimeConfiguration.Current.FrontendUrl));
            await Send(email);
        }

        public async Task SendProvisionerLinkAsync(SendProvisionerLinkEmail model)
        {
            var enrolleeDto = await _context.Enrollees
                .Where(e => e.Id == model.EnrolleeId)
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
                TokenUrl = model.TokenUrl,
                ExpiresInDays = EnrolmentCertificateAccessToken.Lifespan.Days
            };

            var email = await _emailRenderingService.RenderProvisionerLinkEmailAsync(model.RecipientEmails, enrolleeDto.Email, (CareSettingType)model.CareSettingCode, viewModel);
            await Send(email);
        }

        public async Task SendSiteRegistrationSubmissionAsync(SendSiteEmail site)
        {
            var downloadUrl = await _emailDocumentService.GetBusinessLicenceDownloadLink(site.BusinessLicenceId);

            var email = await _emailRenderingService.RenderSiteRegistrationSubmissionEmailAsync(new LinkedEmailViewModel(downloadUrl), (CareSettingType)site.CareSettingCode);
            email.Attachments = await _emailDocumentService.GenerateSiteRegistrationSubmissionAttachmentsAsync(site.Id);
            await Send(email);

            var siteRegReviewPdf = email.Attachments.Single(a => a.Filename == "SiteRegistrationReview.pdf");
            await _emailDocumentService.SaveSiteRegistrationReview(site.Id, siteRegReviewPdf);
        }

        public async Task SendHealthAuthoritySiteRegistrationSubmissionAsync(int healthAuthoritySiteId)
        {
            var email = await _emailRenderingService.RenderSiteRegistrationSubmissionEmailAsync(new LinkedEmailViewModel(null), CareSettingType.HealthAuthority);
            var attachment = await _emailDocumentService.GenerateHealthAuthorityRegistrationReviewAttachmentAsync(healthAuthoritySiteId);
            email.Attachments = new[] { attachment };
            await Send(email);

            await _emailDocumentService.SaveSiteRegistrationReview(healthAuthoritySiteId, attachment);
        }

        public async Task SendSiteReviewedNotificationAsync(SendSiteEmail site)
        {
            var viewModel = await _context.Sites
                .Where(s => s.Id == site.Id)
                .Select(s => new SiteReviewedEmailViewModel
                {
                    Note = site.Note,
                    Pec = s.PEC
                })
                .SingleAsync();

            var email = await _emailRenderingService.RenderSiteReviewedNotificationEmailAsync(viewModel);
            await Send(email);
        }

        public async Task SendRemoteUsersUpdatedAsync(SendSiteEmail site)
        {
            var downloadUrl = await _emailDocumentService.GetBusinessLicenceDownloadLink(site.Id);
            var viewModel = new RemoteUsersUpdatedEmailViewModel
            {
                SiteStreetAddress = site.PhysicalAddressStreet,
                OrganizationName = site.OrganizationName,
                SitePec = site.PEC,
                RemoteUserNames = site.RemoteUserNames,
                DocumentUrl = downloadUrl
            };

            var email = await _emailRenderingService.RenderRemoteUsersUpdatedEmailAsync(viewModel);
            email.Attachments = await _emailDocumentService.GenerateSiteRegistrationSubmissionAttachmentsAsync(site.Id);
            await Send(email);
        }

        public async Task SendRemoteUserNotificationsAsync(SendSiteEmail site)
        {
            if (!site.RemoteUserEmails.Any())
            {
                return;
            }

            var viewModel = new RemoteUserNotificationEmailViewModel
            {
                OrganizationName = site.OrganizationName,
                SiteStreetAddress = site.PhysicalAddressStreet,
                SiteCity = site.PhysicalAddressCity,
                PrimeUrl = PrimeConfiguration.Current.FrontendUrl
            };

            var email = await _emailRenderingService.RenderRemoteUserNotificationEmailAsync(site.RemoteUserEmails.First(), viewModel);
            await Send(email);

            foreach (var recipient in site.RemoteUserEmails.Skip(1))
            {
                email.To = new[] { recipient };
                await Send(email);
            }
        }

        public async Task SendBusinessLicenceUploadedAsync(SendSiteEmail site)
        {
            var downloadUrl = await _emailDocumentService.GetBusinessLicenceDownloadLink(site.BusinessLicenceId);

            var email = await _emailRenderingService.RenderBusinessLicenceUploadedEmailAsync(site.AdjudicatorEmail, new LinkedEmailViewModel(downloadUrl));
            await Send(email);
        }

        public async Task SendSiteApprovedPharmaNetAdministratorAsync(SendSiteEmail site)
        {
            var viewModel = new SiteApprovalEmailViewModel
            {
                DoingBusinessAs = site.DoingBusinessAs,
                Pec = site.PEC
            };

            var email = await _emailRenderingService.RenderSiteApprovedPharmaNetAdministratorEmailAsync(site.AdministratorPharmaNetEmail, viewModel);
            await Send(email);
        }

        public async Task SendSiteApprovedSigningAuthorityAsync(SendSiteEmail site)
        {
            var viewModel = new SiteApprovalEmailViewModel
            {
                DoingBusinessAs = site.DoingBusinessAs,
                Pec = site.PEC
            };

            var email = await _emailRenderingService.RenderSiteApprovedSigningAuthorityEmailAsync(site.ProvisionerEmail, viewModel);
            await Send(email);
        }

        public async Task SendSiteActiveBeforeRegistrationAsync(SendSiteEmail site)
        {
            var viewModel = await _context.Sites
            .Where(s => s.Id == site.Id)
            .Select(s => new SiteActiveBeforeRegistrationEmailViewModel
            {
                Pec = s.PEC
            })
            .SingleAsync();
            var email = await _emailRenderingService.RenderSiteActiveBeforeRegistrationEmailAsync(site.OrganizationSigningAuthorityEmail, viewModel);
            await Send(email);
        }

        public async Task SendSiteApprovedHIBCAsync(SendSiteEmail site)
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

        public async Task SendOrgClaimApprovalNotificationAsync(SendOrgClaimApprovalNotificationEmail orgClaim)
        {
            var orgName = await _context.Organizations
                .Where(o => o.Id == orgClaim.OrganizationId)
                .Select(o => o.Name)
                .SingleAsync();

            var newSigningAuthorityEmail = await _context.Parties
                .Where(p => p.Id == orgClaim.NewSigningAuthorityId)
                .Select(p => p.Email)
                .SingleAsync();

            var viewModel = new OrgClaimApprovalNotificationViewModel
            {
                OrganizationName = orgName,
                ProvidedSiteId = orgClaim.ProvidedSiteId
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

        public async Task SendPaperEnrolmentSubmissionEmailAsync(int enrolleeId)
        {
            var enrolleeDto = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .Select(e => new
                {
                    e.Email,
                    e.GPID
                })
                .SingleOrDefaultAsync();

            var email = await _emailRenderingService.RenderPaperEnrolleeSubmissionEmail(enrolleeDto.Email, new PaperEnrolleeSubmissionEmailViewModel(enrolleeDto.GPID));
            await Send(email);
        }

        private async Task Send(Email email)
        {
            if (!PrimeConfiguration.IsProduction())
            {
                email.Subject = $"THE FOLLOWING EMAIL IS A TEST: {email.Subject}";
            }

            if (PrimeConfiguration.Current.ChesApi.Enabled && await _chesClient.HealthCheckAsync())
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
