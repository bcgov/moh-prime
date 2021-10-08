using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Services;
using Prime.ViewModels.SpecialAuthorityTransformation;


namespace Prime.Controllers
{
    /// <summary>
    /// "Special Authority Transformation" Controller
    /// </summary>
    [Produces("application/json")]
    [Authorize(Roles = Roles.PrimeEnrollee)]
    [Route("api/parties/sat")]
    [ApiController]
    public class SatEnrolmentController : PrimeControllerBase
    {
        private readonly ISatEnrolmentService _satEnrolmentService;
        private readonly IPlrProviderService _plrProviderService;

        public SatEnrolmentController(
            ISatEnrolmentService satEnrolmentService,
            IPlrProviderService plrProviderService
            )
        {
            _satEnrolmentService = satEnrolmentService;
            _plrProviderService = plrProviderService;
        }

        // POST: api/parties/sat
        /// <summary>
        /// Creates a new SAT Enrolment
        /// </summary>
        [HttpPost(Name = nameof(CreateSatEnrollee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Party>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateSatEnrollee(SatEnrolleeDemographicChangeModel payload)
        {
            if (!payload.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the BCSC.");
            }

            int enrolleeId = await _satEnrolmentService.CreateOrUpdateEnrolleeAsync(payload, User);
            Party satParty = await _satEnrolmentService.GetEnrolleeAsync(enrolleeId);
            return CreatedAtAction(
                nameof(GetSatEnrolleeById),
                new { satId = satParty.Id },
                satParty
            );
        }

        // GET: api/parties/sat/5
        /// <summary>
        /// Gets a specific SAT Enrolment
        /// </summary>
        /// <param name="satId"></param>
        [HttpGet("{satId}", Name = nameof(GetSatEnrolleeById))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SatEnrolleeDemographicChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSatEnrolleeById(int satId)
        {
            var satEnrollee = await _satEnrolmentService.GetEnrolleeAsync(satId);
            if (satEnrollee == null)
            {
                return NotFound($"SAT Enrollee not found with id {satId}");
            }
            if (!satEnrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(satEnrollee);
        }

        // PUT: api/parties/sat/5/demographics
        /// <summary>
        /// Updates a SAT Enrollee's demographic information.
        /// </summary>
        [HttpPut("{satId}/demographics", Name = nameof(UpdateSatEnrolleeDemographics))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateSatEnrolleeDemographics(int satId, SatEnrolleeDemographicChangeModel payload)
        {
            var satEnrollee = await _satEnrolmentService.GetEnrolleeAsync(satId);
            if (satEnrollee == null)
            {
                return NotFound($"SAT Enrollee not found with id {satId}");
            }
            if (!satEnrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (!payload.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the BCSC.");
            }

            await _satEnrolmentService.UpdateDemographicsAsync(satId, payload, User);
            return Ok();
        }

        // PUT: api/parties/sat/5/certifications
        /// <summary>
        /// Updates a SAT Enrollee's Certifications.
        /// </summary>
        [HttpPut("{satId}/certifications", Name = nameof(UpdateSatEnrolleeCertifications))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateSatEnrolleeCertifications(int satId, ICollection<SatEnrolleeCertificationViewModel> payload)
        {
            var satEnrollee = await _satEnrolmentService.GetEnrolleeAsync(satId);
            if (satEnrollee == null)
            {
                return NotFound($"SAT Enrollee not found with id {satId}");
            }
            if (!satEnrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _satEnrolmentService.UpdateCertificationsAsync(satId, payload);
            return Ok();
        }

        // POST: api/parties/sat/5/finalize
        /// <summary>
        /// Finalize a SAT Enrolment
        /// </summary>
        /// <param name="satId"></param>
        [HttpPost("{satId}/finalize", Name = nameof(CreateSatEnrollee))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> FinalizeSatEnrollee(int satId)
        {
            var satEnrollee = await _satEnrolmentService.GetEnrolleeAsync(satId);
            if (satEnrollee == null)
            {
                return NotFound($"SAT Enrollee not found with id {satId}");
            }
            if (!satEnrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            // TODO: Currently just checks if party exists in PLR with
            // matching College Id, First Name, and Last Name.
            // Need more talks as to what finalizing a Sat Enrollee entails.
            var found = await _plrProviderService.CheckPartyValidityAsync(satId);

            if (!found)
            {
                return NotFound($"No matching PLR data found.");
            }

            return NoContent();
        }
    }
}
