using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Prime.Contracts;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class EmailDispatchService : IEmailDispatchService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        protected readonly ILogger _logger;


        public EmailDispatchService(
            IBus bus,
            IMapper mapper,
            ILogger<EmailDispatchService> logger)
        {
            _mapper = mapper;
            _bus = bus;
            _logger = logger;
        }

        public async Task SendBusinessLicenceUploadedAsync(CommunitySite site)
        {
            await SendSiteEmailByType(site, SiteEmailType.BusinessLicenceUploaded);
        }

        public async Task SendRemoteUsersUpdatedAsync(CommunitySite site)
        {
            await SendSiteEmailByType(site, SiteEmailType.RemoteUsersUpdated);
        }

        public async Task SendSiteApprovedHIBCAsync(CommunitySite site)
        {
            await SendSiteEmailByType(site, SiteEmailType.SiteApprovedHIBC);
        }

        public async Task SendSiteApprovedPharmaNetAdministratorAsync(CommunitySite site)
        {
            await SendSiteEmailByType(site, SiteEmailType.SiteApprovedPharmaNetAdministrator);
        }

        public async Task SendSiteApprovedSigningAuthorityAsync(CommunitySite site)
        {
            await SendSiteEmailByType(site, SiteEmailType.SiteApprovedSigningAuthority);
        }

        public async Task SendRemoteUserNotificationsAsync(CommunitySite site, IEnumerable<RemoteUser> remoteUsers)
        {
            await _bus.Send<SendSiteEmail>(_mapper.Map<SendSiteEmailModel>(
                site, opt => opt.AfterMap((src, dest) =>
                {
                    dest.EmailType = SiteEmailType.RemoteUserNotifications;
                    dest.RemoteUserEmails = remoteUsers.Select(u => u.Email);
                })));
        }

        public async Task SendSiteActiveBeforeRegistrationAsync(int siteId, string signingAuthorityEmail)
        {
            await _bus.Send<SendSiteEmail>(new SendSiteEmailModel
            {
                Id = siteId,
                OrganizationSigningAuthorityEmail = signingAuthorityEmail,
                EmailType = SiteEmailType.SiteActiveBeforeRegistration
            });
        }

        public async Task SendSiteRegistrationSubmissionAsync(int siteId, int businessLicenceId, CareSettingType careSettingCode)
        {
            await _bus.Send<SendSiteEmail>(new SendSiteEmailModel
            {
                Id = siteId,
                BusinessLicenceId = businessLicenceId,
                CareSettingCode = (int)careSettingCode,
                EmailType = SiteEmailType.SiteRegistrationSubmission
            });
        }

        public async Task SendSiteReviewedNotificationAsync(int siteId, string note)
        {
            await _bus.Send<SendSiteEmail>(new SendSiteEmailModel
            {
                EmailType = SiteEmailType.SiteReviewedNotification,
                Id = siteId,
                Note = note
            });
        }

        public async Task SendHealthAuthoritySiteRegistrationSubmissionAsync(int siteId)
        {
            await _bus.Send<SendHealthAuthoritySiteEmail>(new { SiteId = siteId });
        }

        public async Task SendEnrolleeRenewalEmails()
        {
            _logger.LogDebug("EmailDispatchService.SendEnrolleeRenewalEmails called ...");
            await _bus.Send<SendEnrolleeEmail>(new { EmailType = EnrolleeEmailType.EnrolleeRenewal });
            _logger.LogDebug("EmailDispatchService.SendEnrolleeRenewalEmails completed");
        }

        public async Task SendEnrolleeUnsignedToaReminderEmails()
        {
            await _bus.Send<SendEnrolleeEmail>(new { EmailType = EnrolleeEmailType.UnsignedToaReminder });
        }

        public async Task SendPaperEnrolmentSubmissionEmailAsync(int enrolleeId)
        {
            await _bus.Send<SendEnrolleeEmail>(new
            {
                EmailType = EnrolleeEmailType.PaperEnrolmentSubmission,
                EnrolleeId = enrolleeId
            });
        }

        public async Task SendReminderEmailAsync(int enrolleeId)
        {
            await _bus.Send<SendEnrolleeEmail>(new
            {
                EmailType = EnrolleeEmailType.Reminder,
                EnrolleeId = enrolleeId
            });
        }

        public async Task SendOrgClaimApprovalNotificationAsync(OrganizationClaim organizationClaim)
        {
            await _bus.Send<SendOrgClaimApprovalNotificationEmail>(new
            {
                organizationClaim.OrganizationId,
                organizationClaim.NewSigningAuthorityId,
                organizationClaim.ProvidedSiteId
            });
        }

        public async Task SendProvisionerLinkAsync(IEnumerable<string> recipientEmails, EnrolmentCertificateAccessToken token, int careSettingCode)
        {
            await _bus.Send<SendProvisionerLinkEmail>(new
            {
                RecipientEmails = recipientEmails,
                token.EnrolleeId,
                TokenUrl = token.FrontendUrl,
                CareSettingCode = careSettingCode
            });
        }

        private async Task SendSiteEmailByType(CommunitySite site, SiteEmailType emailType)
        {
            await _bus.Send<SendSiteEmail>(_mapper.Map<SendSiteEmailModel>(
                site, opt => opt.AfterMap((src, dest) => dest.EmailType = emailType)));
        }
    }
}
