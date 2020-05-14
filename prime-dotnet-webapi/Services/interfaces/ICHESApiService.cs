using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;

namespace Prime.Services
{
    public interface ICHESApiService
    {
        Task SendAsync(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body);
        Task<bool> HealthCheckAsync();
    }
}
