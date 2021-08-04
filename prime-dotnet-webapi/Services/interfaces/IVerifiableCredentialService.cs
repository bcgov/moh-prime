using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Prime.Models;
using Prime.Models.VerifiableCredentials;

namespace Prime.Services
{
    public interface IVerifiableCredentialService
    {
        Task<bool> CreateConnectionAsync(Enrollee enrollee);
        Task<bool> WebhookAsync(WebhookData data, string topic);
        Task<bool> RevokeCredentialsAsync(int enrolleeId);
    }
}
