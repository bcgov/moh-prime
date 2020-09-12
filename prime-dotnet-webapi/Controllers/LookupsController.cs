using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.HttpClients;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.AnyUser)]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        private readonly ICollegeLicenceClient _collegeLicenceClient;

        public LookupsController(ILookupService lookupService, ICollegeLicenceClient collegeLicenceClient)
        {
            _lookupService = lookupService;
            _collegeLicenceClient = collegeLicenceClient;
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
        [Authorize(Roles = AuthConstants.PRIME_SUPER_ADMIN_ROLE)]
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

            var record = await _collegeLicenceClient.GetCollegeRecordAsync(cert);

            return Ok(ApiResponse.Result(record));
        }
    }
}
