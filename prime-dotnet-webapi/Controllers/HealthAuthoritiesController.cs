using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities")]
    [ApiController]
    public class HealthAuthoritiesController : ControllerBase
    {
        private readonly IHAAuthorizedUserService _haAuthorizedUserService;

        public HealthAuthoritiesController(IHAAuthorizedUserService haAuthorizedUserService)
        {
            _haAuthorizedUserService = haAuthorizedUserService;
        }

        // POST: api/health-authorities/5/authorized-users
        /// <summary>
        /// Create an authorized user for a health authority
        /// </summary>
        /// <param name="healthAuthorityCode"></param>
        /// <param name="model"></param>
        [HttpPost("{healthAuthorityCode}/authorized-users", Name = nameof(CreateAuthorizedUser))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<HAAuthorizedUser>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateAuthorizedUser(HealthAuthorityCode healthAuthorityCode, HAAuthorizedUser model)
        {
            var user = await _haAuthorizedUserService.CreateAuthorizedUserAsync(healthAuthorityCode, model);
            return Ok(ApiResponse.Result(user));
        }

        // PUT: api/health-authorities/5/authorized-users/15
        /// <summary>
        /// Update an authorized user for a health authority
        /// </summary>
        /// <param name="healthAuthorityCode"></param>
        /// <param name="authorizedUserId"></param>
        /// <param name="model"></param>
        [HttpPut("{healthAuthorityCode}/authorized-users/{authorizedUserId}", Name = nameof(UpdateAuthorizedUser))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HAAuthorizedUser>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAuthorizedUser(HealthAuthorityCode healthAuthorityCode, int authorizedUserId, HAAuthorizedUserUpdateModel model)
        {
            var user = await _haAuthorizedUserService.GetAuthorizedUserByIdAsync(authorizedUserId);
            if (user == null || user.HealthAuthorityCode != healthAuthorityCode)
            {
                return NotFound(ApiResponse.Message($"HA Authorized User not found with id {authorizedUserId} under HA with code {healthAuthorityCode}"));
            }

            var updatedBanner = await _haAuthorizedUserService.UpdateAuthorizedUserAsync(authorizedUserId, model);
            return Ok(ApiResponse.Result(updatedBanner));
        }

        // GET: api/health-authorities/5/authorized-users
        /// <summary>
        /// Get Authorized users for a HA
        /// </summary>
        /// <param name="healthAuthorityCode"></param>
        [HttpGet("{healthAuthorityCode}/authorized-users", Name = nameof(GetAuthorizedUsersByHA))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HAAuthorizedUser>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUsersByHA(HealthAuthorityCode healthAuthorityCode)
        {
            var users = await _haAuthorizedUserService.GetAuthorizedUsersByHACodeAsync(healthAuthorityCode);
            return Ok(ApiResponse.Result(users));
        }

        // GET: api/health-authorities/5/authorized-users/5
        /// <summary>
        /// Get Authorized by id for a HA
        /// </summary>
        /// <param name="healthAuthorityCode"></param>
        /// <param name="authorizedUserId"></param>
        [HttpGet("{healthAuthorityCode}/authorized-users/{authorizedUserId}", Name = nameof(GetAuthorizedUserById))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<HAAuthorizedUser>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUserById(HealthAuthorityCode healthAuthorityCode, int authorizedUserId)
        {
            var user = await _haAuthorizedUserService.GetAuthorizedUserByIdForHaAsync(healthAuthorityCode, authorizedUserId);
            return Ok(ApiResponse.Result(user));
        }

        // DELETE: api/health-authorities/5/authorized-users/5
        /// <summary>
        /// Delete authorized user
        /// </summary>
        /// <param name="healthAuthorityCode"></param>
        /// <param name="authorizedUserId"></param>
        [HttpDelete("{healthAuthorityCode}/authorized-users/{authorizedUserId}", Name = nameof(DeleteAuthorizedUser))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteAuthorizedUser(HealthAuthorityCode healthAuthorityCode, int authorizedUserId)
        {
            await _haAuthorizedUserService.RemoveAuthorizedUserAsync(healthAuthorityCode, authorizedUserId);
            return Ok();
        }
    }
}
