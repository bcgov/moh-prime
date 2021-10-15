using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Services;
using Prime.ViewModels.Parties;
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
        private readonly IMapper _mapper;
        private readonly IPartyService _partyService;

        public SatEnrolmentController(
            IMapper mapper,
            IPartyService partyService)
        {
            _mapper = mapper;
            _partyService = partyService;
        }

        // POST: api/parties/sat
        /// <summary>
        /// Creates a new SAT Enrollee
        /// </summary>
        [HttpPost(Name = nameof(CreateSatEnrollee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<SatViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateSatEnrollee(SatEnrolleeDemographicChangeModel payload)
        {
            if (!payload.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the BCSC.");
            }

            var enrolleeId = await _partyService.CreateOrUpdatePartyAsync(payload, User);
            var satParty = await _partyService.GetPartyAsync(enrolleeId, PartyType.SatEnrollee);

            return CreatedAtAction(
                nameof(GetSatEnrolleeById),
                new { satId = satParty.Id },
                satParty
            );
        }

        // GET: api/parties/sat/5fdd17a6-1797-47a4-97b7-5b27949dd614
        /// <summary>
        /// Gets a specific SAT Enrollee by userId
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId:guid}", Name = nameof(GetSatEnrolleeByUserId))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SatViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSatEnrolleeByUserId(Guid userId)
        {
            var satEnrollee = await _partyService.GetPartyForUserIdAsync(userId, PartyType.SatEnrollee);
            if (satEnrollee == null)
            {
                return NotFound($"SAT Enrollee not found with id {userId}");
            }
            if (!satEnrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(satEnrollee);
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
        [ProducesResponseType(typeof(ApiResultResponse<SatViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSatEnrolleeById(int satId)
        {
            var satEnrollee = await _partyService.GetPartyAsync(satId, PartyType.SatEnrollee);
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

        // PUT: api/parties/sat/5
        /// <summary>
        /// Updates a SAT Enrollee's information.
        /// </summary>
        [HttpPut("{satId}", Name = nameof(UpdateSatEnrollee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateSatEnrollee(int satId, SatEnrolleeDemographicChangeModel payload)
        {
            var satEnrollee = await _partyService.GetPartyAsync(satId, PartyType.SatEnrollee);
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

            await _partyService.CreateOrUpdatePartyAsync(payload, User);
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
        public async Task<ActionResult> UpdateSatEnrolleeCertifications(int satId, ICollection<PartyCertificationViewModel> payload)
        {
            var satEnrollee = await _partyService.GetPartyAsync(satId, PartyType.SatEnrollee);
            if (satEnrollee == null)
            {
                return NotFound($"SAT Enrollee not found with id {satId}");
            }
            if (!satEnrollee.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _partyService.UpdateCertificationsAsync(satId, payload);
            return Ok();
        }
    }
}
