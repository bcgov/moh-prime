using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models.Api;
using Prime.Services;
using Prime.HttpClients;
using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : PrimeControllerBase
    {
        private readonly ILookupService _lookupService;
        private readonly ICollegeLicenceClient _collegeLicenceClient;

        public LookupsController(ILookupService lookupService, ICollegeLicenceClient collegeLicenceClient)
        {
            _lookupService = lookupService;
            _collegeLicenceClient = collegeLicenceClient;
        }

        //GET: /api/Lookups
        /// <summary>
        /// Gets all the lookup code values.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResultResponse<LookupEntity>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetLookups()
        {
            var lookupEntity = await _lookupService.GetLookupsAsync();

            return Ok(lookupEntity);
        }

        // POST /api/lookups/validate-licence
        /// <summary>
        /// For testing college licence validation
        /// </summary>
        [HttpPost("validate-licence", Name = nameof(LicenceCodeTest))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiResultResponse<PharmanetCollegeRecord>), StatusCodes.Status200OK)]
        public async Task<ActionResult> LicenceCodeTest(string collegePrefix, string licenceNumber)
        {
            var record = await _collegeLicenceClient.GetCollegeRecordAsync(collegePrefix, licenceNumber);

            return Ok(record);
        }

        // GET /api/lookups/configuration
        /// <summary>
        /// Get the app configuration
        /// </summary>
        [HttpGet("configuration", Name = nameof(GetConfiguration))]
        // [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetConfiguration()
        {
            return Ok(PrimeConfiguration.Current);
        }
    }
}
