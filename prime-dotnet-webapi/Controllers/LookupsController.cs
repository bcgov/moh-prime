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
        [ProducesResponseType(typeof(ApiResultResponse<LookupEntity>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupEntity>> GetLookups()
        {
            LookupEntity lookupEntity = new LookupEntity();

            lookupEntity.Colleges = await _lookupService.GetLookupsAsync<int, College>(c => c.CollegeLicenses, c => c.CollegePractices);
            lookupEntity.JobNames = await _lookupService.GetLookupsAsync<int, JobName>();
            lookupEntity.Licenses = await _lookupService.GetLookupsAsync<int, License>(l => l.CollegeLicenses);
            lookupEntity.OrganizationTypes = await _lookupService.GetLookupsAsync<int, OrganizationType>();
            lookupEntity.Practices = await _lookupService.GetLookupsAsync<int, Practice>(p => p.CollegePractices);
            lookupEntity.Statuses = await _lookupService.GetLookupsAsync<int, Status>();
            lookupEntity.Countries = await _lookupService.GetLookupsAsync<string, Country>();
            lookupEntity.Provinces = await _lookupService.GetLookupsAsync<string, Province>();
            lookupEntity.StatusReasons = await _lookupService.GetLookupsAsync<int, StatusReason>();
            lookupEntity.PrivilegeGroups = await _lookupService.GetLookupsAsync<int, PrivilegeGroup>();
            lookupEntity.PrivilegeTypes = await _lookupService.GetLookupsAsync<int, PrivilegeType>();

            return Ok(ApiResponse.Result(lookupEntity));
        }
    }
}
