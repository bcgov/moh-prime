using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Services;
using Prime.Models.Api;
using Prime.ViewModels.Parties;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GisController : ControllerBase
    {
        private readonly IGisService _gisService;
        public GisController(IGisService gisService)
        {
            _gisService = gisService;
        }

        // POST: api/gis
        /// <summary>
        /// Creates a new Gis Enrolment
        /// </summary>
        [HttpPost(Name = nameof(CreateGisParty))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateGisParty(GisChangeModel changeModel)
        {
            if (changeModel == null)
            {
                ModelState.AddModelError("Party", "Could not create the Party, the passed in model cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createdGisId = await _gisService.CreateOrUpdateGisEnrolmentAsync(changeModel, User);

            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(createdGisId);

            return CreatedAtAction(
                nameof(GetGisEnrolmentById),
                new { gisId = createdGisId },
                ApiResponse.Result(gisEnrolment)
            );
        }

        // PUT: api/Gis/5
        /// <summary>
        /// Updates a specific Gis Enrolment.
        /// </summary>
        /// <param name="gisId"></param>
        /// <param name="changeModel"></param>
        [HttpPut("{gisId}", Name = nameof(UpdateGisEnrollee))]
        // [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateGisEnrollee(int gisId, GisChangeModel changeModel)
        {
            if (changeModel == null)
            {
                ModelState.AddModelError("GisEnrolment", "Profile update model cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            if (gisEnrolment == null)
            {
                return NotFound(ApiResponse.Message($"Gis Enrolment not found with id {gisId}"));
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _gisService.CreateOrUpdateGisEnrolmentAsync(changeModel, User);

            return NoContent();
        }

        // GET: api/Gis/5
        /// <summary>
        /// Gets a specific Gis Enrolment.
        /// </summary>
        /// <param name="gisId"></param>
        [HttpGet("{gisId}", Name = nameof(GetGisEnrolmentById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<GisViewModel>> GetGisEnrolmentById(int gisId)
        {
            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            if (gisEnrolment == null)
            {
                return NotFound(ApiResponse.Message($"Gis Enrolment not found with id {gisId}"));
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(ApiResponse.Result(gisEnrolment));
        }

        // GET: api/Gis/5
        /// <summary>
        /// Gets a specific Gis Enrolment using the logged in User.
        /// </summary>
        [HttpGet("", Name = nameof(GetGisEnrolmentByUserId))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<GisViewModel>> GetGisEnrolmentByUserId()
        {
            var gisEnrolment = await _gisService.GetGisEnrolmentByUserIdAsync(User.GetPrimeUserId());
            if (gisEnrolment == null)
            {
                return NotFound(ApiResponse.Message($"Gis Enrolment not found for logged in user"));
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(ApiResponse.Result(gisEnrolment));
        }

        // POST: api/gis/ldap/login
        /// <summary>
        /// Login to ldap using username and password
        /// </summary>
        /// <param name="gisId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        [HttpPost("ldap/login", Name = nameof(LdapLogin))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> LdapLogin(int gisId, string username, string password)
        {
            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            if (gisEnrolment == null)
            {
                return NotFound(ApiResponse.Message($"Gis Enrolment not found with id {gisId}"));
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var result = await _gisService.LdapLogin(username, password, User);

            if (result == true)
            {
                return Ok();
            }

            return Unauthorized();
        }
    }
}
