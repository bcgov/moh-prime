using Prime.Models;

namespace Prime.Services
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string body);

        void SendReminderEmail(Enrollee enrollee);
    }
}
