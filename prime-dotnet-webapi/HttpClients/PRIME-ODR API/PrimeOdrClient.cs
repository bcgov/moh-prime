using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prime.Models;

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

        public async Task<(List<PharmanetTransactionLog> Logs, bool ExistsMoreLogs)> RetrieveLatestPharmanetTxLogsAsync()
        {
            _logger.LogDebug(PrimeEnvironment.PrimeOdrApi.Url);
            _logger.LogDebug(PrimeEnvironment.PrimeOdrApi.Username);
            _logger.LogDebug(PrimeEnvironment.PrimeOdrApi.SslCertFilename);

            // Auth header and cert are configured to be injected in Startup.cs
            var response = await _client.GetAsync(PrimeEnvironment.PrimeOdrApi.Url);
            _logger.LogCritical(response.Content.ToString());

            return (null, false);
        }
    }


    public class PrimeOdrClientHandler : HttpClientHandler
    {
        public PrimeOdrClientHandler(ILogger<PrimeOdrClient> logger)
        {
            logger.LogDebug(PrimeEnvironment.PrimeOdrApi.SslCertFilename);
            logger.LogDebug(PrimeEnvironment.PrimeOdrApi.SslCertPassword);

            ClientCertificates.Add(new X509Certificate2(PrimeEnvironment.PrimeOdrApi.SslCertFilename, PrimeEnvironment.PrimeOdrApi.SslCertPassword));
            ClientCertificateOptions = ClientCertificateOption.Manual;
        }
    }
}

