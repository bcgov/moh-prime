using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendReminderEmailAsync(Enrollee enrollee);

        Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null);
    }
}
