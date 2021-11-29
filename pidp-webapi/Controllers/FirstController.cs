using Microsoft.AspNetCore.Mvc;

using Pidp.Data;
using Pidp.Features;
using Pidp.Models.Lookups;

namespace Pidp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : PidpControllerBase
    {
        private readonly PidpDbContext _context;
        public FirstController(PidpDbContext context)
        {
            _context = context;
        }

        // GET: api/First
        /// <summary>
        /// Gets a non-usable result. Used to test connection to database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSomething()
        {
            return Ok(_context.Set<Province>().Count());
        }
    }
}
