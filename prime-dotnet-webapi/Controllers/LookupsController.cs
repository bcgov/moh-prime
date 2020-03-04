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
    // User needs at least the ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = PrimeConstants.USER_POLICY)]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        //GET: /api/Lookup
        /// <summary>
        /// Gets all the lookup code values.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiOkResponse<LookupEntity>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupEntity>> GetLookups()
        {
            var lookupEntity = await _lookupService.GetLookupsAsync();

            return Ok(new ApiOkResponse<LookupEntity>(lookupEntity));
        }
    }
}
