using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.HttpClients.Mail;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendReminderEmailAsync(int enrolleeId);
        Task SendProvisionerLinkAsync(IEnumerable<string> recipientEmails, EnrolmentCertificateAccessToken token, int careSettingCode);
        Task SendSiteRegistrationAsync(Site site);
        Task SendRemoteUsersUpdatedAsync(Site site);
        Task SendRemoteUserNotificationsAsync(Site site, IEnumerable<RemoteUser> remoteUsers);
        Task SendEnrolleeRenewalEmails();
        Task SendBusinessLicenceUploadedAsync(Site site);
        Task SendSiteApprovedPharmaNetAdministratorAsync(Site site);
        Task SendSiteApprovedSigningAuthorityAsync(Site site);
        Task SendSiteApprovedHIBCAsync(Site site);


        Task Send(Email email);
        Task CreateChesEmailLog(Email email, Guid? msgId);
        Task CreateSmtpEmailLog(Email email);
        Task<bool> UpdateEmailLogStatuses();
    }
}
