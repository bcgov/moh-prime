using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prime.HttpClients
{
    public interface ISmtpEmailClient
    {
        Task SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments);
    }
}
