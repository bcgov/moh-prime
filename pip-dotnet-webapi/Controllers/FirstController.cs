using Microsoft.AspNetCore.Mvc;

namespace Pidp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : PidpControllerBase
    {
        // GET: api/First
        /// <summary>
        /// Gets a non-usable result. Used to test connection to database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSomething()
        {
            return Ok("worked");
        }
    }
}
