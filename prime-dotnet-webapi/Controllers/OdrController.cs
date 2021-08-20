using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prime.HttpClients;
using Prime.Services;

namespace Prime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdrController : PrimeControllerBase
    {
        private readonly IPrimeOdrClient _primeOdrClient;

        private readonly IPharmanetTransactionLogService _pnetTransactionLogService;

        private readonly ILogger _logger;


        public OdrController(
            IPrimeOdrClient primeOdrClient,
            IPharmanetTransactionLogService pnetTransactionLogService,
            ILogger<OdrController> logger)
        {
            _primeOdrClient = primeOdrClient;
            _pnetTransactionLogService = pnetTransactionLogService;
            _logger = logger;
        }


        /// <summary>
        /// Expose method invokable via OpenShift Cron
        /// </summary>
        [HttpPost("retrieve-logs", Name = nameof(RetrievePharmanetTxLogs))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RetrievePharmanetTxLogs()
        {
            var result = await _primeOdrClient.RetrieveLatestPharmanetTxLogsAsync(_pnetTransactionLogService.GetMostRecentTransactionId());
            _logger.LogInformation($"{result.Logs.Count} log items retrieved");
            _logger.LogInformation($"Do more logs exist?  {result.ExistsMoreLogs}");
            return Ok(result);
        }
    }
}
