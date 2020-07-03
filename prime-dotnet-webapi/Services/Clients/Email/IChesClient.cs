using System.Threading.Tasks;
using System.Collections.Generic;

namespace Prime.Services
{
    public interface IChesClient
    {
        Task SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments);
        Task<bool> HealthCheckAsync();
    }
}
