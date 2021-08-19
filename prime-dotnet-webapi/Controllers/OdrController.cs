using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prime.HttpClients;

namespace Prime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdrController : PrimeControllerBase
    {
        private readonly IPrimeOdrClient _primeOdrClient;

        private readonly ILogger _logger;


        public OdrController(
            IPrimeOdrClient primeOdrClient,
            ILogger<OdrController> logger)
        {
            _primeOdrClient = primeOdrClient;
            _logger = logger;
        }


        /// <summary>
        /// Expose method invokable via OpenShift Cron
        /// </summary>
        [HttpPost("retrieve-logs", Name = nameof(RetrievePharmanetTxLogs))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RetrievePharmanetTxLogs()
        {
            var result = await _primeOdrClient.RetrieveLatestPharmanetTxLogsAsync();
            _logger.LogDebug(@"{result.Logs.Count} log items retrieved");
            _logger.LogDebug(@"Do more logs exist?  {result.ExistsMoreLogs}");
            return Ok(result);
        }
    }
}
