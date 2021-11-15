using System.Threading.Tasks;

namespace Prime.HttpClients.Mail
{
    public interface ISmtpEmailClient
    {
        Task SendAsync(Email email);
    }
}
