using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels.HealthAuthorities;

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

        // GET: api/health-authorities
        /// <summary>
        /// Gets all of the Health Authority Organizations.
        /// </summary>
        [HttpGet(Name = nameof(GetHealthAuthorities))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthorityListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthorities()
        {
            var ha = new Configuration.HealthAuthorityConfiguration().SeedData
                .Select(x => new HealthAuthorityListViewModel
                {
                    Id = (int)x.Code,
                    Name = x.Name
                });

            return Ok(ApiResponse.Result(ha));
        }

        // GET: api/health-authorities/5
        /// <summary>
        /// Gets a specific Health Authority Organization.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        [HttpGet("{healthAuthorityId}", Name = nameof(GetHealthAuthorityById))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthorityViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthorityById(int healthAuthorityId)
        {
            var ha = new Configuration.HealthAuthorityConfiguration().SeedData
                .Where(x => (int)x.Code == healthAuthorityId)
                .Select(x => new HealthAuthorityViewModel
                {
                    Name = x.Name,
                    CareTypes = Enumerable.Empty<string>(),
                    Vendors = Enumerable.Empty<Vendor>(),
                    TechnicalSupports = Enumerable.Empty<Contact>(),
                    PharmanetAdministrators = Enumerable.Empty<Contact>()
                })
                .SingleOrDefault();

            if (ha == null)
            {
                return NotFound();
            }

            return Ok(ApiResponse.Result(ha));
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

        // GET: api/health-authorities/under-review
        /// <summary>
        /// Get health authority codes with under review authorized users
        /// </summary>
        [HttpGet("under-review", Name = nameof(GetHealthAuthorityCodesWithUnderReviewAuthorizedUsers))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthorityCode>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthorityCodesWithUnderReviewAuthorizedUsers()
        {
            var haIds = await _healthAuthorityService.GetHealthAuthorityCodesWithUnderReviewAuthorizedUsersAsync();
            return Ok(ApiResponse.Result(haIds));
        }
    }
}
