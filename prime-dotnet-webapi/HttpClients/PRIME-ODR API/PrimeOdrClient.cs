using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Prime.Models;
using Flurl;

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
            _logger.LogInformation(PrimeEnvironment.PrimeOdrApi.Url);
            _logger.LogInformation(PrimeEnvironment.PrimeOdrApi.Username);
            _logger.LogInformation(PrimeEnvironment.PrimeOdrApi.SslCertFilename);

            // Auth header and cert are configured to be injected in Startup.cs
            var response = await _client.GetAsync(PrimeEnvironment.PrimeOdrApi.Url.SetQueryParams(
                new
                {
                    requestUUID = System.Guid.NewGuid().ToString(),
                    clientName = "PRIME-dev",
                    lastTxnId = 0,
                    fetchSize = 1000
                }));
            _logger.LogInformation(await response.Content.ReadAsStringAsync());

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

