using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Services;
using Prime.Models.Api;
using Prime.ViewModels.Parties;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = Schemes.MohJwt)]
    [Route("api/parties/[controller]")]
    [ApiController]
    public class GisController : ControllerBase
    {
        private readonly IGisService _gisService;
        public GisController(IGisService gisService)
        {
            _gisService = gisService;
        }

        // POST: api/parties/gis
        /// <summary>
        /// Creates a new Gis Enrolment
        /// </summary>
        [HttpPost(Name = nameof(CreateGisParty))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateGisParty(GisChangeModel changeModel)
        {
            if (changeModel == null)
            {
                ModelState.AddModelError("Party", "Could not create the Party, the passed in model cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (!changeModel.Validate(User))
            {
                ModelState.AddModelError("GisEnrolment", "One or more Properties did not match the information on the card.");
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

        // PUT: api/parties/gis/5
        /// <summary>
        /// Updates a specific Gis Enrolment.
        /// </summary>
        /// <param name="gisId"></param>
        /// <param name="changeModel"></param>
        [HttpPut("{gisId}", Name = nameof(UpdateGisEnrollee))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateGisEnrollee(int gisId, GisChangeModel changeModel)
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

            if (!changeModel.Validate(User))
            {
                ModelState.AddModelError("GisEnrolment", "One or more Properties did not match the information on the card.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            await _gisService.CreateOrUpdateGisEnrolmentAsync(changeModel, User);

            return NoContent();
        }

        // GET: api/parties/gis/5
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
        public async Task<ActionResult> GetGisEnrolmentById(int gisId)
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

        // GET: api/parties/gis/5fdd17a6-1797-47a4-97b7-5b27949dd614
        /// <summary>
        /// Gets a specific Gis Enrolment by userId
        /// </summary>
        /// /// <param name="userId"></param>
        [HttpGet("{userId:guid}", Name = nameof(GetGisEnrolmentByUserId))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetGisEnrolmentByUserId(Guid userId)
        {
            if (userId != User.GetPrimeUserId())
            {
                return Forbid();
            }

            var gisEnrolment = await _gisService.GetGisEnrolmentByUserIdAsync(userId);
            if (gisEnrolment == null)
            {
                return NotFound(ApiResponse.Message($"Gis Enrolment not found for logged in user"));
            }

            return Ok(ApiResponse.Result(gisEnrolment));
        }

        // POST: api/parties/gis/5/ldap/login
        /// <summary>
        /// Login to ldap using username and password
        /// </summary>
        /// <param name="gisId"></param>
        /// <param name="payload"></param>
        [HttpPost("{gisId}/ldap/login", Name = nameof(LdapLogin))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> LdapLogin(int gisId, LdapLoginPayload payload)
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

            var result = await _gisService.LdapLogin(payload.LdapUsername, payload.LdapPassword, User);

            if (result)
            {
                return Ok();
            }

            return Unauthorized();
        }

        // POST: api/parties/gis/5/submission
        /// <summary>
        /// Submits the given Gis enrolment.
        /// </summary>
        [HttpPost("{gisId}/submission", Name = nameof(SubmitGis))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SubmitGis(int gisId)
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

            await _gisService.SubmitApplicationAsync(gisId);

            gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            return Ok(ApiResponse.Result(gisEnrolment));
        }
    }
}
