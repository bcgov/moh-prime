using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using Prime.ViewModels.Parties;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/parties/authorized-users")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class AuthorizedUsersController : ControllerBase
    {
        private readonly IPartyService _partyService;
        private readonly IOrganizationService _organizationService;

        public AuthorizedUsersController(
            IPartyService partyService,
            IOrganizationService organizationService)
        {
            _partyService = partyService;
            _organizationService = organizationService;
        }

        // GET: api/AuthorizedUser/5fdd17a6-1797-47a4-97b7-5b27949dd614
        /// <summary>
        /// Gets a AuthorizedUser by user ID.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId:guid}", Name = nameof(GetAuthorizedUserByUserId))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthorizedUserChangeModel>> GetAuthorizedUserByUserId(Guid userId)
        {
            var authorizedUser = await _partyService.GetPartyForUserIdAsync(userId, PartyType.AuthorizedUser);
            if (authorizedUser == null)
            {
                return NotFound(ApiResponse.Message($"Authorized user not found with id {userId}"));
            }

            return Ok(ApiResponse.Result(authorizedUser));
        }

        // GET: api/AuthorizedUser/5
        /// <summary>
        /// Gets a specific AuthorizedUser.
        /// </summary>
        /// <param name="partyId"></param>
        [HttpGet("{partyId:int}", Name = nameof(GetAuthorizedUserById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthorizedUserChangeModel>> GetAuthorizedUserById(int partyId)
        {
            var authorizedUser = await _partyService.GetPartyAsync(partyId, PartyType.AuthorizedUser);
            if (authorizedUser == null)
            {
                return NotFound(ApiResponse.Message($"Authorized user not found with id {partyId}"));
            }

            return Ok(ApiResponse.Result(authorizedUser));
        }

        // POST: api/AuthorizedUser
        /// <summary>
        /// Creates a new AuthorizedUser.
        /// </summary>
        [HttpPost(Name = nameof(CreateAuthorizedUser))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserChangeModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult<AuthorizedUserChangeModel>> CreateAuthorizedUser(AuthorizedUserChangeModel authorizedUser)
        {
            if (authorizedUser == null)
            {
                ModelState.AddModelError("AuthorizedUser", "AuthorizedUser can not be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createdAuthorizedUserId = await _partyService.CreateOrUpdatePartyAsync(authorizedUser, User);
            var createdAuthorizedUser = await _partyService.GetPartyAsync(createdAuthorizedUserId);

            return CreatedAtAction(
                nameof(GetAuthorizedUserById),
                new { partyId = createdAuthorizedUserId },
                ApiResponse.Result(createdAuthorizedUser)
            );
        }

        // PUT: api/AuthorizedUser/5
        /// <summary>
        /// Updates a specific AuthorizedUser.
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="updatedAuthorizedUser"></param>
        [HttpPut("{partyId}", Name = nameof(UpdateAuthorizedUser))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAuthorizedUser(int partyId, AuthorizedUserChangeModel updatedAuthorizedUser)
        {
            if (!await _partyService.PartyExistsAsync(partyId, PartyType.AuthorizedUser))
            {
                return NotFound(ApiResponse.Message($"AuthorizedUser not found with id {partyId}"));
            }

            await _partyService.CreateOrUpdatePartyAsync(updatedAuthorizedUser, User);

            return NoContent();
        }

        // GET: api/AuthorizedUser/5fdd17a6-1797-47a4-97b7-5b27949dd614/organizations
        /// <summary>
        /// Gets all of the Organizations for a authorized user by userId.
        /// </summary>
        /// <param name="userId"></param>
        [HttpGet("{userId}/organizations", Name = nameof(GetAuthorizedUserOrganizationsByUserId))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<OrganizationListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrganizationListViewModel>>> GetAuthorizedUserOrganizationsByUserId(Guid userId)
        {
            if (userId != User.GetPrimeUserId())
            {
                return Forbid();
            }

            if (!await _partyService.PartyExistsForUserIdAsync(userId, PartyType.AuthorizedUser))
            {
                return NotFound(ApiResponse.Message($"AuthorizedUser not found with user id {userId}"));
            }

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());
            var organizations = (party != null)
                ? await _organizationService.GetOrganizationsByPartyIdAsync(party.Id)
                : Enumerable.Empty<OrganizationListViewModel>();

            return Ok(ApiResponse.Result(organizations));
        }
    }
}
