using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HealthcheckController : ControllerBase
    {
        private readonly IEnrolleeService _enrolleeService;

        public HealthcheckController(IEnrolleeService enrolleeService)
        {
            _enrolleeService = enrolleeService;
        }

        // GET: api/Healthcheck
        /// <summary>
        /// Does a healthcheck that queries the enrollee table to wake up the database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task GetHealthcheck()
        {
            await _enrolleeService.GetEnrolleeCountAsync();
        }
    }
}
