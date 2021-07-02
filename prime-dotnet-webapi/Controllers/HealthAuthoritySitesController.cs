using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Auth;
using Prime.Services;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities/{healthAuthorityId}/sites")]
    [ApiController]
    public class HealthAuthoritySitesController : PrimeControllerBase
    {
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;

        public HealthAuthoritySitesController(IHealthAuthoritySiteService healthAuthoritySiteService)
        {
            _healthAuthoritySiteService = healthAuthoritySiteService;
        }

        // POST: api/health-authorities/5/sites
        /// <summary>
        /// Creates a new health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="payload"></param>
        [HttpPost(Name = nameof(CreateHealthAuthoritySite))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySite>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateHealthAuthoritySite(int healthAuthorityId, HealthAuthoritySiteVendorViewModel payload)
        {
            var createdSite = await _healthAuthoritySiteService.CreateSiteAsync(healthAuthorityId, payload.VendorCode);

            return CreatedAtAction(
                nameof(GetHealthAuthoritySiteById),
                new { healthAuthorityId, siteId = createdSite.Id },
                createdSite
            );
        }

        // GET: api/health-authorities/5/sites
        /// <summary>
        /// Gets all of the sites for a health authority.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        [HttpGet(Name = nameof(GetHealthAuthoritySites))]
        //[Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthoritySiteViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthoritySites(int healthAuthorityId)
        {
            return Ok(await _healthAuthoritySiteService.GetSitesAsync(healthAuthorityId));
        }

        // GET: api/health-authorities/5/sites/5
        /// <summary>
        /// Gets a specific site for a health authority.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}", Name = nameof(GetHealthAuthoritySiteById))]
        //[Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthoritySiteById(int healthAuthorityId, int siteId)
        {
            var healthAuthority = await _healthAuthoritySiteService.GetSiteAsync(siteId);
            if (healthAuthority == null)
            {
                return NotFound();
            }

            return Ok(healthAuthority);
        }

        // PUT: api/health-authorities/5/sites/5/vendor
        /// <summary>
        /// Updates a specific health authority site vendor.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="payload"></param>
        [HttpPut("{siteId}/vendor", Name = nameof(UpdateVendor))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVendor(int healthAuthorityId, int siteId, HealthAuthoritySiteVendorViewModel payload)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            await _healthAuthoritySiteService.UpdateVendorAsync(siteId, healthAuthorityId, payload.VendorCode);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/site-info
        /// <summary>
        /// Updates a specific health authority site's information.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="payload"></param>
        [HttpPut("{siteId}/site-info", Name = nameof(UpdateSiteInfo))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSiteInfo(int healthAuthorityId, int siteId, HealthAuthoritySiteInfoViewModel payload)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            await _healthAuthoritySiteService.UpdateSiteInfoAsync(siteId, payload);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/care-type
        /// <summary>
        /// Updates a specific Health Authority Site's care type.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="payload"></param>
        [HttpPut("{siteId}/care-type", Name = nameof(UpdateCareType))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCareType(int healthAuthorityId, int siteId, HealthAuthoritySiteCareTypeViewModel payload)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            await _healthAuthoritySiteService.UpdateCareTypeAsync(siteId, payload.CareType);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/address
        /// <summary>
        /// Updates a specific health authority site's address.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="payload"></param>
        [HttpPut("{siteId}/address", Name = nameof(UpdateAddress))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAddress(int healthAuthorityId, int siteId, HealthAuthoritySiteAddressViewModel payload)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            await _healthAuthoritySiteService.UpdatePhysicalAddressAsync(siteId, payload.PhysicalAddress);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/hours-operation
        /// <summary>
        /// Updates a specific health authority site's hours operation.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="payload"></param>
        [HttpPut("{siteId}/hours-operation", Name = nameof(UpdateHoursOperation))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateHoursOperation(int healthAuthorityId, int siteId, HealthAuthoritySiteHoursOperationViewModel payload)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            await _healthAuthoritySiteService.UpdateHoursOperationAsync(siteId, payload);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/remote-users
        /// <summary>
        /// Updates a specific health authority site's remote users.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="payload"></param>
        [HttpPut("{siteId}/remote-users", Name = nameof(UpdateRemoteUsers))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRemoteUsers(int healthAuthorityId, int siteId, HealthAuthoritySiteRemoteUsersViewModel payload)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            await _healthAuthoritySiteService.UpdateRemoteUsersAsync(siteId, payload);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/administrator
        /// <summary>
        /// Updates a specific health authority site's administrator.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="payload"></param>
        [HttpPut("{siteId}/administrator", Name = nameof(UpdateAdministrator))]
        //[Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAdministrator(int healthAuthorityId, int siteId, HealthAuthoritySiteAdministratorViewModel payload)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            await _healthAuthoritySiteService.UpdateAdministratorAsync(siteId, payload);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/finalize/site-completed
        /// <summary>
        /// Sets the health authority site as "completed", allowing frontend and backend behavioural changes.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/site-completed", Name = nameof(SetHealthAuthoritySiteCompleted))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SetHealthAuthoritySiteCompleted(int healthAuthorityId, int siteId)
        {
            // if (!await _healthAuthoritySiteService.SiteIsEditableAsync(siteId))
            // {
            //     return NotFound($"No editable health authority site found with site id {siteId}");
            // }

            await _healthAuthoritySiteService.SetSiteCompletedAsync(siteId);

            return Ok();
        }

        // POST: api/health-authorities/5/sites/5/finalize
        /// <summary>
        /// Finalizes a specific health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/finalize", Name = nameof(FinalizeHealthAuthoritySite))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> FinalizeHealthAuthoritySite(int healthAuthorityId, int siteId)
        {
            // if (!await _healthAuthoritySiteService.SiteIsEditableAsync(siteId))
            // {
            //     return NotFound($"No editable health authority site found with site id {siteId}");
            // }

            await _healthAuthoritySiteService.FinalizeSubmissionAsync(siteId);

            return Ok();
        }
    }
}
