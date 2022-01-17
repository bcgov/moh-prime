using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HealthcheckController : PrimeControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;

        protected readonly ILogger _logger;

        public HealthcheckController(IEnrolleeService enrolleeService,
                                     ILogger<HealthcheckController> logger)
        {
            _enrolleeService = enrolleeService;
            _logger = logger;
        }

        // GET: api/Healthcheck
        /// <summary>
        /// Does a healthcheck that queries the enrollee table to wake up the database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task GetHealthcheck()
        {
            _logger.LogDebug("HealthcheckController.GetHealthcheck called ...");
            await _enrolleeService.GetEnrolleeCountAsync();
            _logger.LogDebug("HealthcheckController.GetHealthcheck completed");
        }
    }
}
