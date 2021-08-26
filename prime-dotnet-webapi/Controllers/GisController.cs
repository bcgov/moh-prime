using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Services;
using Prime.HttpClients;
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
    public class GisController : PrimeControllerBase
    {
        private readonly IGisService _gisService;
        private readonly ILdapClient _ldapClient;
        public GisController(IGisService gisService)
        {
            _gisService = gisService;
        }

        // POST: api/parties/gis
        /// <summary>
        /// Creates a new Gis Enrolment
        /// </summary>
        [HttpPost(Name = nameof(CreateGisParty))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateGisParty(GisChangeModel changeModel)
        {
            if (changeModel == null)
            {
                return BadRequest("Could not create the Party, the passed in model cannot be null.");
            }

            if (!changeModel.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            var createdGisId = await _gisService.CreateOrUpdateGisEnrolmentAsync(changeModel, User);
            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(createdGisId);

            return CreatedAtAction(
                nameof(GetGisEnrolmentById),
                new { gisId = createdGisId },
                gisEnrolment
            );
        }

        // PUT: api/parties/gis/5
        /// <summary>
        /// Updates a specific Gis Enrolment.
        /// </summary>
        /// <param name="gisId"></param>
        /// <param name="changeModel"></param>
        [HttpPut("{gisId}", Name = nameof(UpdateGisEnrollee))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateGisEnrollee(int gisId, GisChangeModel changeModel)
        {
            if (changeModel == null)
            {
                return BadRequest("Profile update model cannot be null.");
            }

            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            if (gisEnrolment == null)
            {
                return NotFound($"Gis Enrolment not found with id {gisId}");
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            if (!changeModel.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetGisEnrolmentById(int gisId)
        {
            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            if (gisEnrolment == null)
            {
                return NotFound($"Gis Enrolment not found with id {gisId}");
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(gisEnrolment);
        }

        // GET: api/parties/gis/5fdd17a6-1797-47a4-97b7-5b27949dd614
        /// <summary>
        /// Gets a specific Gis Enrolment by userId
        /// </summary>
        /// /// <param name="userId"></param>
        [HttpGet("{userId:guid}", Name = nameof(GetGisEnrolmentByUserId))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
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
                return NotFound($"Gis Enrolment not found for logged in user");
            }

            return Ok(gisEnrolment);
        }

        // POST: api/parties/gis/5/ldap/login
        /// <summary>
        /// Login to ldap using username and password
        /// </summary>
        /// <param name="gisId"></param>
        /// <param name="payload"></param>
        [HttpPost("{gisId}/ldap/login", Name = nameof(LdapLogin))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> LdapLogin(int gisId, LdapLoginPayload payload)
        {
            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            if (gisEnrolment == null)
            {
                return NotFound($"Gis Enrolment not found with id {gisId}");
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var ldapResponse = await _gisService.LdapLogin(payload.LdapUsername, payload.LdapPassword, User);

            if (ldapResponse.Success)
            {
                return Ok();
            }

            Response.Headers.Add("RemainingAttempts", ldapResponse.RemainingAttempts.ToString());
            Response.Headers.Add("LockoutTimeInHours", ldapResponse.LockoutTimeInHours.ToString());

            return Unauthorized();
        }

        // POST: api/parties/gis/5/submission
        /// <summary>
        /// Submits the given Gis enrolment.
        /// </summary>
        [HttpPost("{gisId}/submission", Name = nameof(SubmitGis))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<GisViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SubmitGis(int gisId)
        {
            var gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            if (gisEnrolment == null)
            {
                return NotFound($"Gis Enrolment not found with id {gisId}");
            }
            if (!gisEnrolment.Party.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _gisService.SubmitApplicationAsync(gisId);

            gisEnrolment = await _gisService.GetGisEnrolmentByIdAsync(gisId);
            return Ok(gisEnrolment);
        }
    }
}
