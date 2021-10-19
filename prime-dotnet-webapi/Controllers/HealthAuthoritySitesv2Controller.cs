using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using Prime.Services;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities/{healthAuthorityId}/sites")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee)]
    public class HealthAuthoritySitesV2Controller : PrimeControllerBase
    {
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;

        public HealthAuthoritySitesV2Controller(IHealthAuthoritySiteService healthAuthoritySiteService)
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySite>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateHealthAuthoritySite(int healthAuthorityId, HealthAuthoritySiteCreateModel payload)
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
        [HttpGet("{siteId}", Name = nameof(GetHealthAuthoritySiteById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthoritySiteById(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            var site = await _healthAuthoritySiteService.GetSiteAsync(siteId);

            return Ok(site);
        }

        // GET: api/health-authorities/5/sites/5/hours-operation
        /// <summary>
        /// Gets a sites hours of operations.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/hours-operation", Name = nameof(GetHoursOfOperation))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteHoursOperationViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHoursOfOperation(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            var siteHoursOfOperation = await _healthAuthoritySiteService.GetHoursOfOperationAsync(healthAuthorityId, siteId);

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
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteRemoteUsersViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRemoteUsers(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            var siteRemoteUsers = await _healthAuthoritySiteService.GetRemoteUsersAsync(healthAuthorityId, siteId);

            return Ok(siteRemoteUsers);
        }

        // PUT: api/health-authorities/5/sites/5/vendor
        /// <summary>
        /// Updates a health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="updateModel"></param>
        [HttpPut("{siteId}", Name = nameof(UpdateSite))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateSite(int healthAuthorityId, int siteId, HealthAuthorityUpdateViewModel updateModel)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }
            // TODO SiteIsEditable doesn't exist
            // if (!await _healthAuthoritySiteService.SiteIsEditableAsync(siteId))
            // {
            //     return NotFound($"No editable health authority site found with site id {siteId}");
            // }

            await _healthAuthoritySiteService.UpdateSiteAsync(siteId, updateModel);

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
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }
            // TODO SiteIsEditable doesn't exist
            // if (!await _healthAuthoritySiteService.SiteIsEditableAsync(siteId))
            // {
            //     return NotFound($"No editable health authority site found with site id {siteId}");
            // }

            await _healthAuthoritySiteService.SetSiteCompletedAsync(siteId);

            return Ok();
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> HealthAuthoritySiteSubmission(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }
            // TODO SiteIsEditable doesn't exist
            // if (!await _healthAuthoritySiteService.SiteIsEditableAsync(siteId))
            // {
            //     return NotFound($"No editable health authority site found with site id {siteId}");
            // }

            await _healthAuthoritySiteService.SiteSubmissionAsync(siteId);

            return Ok();
        }
    }
}
