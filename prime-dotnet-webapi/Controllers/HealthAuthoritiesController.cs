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
        /// Updates a specific Health Authority's care types.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="careTypes"></param>
        [HttpPut("{healthAuthorityId}/care-types", Name = nameof(UpdateCareTypes))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCareTypes(int healthAuthorityId, IEnumerable<string> careTypes)
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

            await _healthAuthorityService.UpdateCareTypesAsync(healthAuthorityId, careTypes);

            return NoContent();
        }

        // PUT: api/health-authorities/5/vendors
        /// <summary>
        /// Updates a specific Health authorities vendors.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="vendors"></param>
        [HttpPut("{healthAuthorityId}/vendors", Name = nameof(UpdateVendors))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVendors(int healthAuthorityId, IEnumerable<int> vendors)
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

            await _healthAuthorityService.UpdateVendorsAsync(healthAuthorityId, vendors);

            return NoContent();
        }

        // PUT: api/health-authorities/1/privacy-officers
        /// <summary>
        /// Updates the Privacy Officer contacts on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityId}/privacy-officers", Name = nameof(UpdatePrivacyOfficerContacts))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePrivacyOfficerContacts(int healthAuthorityId, IEnumerable<Contact> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityPrivacyOfficer>(healthAuthorityId, contacts);
            return NoContent();
        }

        // PUT: api/health-authorities/1/technical-supports
        /// <summary>
        /// Updates the Technical Support contacts on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityId}/technical-supports", Name = nameof(UpdateTechnicalSupportContacts))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTechnicalSupportContacts(int healthAuthorityId, IEnumerable<Contact> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityTechnicalSupport>(healthAuthorityId, contacts);
            return NoContent();
        }

        // PUT: api/health-authorities/1/pharmanet-administrators
        /// <summary>
        /// Updates the Pharmanet Administrator contacts on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityId}/pharmanet-administrators", Name = nameof(UpdatePharmanetAdministratorContacts))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePharmanetAdministratorContacts(int healthAuthorityId, IEnumerable<Contact> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityPharmanetAdministrator>(healthAuthorityId, contacts);
            return NoContent();
        }
    }
}
