using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Extensions;
using Prime.Models;
using Prime.Services;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Parties;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/parties/authorized-users")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class AuthorizedUsersController : PrimeControllerBase
    {
        private readonly IAuthorizedUserService _authorizedUserService;

        public AuthorizedUsersController(
            IAuthorizedUserService authorizedUserService)
        {
            _authorizedUserService = authorizedUserService;
        }

        // GET: api/parties/authorized-users/5fdd17a6-1797-47a4-97b7-5b27949dd614
        /// <summary>
        /// Gets a AuthorizedUser by user ID.
        /// </summary>
        /// <param name="username"></param>
        [HttpGet("{username}", Name = nameof(GetAuthorizedUserByUsername))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUserByUsername(string username)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserForUsernameAsync(username);
            if (authorizedUser == null)
            {
                return NotFound($"Authorized user not found with username {username}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(authorizedUser);
        }

        // GET: api/parties/authorized-users/5
        /// <summary>
        /// Gets a specific AuthorizedUser.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpGet("{authorizedUserId:int}", Name = nameof(GetAuthorizedUserById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUserById(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"Authorized user not found with id {authorizedUserId}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(authorizedUser);
        }

        // GET: api/parties/authorized-users/5/sites
        /// <summary>
        /// Gets health authority sites of an authorized user.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpGet("{authorizedUserId:int}/sites", Name = nameof(GetAuthorizedUserSites))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteListViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUserSites(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"Authorized user not found with id {authorizedUserId}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var healthAuthoritySites = await _authorizedUserService.GetAuthorizedUserSitesAsync(authorizedUser.Id);

            return Ok(healthAuthoritySites);
        }


        // GET: api/parties/authorized-users/5/site-count
        /// <summary>
        /// Gets the number of sites belong to an authorized user.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpGet("{authorizedUserId:int}/site-count", Name = nameof(GetAuthorizedUserSiteCount))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUserSiteCount(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"Authorized user not found with id {authorizedUserId}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var siteCount = await _authorizedUserService.GetAuthorizedUserSiteCountAsync(authorizedUser.Id);

            return Ok(siteCount);
        }

        // POST: api/parties/authorized-users
        /// <summary>
        /// Creates a new AuthorizedUser.
        /// </summary>
        [HttpPost(Name = nameof(CreateAuthorizedUser))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAuthorizedUser(AuthorizedUserChangeModel authorizedUser)
        {
            if (authorizedUser == null)
            {
                return BadRequest("Could not create AuthorizedUser, the passed in model cannot be null.");
            }
            if (!authorizedUser.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            var createdAuthorizedUserId = await _authorizedUserService.CreateOrUpdateAuthorizedUserAsync(authorizedUser, User);
            if (createdAuthorizedUserId.IsInvalidId())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Could not create the authorized user." });
            }

            var createdAuthorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(createdAuthorizedUserId);

            return CreatedAtAction(
                nameof(GetAuthorizedUserById),
                new { authorizedUserId = createdAuthorizedUserId },
                createdAuthorizedUser
            );
        }

        // PUT: api/parties/authorized-users/5
        /// <summary>
        /// Updates a specific AuthorizedUser.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        /// <param name="updatedAuthorizedUser"></param>
        [HttpPut("{authorizedUserId}", Name = nameof(UpdateAuthorizedUser))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateAuthorizedUser(int authorizedUserId, AuthorizedUserChangeModel updatedAuthorizedUser)
        {
            if (updatedAuthorizedUser == null)
            {
                return BadRequest("Authorized user update model cannot be null.");
            }
            if (!updatedAuthorizedUser.Validate(User))
            {
                return BadRequest("One or more Properties did not match the information on the card.");
            }

            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"Authorized user not found with id {authorizedUserId}");
            }

            var updatedAuthorizedUserId = await _authorizedUserService.CreateOrUpdateAuthorizedUserAsync(updatedAuthorizedUser, User);
            if (updatedAuthorizedUserId.IsInvalidId())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Could not update the authorized user." });
            }

            return NoContent();
        }

        // POST: api/parties/authorized-user/5/activate
        /// <summary>
        /// Activates the authorized user.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpPost("{authorizedUserId}/activate", Name = nameof(ActivateAuthorizedUser))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ActivateAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"AuthorizedUser not found with id {authorizedUserId}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (authorizedUser.Status == AccessStatusType.Active)
            {
                return NoContent();
            }
            if (authorizedUser.Status != AccessStatusType.Approved)
            {
                return BadRequest($"Status cannot be changed from {Enum.GetName(typeof(AccessStatusType), authorizedUser.Status)} to {nameof(AccessStatusType.Active)}");
            }

            await _authorizedUserService.ActivateAuthorizedUser(authorizedUserId);

            return NoContent();
        }

        // POST: api/parties/authorized-user/5/approve
        /// <summary>
        /// Approves the authorized user.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpPost("{authorizedUserId}/approve", Name = nameof(ApproveAuthorizedUser))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ApproveAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"AuthorizedUser not found with id {authorizedUserId}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _authorizedUserService.ApproveAuthorizedUser(authorizedUserId);

            return NoContent();
        }


        // DELETE: api/parties/authorized-user/5
        /// <summary>
        /// Delete the authorized user.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpDelete("{authorizedUserId}", Name = nameof(DeleteAuthorizedUser))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"AuthorizedUser not found with id {authorizedUserId}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (authorizedUser.Status != AccessStatusType.UnderReview)
            {
                return Forbid("Authorized User is not in Under Review status. User can't be deleted.");
            }

            await _authorizedUserService.DeleteAuthorizedUserAsync(authorizedUserId);

            return NoContent();
        }

        // PUT: api/health-authorities/authorized-users/5/disable
        /// <summary>
        /// Disable authorized user
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpPut("{authorizedUserId}/disable", Name = nameof(DisableAuthorizedUser))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthorityOrganizationAgreementDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DisableAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound($"AuthorizedUser not found with id {authorizedUserId}");
            }
            if (!authorizedUser.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _authorizedUserService.UpdateAuthorizedUserStatus(authorizedUserId, AccessStatusType.Disabled);

            return NoContent();
        }
    }
}
