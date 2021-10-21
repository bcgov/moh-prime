using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using Prime.Services;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities")]
    [ApiController]
    public class HealthAuthoritiesController : PrimeControllerBase
    {
        private readonly IHealthAuthorityService _healthAuthorityService;
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;

        public HealthAuthoritiesController(IHealthAuthorityService healthAuthorityService, IHealthAuthoritySiteService healthAuthoritySiteService)
        {
            _healthAuthorityService = healthAuthorityService;
            _healthAuthoritySiteService = healthAuthoritySiteService;
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
            return Ok(await _healthAuthorityService.GetHealthAuthoritiesAsync());
        }

        // GET: api/health-authorities/5
        /// <summary>
        /// Gets a specific Health Authority Organization.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        [HttpGet("{healthAuthorityId:int}", Name = nameof(GetHealthAuthorityById))]
        [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
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

            return Ok(healthAuthority);
        }

        // GET: api/health-authorities/5/authorized-users
        /// <summary>
        /// Get the Authorized Users for a Health Authority
        /// </summary>
        // <param name="healthAuthorityId"></param>
        [HttpGet("{healthAuthorityId}/authorized-users", Name = nameof(GetAuthorizedUsers))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AuthorizedUserViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUsers(int healthAuthorityId)
        {
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            var users = await _healthAuthorityService.GetAuthorizedUsersAsync(healthAuthorityId);
            return Ok(users);
        }

        // GET: api/health-authorities/sites
        /// <summary>
        /// Gets all sites for any health authority.
        /// </summary>
        [HttpGet("sites", Name = nameof(GetAllHealthAuthoritySites))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<V2HealthAuthoritySiteViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllHealthAuthoritySites()
        {
            return Ok(await _healthAuthoritySiteService.GetSitesAsync());
        }

        // PUT: api/health-authorities/5/care-types
        /// <summary>
        /// Updates a specific Health Authority's care types.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="careTypes"></param>
        [HttpPut("{healthAuthorityId}/care-types", Name = nameof(UpdateCareTypes))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCareTypes(int healthAuthorityId, IEnumerable<string> careTypes)
        {
            if (careTypes == null)
            {
                return BadRequest("Health authority care types cannot be null.");
            }
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateVendors(int healthAuthorityId, IEnumerable<int> vendors)
        {
            if (vendors == null)
            {
                return BadRequest("Health authority vendors cannot be null.");
            }
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            await _healthAuthorityService.UpdateVendorsAsync(healthAuthorityId, vendors);

            return NoContent();
        }

        // PUT: api/health-authorities/1/privacy-office
        /// <summary>
        /// Updates the Privacy Office on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="privacyOffice"></param>
        [HttpPut("{healthAuthorityId}/privacy-office", Name = nameof(UpdatePrivacyOffice))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePrivacyOffice(int healthAuthorityId, PrivacyOfficeViewModel privacyOffice)
        {
            await _healthAuthorityService.UpdatePrivacyOfficeAsync(healthAuthorityId, privacyOffice);
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
        public async Task<ActionResult> UpdatePrivacyOfficerContacts(int healthAuthorityId, IEnumerable<ContactViewModel> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityPrivacyOfficer>(healthAuthorityId, contacts);
            return NoContent();
        }

        // GET: api/health-authorities/1/technical-supports/4

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
        public async Task<ActionResult> UpdateTechnicalSupportContacts(int healthAuthorityId, IEnumerable<ContactViewModel> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityTechnicalSupport>(healthAuthorityId, contacts);
            return NoContent();
        }

        // GET: api/health-authorities/1/pharmanet-administrators/2

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
        public async Task<ActionResult> UpdatePharmanetAdministratorContacts(int healthAuthorityId, IEnumerable<ContactViewModel> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityPharmanetAdministrator>(healthAuthorityId, contacts);
            return NoContent();
        }
    }
}
