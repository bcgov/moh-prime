using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Services;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : PrimeControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        // POST /api/logs/error
        /// <summary>
        /// Creates an error log
        /// </summary>
        [HttpPost("error", Name = nameof(CreateErrorLog))]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateErrorLog(FromBodyText log)
        {
            await _logService.CreateLogAsync(LogType.Error, log);
            return NoContent();
        }
    }
}
