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
    public class LookupsController : ControllerBase
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

            return Ok(ApiResponse.Result(lookupEntity));
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

            return Ok(ApiResponse.Result(record));
        }
    }
}
