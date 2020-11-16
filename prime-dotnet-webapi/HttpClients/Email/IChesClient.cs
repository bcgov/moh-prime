using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Prime.HttpClients
{
    public interface IChesClient
    {
        Task<Guid> SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<(string Filename, byte[] Content)> attachments);
        Task<bool> HealthCheckAsync();
    }
}
