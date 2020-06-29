using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Prime.Models;

namespace Prime.Services
{
    public interface IVerifiableCredentialService
    {
        Task<JObject> CreateConnectionAsync(Enrollee enrollee);
        Task<bool> WebhookAsync(JObject data, string topic);
    }
}
