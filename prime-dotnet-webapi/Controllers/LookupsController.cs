using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    // User needs at least the READONLY ADMIN or ENROLLEE role to use this controller
    [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        private readonly IPharmanetApiService _pharmanetApiService;

        public LookupsController(ILookupService lookupService, IPharmanetApiService pharmanetApiService)
        {
            _lookupService = lookupService;
            _pharmanetApiService = pharmanetApiService;
        }

        //GET: /api/Lookup
        /// <summary>
        /// Gets all the lookup code values.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResultResponse<LookupEntity>), StatusCodes.Status200OK)]
        public async Task<ActionResult<LookupEntity>> GetLookups()
        {
            var lookupEntity = await _lookupService.GetLookupsAsync();

            return Ok(ApiResponse.Result(lookupEntity));
        }

        // POST /api/lookups/validate-licence
        /// <summary>
        /// For testing college licence validation
        /// </summary>
        [HttpPost("validate-licence", Name = nameof(LicenceCodeTest))]
        [Authorize(Policy = AuthConstants.SUPER_ADMIN_POLICY)]
        public async Task<ActionResult<PharmanetCollegeRecord>> LicenceCodeTest(string collegePrefix, string licenceNumber)
        {
            Certification cert = new Certification
            {
                LicenseNumber = licenceNumber,
                College = new College
                {
                    Prefix = collegePrefix
                }
            };

            var record = await _pharmanetApiService.GetCollegeRecordAsync(cert);

            return Ok(ApiResponse.Result(record));
        }
    }
}
