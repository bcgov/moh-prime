using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Services;
using Prime.ViewModels;
using Prime.ViewModels.Parties;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/parties/signing-authorities")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class SigningAuthorityController : PrimeControllerBase
    {
        private readonly IPartyService _partyService;
        private readonly IOrganizationService _organizationService;

        public SigningAuthorityController(
            IPartyService partyService,
            IOrganizationService organizationService)
        {
            _partyService = partyService;
            _organizationService = organizationService;
        }

        // GET: api/SigningAuthority/5fdd17a6-1797-47a4-97b7-5b27949dd614
        /// <summary>
        /// Gets a SigningAuthority by user ID.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId:guid}", Name = nameof(GetSigningAuthorityByUserId))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SigningAuthorityChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSigningAuthorityByUserId(Guid userId)
        {
            var signingAuthority = await _partyService.GetPartyForUserIdAsync(userId, PartyType.SigningAuthority);
            if (signingAuthority == null)
            {
                return NotFound($"Signing authority not found with id {userId}");
            }

            return Ok(signingAuthority);
        }

        // GET: api/SigningAuthority/5
        /// <summary>
        /// Gets a specific SigningAuthority.
        /// </summary>
        /// <param name="partyId"></param>
        [HttpGet("{partyId:int}", Name = nameof(GetSigningAuthorityById))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SigningAuthorityChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSigningAuthorityById(int partyId)
        {
            var signingAuthority = await _partyService.GetPartyAsync(partyId, PartyType.SigningAuthority);
            if (signingAuthority == null)
            {
                return NotFound($"Signing authority not found with id {partyId}");
            }

            return Ok(signingAuthority);
        }

        // POST: api/SigningAuthority
        /// <summary>
        /// Creates a new SigningAuthority.
        /// </summary>
        [HttpPost(Name = nameof(CreateSigningAuthority))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<SigningAuthorityChangeModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateSigningAuthority(SigningAuthorityChangeModel signingAuthority)
        {
            if (signingAuthority == null)
            {
                return BadRequest("SigningAuthority can not be null.");
            }

            var createdSigningAuthorityId = await _partyService.CreateOrUpdatePartyAsync(signingAuthority, User);
            var createdSigningAuthority = await _partyService.GetPartyAsync(createdSigningAuthorityId);

            return CreatedAtAction(
                nameof(GetSigningAuthorityById),
                new { partyId = createdSigningAuthorityId },
                createdSigningAuthority
            );
        }

        // PUT: api/SigningAuthority/5
        /// <summary>
        /// Updates a specific SigningAuthority.
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="updatedSigningAuthority"></param>
        [HttpPut("{partyId}", Name = nameof(UpdateSigningAuthority))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateSigningAuthority(int partyId, SigningAuthorityChangeModel updatedSigningAuthority)
        {
            if (!await _partyService.PartyExistsAsync(partyId, PartyType.SigningAuthority))
            {
                return NotFound($"SigningAuthority not found with id {partyId}");
            }

            await _partyService.CreateOrUpdatePartyAsync(updatedSigningAuthority, User);

            return NoContent();
        }

        // GET: api/SigningAuthority/5fdd17a6-1797-47a4-97b7-5b27949dd614/organizations
        /// <summary>
        /// Gets all of the Organizations for a signing authority by userId.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId}/organizations", Name = nameof(GetSigningAuthorityOrganizationsByUserId))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<OrganizationListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSigningAuthorityOrganizationsByUserId(Guid userId)
        {
            if (userId != User.GetPrimeUserId())
            {
                return Forbid();
            }

            if (!await _partyService.PartyExistsForUserIdAsync(userId, PartyType.SigningAuthority))
            {
                return NotFound($"SigningAuthority not found with user id {userId}");
            }

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());
            var organizations = (party != null)
                ? await _organizationService.GetOrganizationsByPartyIdAsync(party.Id)
                : Enumerable.Empty<OrganizationListViewModel>();

            return Ok(organizations);
        }
    }
}
