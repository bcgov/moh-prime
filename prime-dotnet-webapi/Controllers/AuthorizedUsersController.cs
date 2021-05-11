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
        private readonly IAuthorizedUserService _authorizedUserService;
        private readonly IOrganizationService _organizationService;

        public AuthorizedUsersController(
            IAuthorizedUserService authorizedUserService,
            IOrganizationService organizationService)
        {
            _authorizedUserService = authorizedUserService;
            _organizationService = organizationService;
        }

        // GET: api/parties/authorized-users/5fdd17a6-1797-47a4-97b7-5b27949dd614
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
        public async Task<ActionResult> GetAuthorizedUserByUserId(Guid userId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserForUserIdAsync(userId);
            if (authorizedUser == null)
            {
                return NotFound(ApiResponse.Message($"Authorized user not found with id {userId}"));
            }

            return Ok(ApiResponse.Result(authorizedUser));
        }

        // GET: api/parties/authorized-users/5
        /// <summary>
        /// Gets a specific AuthorizedUser.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpGet("{authorizedUserId:int}", Name = nameof(GetAuthorizedUserById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserChangeModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUserById(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound(ApiResponse.Message($"Authorized user not found with id {authorizedUserId}"));
            }

            return Ok(ApiResponse.Result(authorizedUser));
        }

        // POST: api/parties/authorized-users
        /// <summary>
        /// Creates a new AuthorizedUser.
        /// </summary>
        [HttpPost(Name = nameof(CreateAuthorizedUser))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<AuthorizedUserChangeModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateAuthorizedUser(AuthorizedUserChangeModel authorizedUser)
        {
            if (authorizedUser == null)
            {
                ModelState.AddModelError("AuthorizedUser", "AuthorizedUser can not be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createdAuthorizedUserId = await _authorizedUserService.CreateOrUpdateAuthorizedUserAsync(authorizedUser, User);
            var createdAuthorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(createdAuthorizedUserId);

            return CreatedAtAction(
                nameof(GetAuthorizedUserById),
                new { authorizedUserId = createdAuthorizedUserId },
                ApiResponse.Result(createdAuthorizedUser)
            );
        }

        // PUT: api/parties/authorized-users/5
        /// <summary>
        /// Updates a specific AuthorizedUser.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        /// <param name="updatedAuthorizedUser"></param>
        [HttpPut("{authorizedUserId}", Name = nameof(UpdateAuthorizedUser))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAuthorizedUser(int authorizedUserId, AuthorizedUserChangeModel updatedAuthorizedUser)
        {
            if (!await _authorizedUserService.AuthorizedUserExistsAsync(authorizedUserId))
            {
                return NotFound(ApiResponse.Message($"AuthorizedUser not found with id {authorizedUserId}"));
            }

            await _authorizedUserService.CreateOrUpdateAuthorizedUserAsync(updatedAuthorizedUser, User);

            return NoContent();
        }

        // POST: api/parties/authorized-user/5/activate
        /// <summary>
        /// Activates the authorized user.
        /// </summary>
        /// <param name="authorizedUserId"></param>
        [HttpPost("{authorizedUserId}/activate", Name = nameof(ActivateAuthorizedUser))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ActivateAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound(ApiResponse.Message($"AuthorizedUser not found with id {authorizedUserId}"));
            }

            if (authorizedUser.Status != AccessStatusType.Approved)
            {
                ModelState.AddModelError("AuthorizedUser", $"Status cannot be changed from {Enum.GetName(typeof(AccessStatusType), authorizedUser.Status)} to {Enum.GetName(typeof(AccessStatusType), 3)}");
                return BadRequest(ApiResponse.BadRequest(ModelState));
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
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ApproveAuthorizedUser(int authorizedUserId)
        {
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserAsync(authorizedUserId);
            if (authorizedUser == null)
            {
                return NotFound(ApiResponse.Message($"AuthorizedUser not found with id {authorizedUserId}"));
            }

            await _authorizedUserService.ApproveAuthorizedUser(authorizedUserId);

            return NoContent();
        }
    }
}
