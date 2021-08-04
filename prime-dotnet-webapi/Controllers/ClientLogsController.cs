using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Services;
using Prime.Models;
using Prime.ViewModels;
using System;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/client-logs")]
    [ApiController]
    public class ClientLogsController : PrimeControllerBase
    {
        private readonly IClientLogService _logService;

        public ClientLogsController(IClientLogService logService)
        {
            _logService = logService;
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
            catch (Exception)
            {
                // Do Nothing
            }
            return Ok(logId);
        }
    }
}
