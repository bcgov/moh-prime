using Microsoft.AspNetCore.Mvc;

using Pidp.Services;

namespace Pidp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : PidpControllerBase
    {
        public class ApiMessageResponse
        {
            public string Message { get; }

            public ApiMessageResponse(string message)
            {
                Message = message;
            }
        }
        private readonly IFirstService _firstService;

        public FirstController(IFirstService firstService)
        {
            _firstService = firstService;
        }

        // GET: api/First
        /// <summary>
        /// Gets a non-usable result. Used to test connection to database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSomething()
        {
            var result = await _firstService.GetSomethingAsync();

            return Ok(result);
        }


        // GET: api/First/lookups
        /// <summary>
        /// Gets entries in the FirstLookup Table
        /// </summary>
        [HttpGet("lookups")]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLookups()
        {
            var result = await _firstService.GetLookupModelNamesAsync();

            return Ok(result);
        }
    }

}
