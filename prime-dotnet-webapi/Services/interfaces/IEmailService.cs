using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendReminderEmailAsync(Enrollee enrollee);

        Task SendProvisionerLinkAsync(string provisionerEmail, EnrolmentCertificateAccessToken token);
    }
}
