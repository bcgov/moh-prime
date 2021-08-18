using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prime.HttpClients;

namespace Prime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdrController : PrimeControllerBase
    {
        private readonly IPrimeOdrClient _primeOdrClient;

        public OdrController(IPrimeOdrClient primeOdrClient)
        {
            _primeOdrClient = primeOdrClient;
        }


        /// <summary>
        /// Expose method invokable via OpenShift Cron
        /// </summary>
        [HttpPost("retrieve-logs", Name = nameof(RetrievePharmanetTxLogs))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RetrievePharmanetTxLogs()
        {
            var lastTxId = await _primeOdrClient.RetrieveLatestPharmanetTxLogsAsync();

            return Ok(lastTxId);
        }
    }
}
