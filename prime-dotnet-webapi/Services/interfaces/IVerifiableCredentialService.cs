using System;
using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IVerifiableCredentialService
    {
        Task<bool> create(Object data, WebhookTopic topic);
    }
}
