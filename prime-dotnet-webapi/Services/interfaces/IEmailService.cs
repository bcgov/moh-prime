using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        void SendReminderEmail(Enrollee enrollee);

        void SendProvisionerLink(string provisionerEmail, EnrolmentCertificateAccessToken token);
    }
}
