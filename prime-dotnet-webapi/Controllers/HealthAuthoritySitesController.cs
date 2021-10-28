using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using Prime.Services;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Sites;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities/{healthAuthorityId}/sites")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee)]
    public class HealthAuthoritySitesController : PrimeControllerBase
    {
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;
        private readonly IHealthAuthorityService _healthAuthorityService;
        private readonly ISiteService _siteService;

        public HealthAuthoritySitesController(
            IHealthAuthoritySiteService healthAuthoritySiteService,
            IHealthAuthorityService healthAuthorityService,
            ISiteService siteService)
        {
            _healthAuthoritySiteService = healthAuthoritySiteService;
            _healthAuthorityService = healthAuthorityService;
            _siteService = siteService;
        }

        // POST: api/health-authorities/5/sites
        /// <summary>
        /// Creates a new health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="payload"></param>
        [HttpPost(Name = nameof(CreateHealthAuthoritySite))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteViewModel>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateHealthAuthoritySite(int healthAuthorityId, HealthAuthoritySiteCreateModel payload)
        {
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }
            if (!await _healthAuthorityService.AuthorizedUserExistsOnHealthAuthorityAsync(healthAuthorityId, payload.AuthorizedUserId))
            {
                return Forbid();
            }
            if (!await _healthAuthorityService.VendorExistsOnHealthAuthorityAsync(healthAuthorityId, payload.HealthAuthorityVendorId))
            {
                return NotFound($"Health Authority Vendor not found with id {payload.HealthAuthorityVendorId}");
            }

            var createdSite = await _healthAuthoritySiteService.CreateSiteAsync(healthAuthorityId, payload);

            return CreatedAtAction(
                nameof(GetHealthAuthoritySite),
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthoritySiteViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthoritySites(int healthAuthorityId)
        {
            return Ok(await _healthAuthoritySiteService.GetSitesAsync(healthAuthorityId));
        }

        // GET: api/health-authorities/5/sites/5
        /// <summary>
        /// Gets a specific health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}", Name = nameof(GetHealthAuthoritySite))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthoritySite(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            var site = await _healthAuthoritySiteService.GetSiteAsync(siteId);

            return Ok(site);
            // return Ok(new HealthAuthoritySiteViewModel { Id = siteId });
        }

        // GET: api/health-authorities/5/sites/5/hours-operation
        /// <summary>
        /// Gets a Site's hours of operations.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/hours-operation", Name = nameof(GetHoursOfOperation))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BusinessDayViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHoursOfOperation(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            var siteHoursOfOperation = await _siteService.GetBusinessHoursAsync(siteId);

            return Ok(siteHoursOfOperation);
        }

        // GET: api/health-authorities/5/sites/5/remote-users
        /// <summary>
        /// Gets a sites remote users.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/remote-users", Name = nameof(GetRemoteUsers))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<RemoteUserViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRemoteUsers(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            var siteRemoteUsers = await _siteService.GetRemoteUsersAsync(siteId);

            return Ok(siteRemoteUsers);
        }

        // PUT: api/health-authorities/5/sites/5
        /// <summary>
        /// Updates a health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="updateModel"></param>
        [HttpPut("{siteId}", Name = nameof(UpdateHealthAuthoritySite))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateHealthAuthoritySite(int healthAuthorityId, int siteId, HealthAuthoritySiteUpdateModel updateModel)
        {
            if (!await _healthAuthoritySiteService.SiteIsEditableAsync(healthAuthorityId, siteId))
            {
                return NotFound($"No Editable Health Authority Site found with id {siteId}");
            }
            if (!await _healthAuthorityService.ValidateSiteUpdateSelectionsAsync(healthAuthorityId, updateModel))
            {
                return BadRequest();
            }

            await _healthAuthoritySiteService.UpdateSiteAsync(siteId, updateModel);

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/site-completed
        /// <summary>
        /// Sets the health authority site as "completed", allowing frontend and backend behavioural changes.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/site-completed", Name = nameof(SetHealthAuthoritySiteCompleted))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SetHealthAuthoritySiteCompleted(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }
            if (!await _healthAuthoritySiteService.SiteIsEditableAsync(healthAuthorityId, siteId))
            {
                return NotFound($"No editable health authority site found with site id {siteId}");
            }

            await _healthAuthoritySiteService.SetSiteCompletedAsync(siteId);

            return NoContent();
        }

        // POST: api/health-authorities/5/sites/5/submissions
        /// <summary>
        /// Submits a health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/submissions", Name = nameof(HealthAuthoritySiteSubmission))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> HealthAuthoritySiteSubmission(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteIsEditableAsync(healthAuthorityId, siteId))
            {
                return NotFound($"No editable Health Authority Site found with Id {siteId}");
            }
            if (!await _healthAuthorityService.ValidateSiteSelectionsAsync(healthAuthorityId, siteId))
            {
                return Conflict("Cannot submit Site, one or more selections dependant on the Health Authority are invalid.");
            }

            await _healthAuthoritySiteService.SiteSubmissionAsync(siteId);

            return NoContent();
        }
    }
}
