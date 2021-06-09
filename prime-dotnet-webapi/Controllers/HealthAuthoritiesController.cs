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
using Prime.Models.HealthAuthorities;
using Prime.Extensions;

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
            return Ok(ApiResponse.Result(await _healthAuthorityService.GetHealthAuthoritiesAsync()));
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
            var healthAuthority = await _healthAuthorityService.GetHealthAuthorityAsync(healthAuthorityId);

            if (healthAuthority == null)
            {
                return NotFound();
            }

            return Ok(ApiResponse.Result(healthAuthority));
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

        // PUT: api/health-authorities/5/care-types
        /// <summary>
        /// Updates a specific Health authorities care types.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="careTypes"></param>
        [HttpPut("{healthAuthorityId}/care-types", Name = nameof(UpdateCareTypes))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCareTypes(int healthAuthorityId, string[] careTypes)
        {
            if (careTypes == null)
            {
                ModelState.AddModelError("HealthAuthorityCareType", "Health authority care types cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound(ApiResponse.Message($"Health Authority not found with id {healthAuthorityId}"));
            }

            var updatedHealthAuthorityId = await _healthAuthorityService.UpdateCareTypesAsync(healthAuthorityId, careTypes);
            if (updatedHealthAuthorityId.IsInvalidId())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Could not update the health authority care types." });
            }

            return NoContent();
        }

        // PUT: api/health-authorities/5/vendors
        /// <summary>
        /// Updates a specific Health authorities vendors.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="vendors"></param>
        [HttpPut("{healthAuthorityId}/vendors", Name = nameof(UpdateVendors))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVendors(int healthAuthorityId, int[] vendors)
        {
            if (vendors == null)
            {
                ModelState.AddModelError("HealthAuthorityVendors", "Health authority vendors cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound(ApiResponse.Message($"Health Authority not found with id {healthAuthorityId}"));
            }

            var updatedHealthAuthorityId = await _healthAuthorityService.UpdateVendorsAsync(healthAuthorityId, vendors);
            if (updatedHealthAuthorityId.IsInvalidId())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Could not update the health authority vendors." });
            }

            return NoContent();
        }

        // PUT: api/health-authorities/1/technical-supports
        /// <summary>
        /// Updates technical support contacts on a health authority
        /// </summary>
        /// <param name="healthAuthorityOrganizationId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityOrganizationId}/technical-supports", Name = nameof(UpdateTechnicalSupportContacts))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTechnicalSupportContacts(int healthAuthorityOrganizationId, IEnumerable<Contact> contacts)
        {
            await _healthAuthorityService.UpdateContacts<HealthAuthorityTechnicalSupport>(healthAuthorityOrganizationId, contacts);
            return NoContent();
        }

        // PUT: api/health-authorities/1/pharmanet-administrators
        /// <summary>
        /// Updates pharmanet administrator contacts on a health authority
        /// </summary>
        /// <param name="healthAuthorityOrganizationId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityOrganizationId}/pharmanet-administrators", Name = nameof(UpdatePharmanetAdministratorContacts))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePharmanetAdministratorContacts(int healthAuthorityOrganizationId, IEnumerable<Contact> contacts)
        {
            await _healthAuthorityService.UpdateContacts<HealthAuthorityTechnicalSupport>(healthAuthorityOrganizationId, contacts);
            return NoContent();
        }
    }
}
