using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Models.Api;
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
        [ProducesResponseType(typeof(ApiOkResponse<Enrollee>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Enrollee>> GetHealthcheck()
        {
            var enrollees = await _enrolleeService.GetEnrolleesAsync();
            var empty = new Enrollee();
            return Ok(new ApiOkResponse<Enrollee>(empty));
        }
    }
}
