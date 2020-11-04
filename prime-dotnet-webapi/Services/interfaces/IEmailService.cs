using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendReminderEmailAsync(int enrolleeId);

        Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null);

        Task SendSiteRegistrationAsync(Site site);

        Task SendRemoteUsersUpdatedAsync(Site site);

        Task SendRemoteUsersNotificationAsync(Site site, IEnumerable<RemoteUser> remoteUsers);

        Task<string> GetPharmaNetProvisionerEmailAsync(string provisionerName);

        Task<IEnumerable<string>> GetPharmaNetProvisionerNamesAsync();
    }
}
