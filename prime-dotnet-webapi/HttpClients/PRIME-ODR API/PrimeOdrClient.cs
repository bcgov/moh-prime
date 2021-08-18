using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Prime.HttpClients
{
    public class PrimeOdrClient : BaseClient, IPrimeOdrClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public PrimeOdrClient(
            HttpClient client,
            ILogger<PrimeOdrClient> logger) : base(PropertySerialization.CamelCase)
        {
            _client = client;
            _logger = logger;
        }


        public async Task<long> RetrieveLatestPharmanetTxLogsAsync()
        {
            _logger.LogDebug(PrimeEnvironment.PrimeOdrApi.Url);
            _logger.LogDebug(PrimeEnvironment.PrimeOdrApi.Username);
            _logger.LogDebug(PrimeEnvironment.PrimeOdrApi.SslCertFilename);

            // Auth header and cert are configured to be injected in Startup.cs
            await _client.GetAsync(PrimeEnvironment.PrimeOdrApi.Url);

            // TODO:
            return -123L;
        }
    }


    public class PrimeOdrClientHandler : HttpClientHandler
    {
        public PrimeOdrClientHandler()
        {
            ClientCertificates.Add(new X509Certificate2(PrimeEnvironment.PrimeOdrApi.SslCertFilename, PrimeEnvironment.PrimeOdrApi.SslCertPassword));
            ClientCertificateOptions = ClientCertificateOption.Manual;
        }
    }
}

