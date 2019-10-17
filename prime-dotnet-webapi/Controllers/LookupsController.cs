using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Models;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs at least the ADMIN or ENROLMENT role to use this controller
    // TODO - add this back once there are OAuth tokens
    // [Authorize(Policy = PrimeConstants.PRIME_USER_POLICY)]
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
            LookupEntity lookupEntity = new LookupEntity();

            lookupEntity.Colleges = await _lookupService.GetLookupsAsync<College>(c => c.CollegeLicenses, c => c.CollegePractices);
            lookupEntity.JobNames = await _lookupService.GetLookupsAsync<JobName>();
            lookupEntity.Licenses = await _lookupService.GetLookupsAsync<License>(l => l.CollegeLicenses);
            lookupEntity.OrganizationNames = await _lookupService.GetLookupsAsync<OrganizationName>();
            lookupEntity.OrganizationTypes = await _lookupService.GetLookupsAsync<OrganizationType>();
            lookupEntity.Practices = await _lookupService.GetLookupsAsync<Practice>(p => p.CollegePractices);
            lookupEntity.Statuses = await _lookupService.GetLookupsAsync<Status>();

            return Ok(new ApiOkResponse<LookupEntity>(lookupEntity));
        }
    }
}
