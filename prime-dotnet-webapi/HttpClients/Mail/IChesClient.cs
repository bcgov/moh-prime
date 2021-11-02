using System.Threading.Tasks;
using System;

namespace Prime.HttpClients.Mail
{
    public interface IChesClient
    {
        Task<Guid?> SendAsync(Email email);
        Task<string> GetStatusAsync(Guid msgId);
        Task<bool> HealthCheckAsync();
    }
}
