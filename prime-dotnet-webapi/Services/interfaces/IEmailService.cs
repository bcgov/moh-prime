using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        Task SendReminderEmailAsync(Enrollee enrollee);

        Task SendProvisionerLinkAsync(string provisionerName, string provisionerEmail, EnrolmentCertificateAccessToken token);

        Task SendOfficeManagerEmailAsync(string[] officeManagerEmails, EnrolmentCertificateAccessToken token);
    }
}
