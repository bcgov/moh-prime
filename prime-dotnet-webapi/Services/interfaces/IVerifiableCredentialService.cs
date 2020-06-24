using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Prime.Services
{
    public interface IVerifiableCredentialService
    {
        Task<JObject> CreateConnection();
        Task<JObject> SendCredential(String gpid);
        Task<bool> Create(JObject data, string topic);
    }
}
