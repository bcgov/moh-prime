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
        private readonly IHealthAuthorityService _healthAuthorityService;

        public HealthAuthoritiesController(IHealthAuthorityService healthAuthorityService)
        {
            _healthAuthorityService = healthAuthorityService;
        }

        // GET: api/health-authorities/5/authorized-users
        /// <summary>
        /// Get Authorized users for a health authority
        /// </summary>
        // <param name="healthAuthorityCode"></param>
        [HttpGet("{healthAuthorityCode}/authorized-users", Name = nameof(GetAuthorizedUsersByHealthAuthority))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AuthorizedUser>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUsersByHealthAuthority(HealthAuthorityCode healthAuthorityCode)
        {
            var users = await _healthAuthorityService.GetAuthorizedUsersByHealthAuthorityAsync(healthAuthorityCode);
            return Ok(ApiResponse.Result(users));
        }
    }
}
