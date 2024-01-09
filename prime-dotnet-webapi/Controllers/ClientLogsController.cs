using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Services;
using Prime.ViewModels;
using System;
using Microsoft.Extensions.Logging;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/client-logs")]
    [ApiController]
    public class ClientLogsController : PrimeControllerBase
    {
        private readonly IClientLogService _logService;

        private readonly ILogger<ClientLogsController> _logger;


        public ClientLogsController(IClientLogService logService,
            ILogger<ClientLogsController> logger)
        {
            _logService = logService;
            _logger = logger;
        }

        // POST /api/client-logs
        /// <summary>
        /// Creates a client log
        /// </summary>
        [HttpPost("", Name = nameof(CreateLog))]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateLog(ClientLogViewModel log)
        {
            var logId = 0;
            try
            {
                logId = await _logService.CreateLogAsync(log);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while logging {log}", e);
            }
            return Ok(logId);
        }
    }
}
