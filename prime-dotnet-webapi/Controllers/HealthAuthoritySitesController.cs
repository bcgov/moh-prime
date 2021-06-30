using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Auth;
using Prime.Services;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities")]
    [ApiController]
    public class HealthAuthoritySitesController : PrimeControllerBase
    {
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;

        public HealthAuthoritySitesController(IHealthAuthoritySiteService healthAuthoritySiteService)
        {
            _healthAuthoritySiteService = healthAuthoritySiteService;
        }

        // GET: api/health-authorities/5/sites
        /// <summary>
        /// Gets all of the Sites for a Health Authority Organization.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        [HttpGet("{healthAuthorityId}/sites", Name = nameof(GetHealthAuthoritySites))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthorityListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthoritySites(int healthAuthorityId)
        {
            return Ok(await _healthAuthoritySiteService.GetSitesAsync(healthAuthorityId));
        }

        // GET: api/health-authorities/5/sites/5
        /// <summary>
        /// Gets a specific Site for a Health Authority Organization.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{healthAuthorityId}/sites/{siteId}", Name = nameof(GetHealthAuthorityById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthorityViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthorityById(int healthAuthorityId, int siteId)
        {
            var healthAuthority = await _healthAuthoritySiteService.GetSiteAsync(siteId);
            if (healthAuthority == null)
            {
                return NotFound();
            }

            return Ok(healthAuthority);
        }

        // PUT: api/health-authorities/5/sites/5/care-type
        /// <summary>
        /// Updates a specific Health Authority Site's care type.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="careType"></param>
        [HttpPut("{healthAuthorityId}/sites/{siteId}/care-types", Name = nameof(UpdateCareTypes))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCareTypes(int healthAuthorityId, int siteId, string careType)
        {
            if (careType == null)
            {
                return BadRequest("Health authority site care type cannot be null.");
            }
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            await _healthAuthoritySiteService.UpdateCareTypeAsync(siteId, careType);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/vendor
        /// <summary>
        /// Updates a specific Health authority Site vendor.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// /// <param name="siteId"></param>
        /// <param name="vendor"></param>
        [HttpPut("{healthAuthorityId}/sites/{siteId}/vendor", Name = nameof(UpdateVendors))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVendors(int healthAuthorityId, int siteId, int vendor)
        {
            if (vendor == null)
            {
                return BadRequest("Health authority site vendor cannot be null.");
            }
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            await _healthAuthoritySiteService.UpdateVendorAsync(siteId, vendor);

            return NoContent();
        }
    }
}
