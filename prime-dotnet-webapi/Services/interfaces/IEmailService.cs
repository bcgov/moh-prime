using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;

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
    }
}
