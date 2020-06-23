using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Prime.Services
{
    public interface IVerifiableCredentialService
    {
        Task<JObject> CreateConnection();
        Task<bool> create(Object data, WebhookTopic topic);
    }
}
