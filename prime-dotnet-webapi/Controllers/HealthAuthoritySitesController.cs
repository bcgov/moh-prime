using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using Prime.Engines;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Sites;
using System.Linq;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities/{healthAuthorityId}/sites")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class HealthAuthoritySitesController : PrimeControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IHealthAuthorityService _healthAuthorityService;
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;
        private readonly IAuthorizedUserService _authorizedUserService;
        private readonly ISiteService _siteService;
        private readonly ISiteSubmissionService _siteSubmissionService;

        public HealthAuthoritySitesController(
            IEmailService emailService,
            IBusinessEventService businessEventService,
            IHealthAuthorityService healthAuthorityService,
            IHealthAuthoritySiteService healthAuthoritySiteService,
            ISiteService siteService,
            IAuthorizedUserService authorizedUserService,
            ISiteSubmissionService siteSubmissionService
        )
        {
            _emailService = emailService;
            _businessEventService = businessEventService;
            _healthAuthorityService = healthAuthorityService;
            _healthAuthoritySiteService = healthAuthoritySiteService;
            _siteService = siteService;
            _authorizedUserService = authorizedUserService;
            _siteSubmissionService = siteSubmissionService;
        }

        // POST: api/health-authorities/5/sites
        /// <summary>
        /// Creates a new health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="payload"></param>
        [HttpPost(Name = nameof(CreateHealthAuthoritySite))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
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

            var createdSite = await _healthAuthoritySiteService.CreateSiteAsync(healthAuthorityId, payload);

            return CreatedAtAction(
                nameof(GetHealthAuthoritySite),
                new { healthAuthorityId, siteId = createdSite.Id },
                createdSite
            );
        }

        // GET: api/health-authorities/5/sites?healthAuthoritySiteId=1
        /// <summary>
        /// Gets all of the sites for a health authority.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="healthAuthoritySiteId"></param>
        [HttpGet(Name = nameof(GetHealthAuthoritySites))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthoritySiteAdminListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthoritySites(int healthAuthorityId, [FromQuery] int healthAuthoritySiteId)
        {
            var sites = await _healthAuthoritySiteService.GetSitesAsync(healthAuthorityId, healthAuthoritySiteId);

            var notifiedIds = await _siteService.GetNotifiedSiteIdsForAdminAsync(User);
            foreach (var site in sites)
            {
                site.HasNotification = notifiedIds.Contains(site.Id);
            }

            return Ok(sites);
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

            var site = await _healthAuthoritySiteService.GetSiteViewModelAsync(siteId);

            return Ok(site);
        }

        // GET: api/health-authorities/5/sites/5/admin-view
        /// <summary>
        /// Gets a specific health authority site for an admin.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/admin-view", Name = nameof(GetHealthAuthorityAdminSite))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthoritySiteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthorityAdminSite(int healthAuthorityId, int siteId)
        {
            if (!await _healthAuthoritySiteService.SiteExistsAsync(healthAuthorityId, siteId))
            {
                return NotFound($"Health authority site not found with id {siteId}");
            }

            var site = await _healthAuthoritySiteService.GetAdminSiteAsync(siteId);

            return Ok(site);
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

        // PUT: api/health-authorities/5/sites/5
        /// <summary>
        /// Updates a health authority site.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        /// <param name="updateModel"></param>
        [HttpPut("{siteId}", Name = nameof(UpdateHealthAuthoritySite))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
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
            if (updateModel.HealthAuthorityVendorId.HasValue &&
                !await _healthAuthorityService.VendorExistsOnHealthAuthorityAsync(healthAuthorityId, updateModel.HealthAuthorityVendorId.Value))
            {
                return NotFound($"Health Authority Vendor not found with id {updateModel.HealthAuthorityVendorId}");
            }
            if (!await _healthAuthorityService.ValidateSiteSelectionsAsync(healthAuthorityId, updateModel))
            {
                return BadRequest();
            }
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserForUsernameAsync(User.GetPrimeUsername());
            await _healthAuthoritySiteService.UpdateSiteAsync(siteId, updateModel, authorizedUser.Id);
            await _businessEventService.CreateSiteEventAsync(siteId, User.GetPrimeUsername(), "Health Authority Site Updated");

            return NoContent();
        }

        // PUT: api/health-authorities/5/sites/5/site-completed
        /// <summary>
        /// Sets the health authority site as "completed", allowing frontend and backend behavioural changes.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/site-completed", Name = nameof(SetHealthAuthoritySiteCompleted))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
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
        /// <param name="updateModel"></param>
        [HttpPost("{siteId}/submissions", Name = nameof(HealthAuthoritySiteSubmission))]
        [Authorize(Roles = Roles.PrimeEnrollee)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> HealthAuthoritySiteSubmission(int healthAuthorityId, int siteId, HealthAuthoritySiteUpdateModel updateModel)
        {
            if (!await _healthAuthoritySiteService.SiteIsEditableAsync(healthAuthorityId, siteId))
            {
                return NotFound($"No editable Health Authority Site found with Id {siteId}");
            }

            // Check if the user is belong to the same HA as the site's HA
            if (!await _healthAuthoritySiteService.AllowToSubmitSite(siteId, User.GetPrimeUsername()))
            {
                return Forbid();
            }

            if (!await _healthAuthorityService.ValidateSiteSelectionsAsync(healthAuthorityId, siteId))
            {
                return Conflict("Cannot submit Site, one or more selections dependent on the Health Authority are invalid.");
            }

            var status = await _siteService.GetSiteCurrentStatusAsync(siteId);
            if (!SiteStatusStateEngine.AllowableStatusChange(SiteRegistrationAction.Submit, status))
            {
                return BadRequest("Action could not be performed.");
            }
            var authorizedUser = await _authorizedUserService.GetAuthorizedUserForUsernameAsync(User.GetPrimeUsername());

            await _healthAuthoritySiteService.UpdateSiteAsync(siteId, updateModel, authorizedUser.Id);
            await _healthAuthoritySiteService.SiteSubmissionAsync(siteId);
            await _siteSubmissionService.CreateHealthAuthoritySiteSubmissionAsync(siteId);
            await _businessEventService.CreateSiteEmailEventAsync(siteId, User.GetPrimeUsername(), "Health Authority Site has been updated and submitted");

            await _emailService.SendHealthAuthoritySiteRegistrationSubmissionAsync(siteId);
            await _businessEventService.CreateSiteEmailEventAsync(siteId, User.GetPrimeUsername(), "Notified of health authority site registration submission");

            return NoContent();
        }


        // POST: api/health-authorities/5/sites/transfer/from/10/to/11
        /// <summary>
        /// Transfer site from one authorized user to another within a health authority.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="currentAuthorizedUserId"></param>
        /// <param name="newAuthorizedUserId"></param>
        [HttpPost("transfer/from/{currentAuthorizedUserId}/to/{newAuthorizedUserId}", Name = nameof(TransferHealthAuthoritySite))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> TransferHealthAuthoritySite(int healthAuthorityId, int currentAuthorizedUserId, int newAuthorizedUserId)
        {
            var authorizedUsers = await _healthAuthorityService.GetAuthorizedUsersAsync(healthAuthorityId);

            if (authorizedUsers.Any() &&
                authorizedUsers.Any(au => au.Id == currentAuthorizedUserId) &&
                authorizedUsers.Any(au => au.Id == newAuthorizedUserId))
            {
                var currentAuthorizedUser = authorizedUsers.First(au => au.Id == currentAuthorizedUserId);
                var newAuthorizedUser = authorizedUsers.First(au => au.Id == newAuthorizedUserId);

                var siteIds = await _healthAuthoritySiteService.TransferAuthorizedUserAsync(currentAuthorizedUserId, newAuthorizedUserId);
                foreach (int siteId in siteIds)
                {
                    await _businessEventService.CreateSiteEventAsync(siteId,
                        $"Health Authorized Site is transfered from {currentAuthorizedUser.FirstName} {currentAuthorizedUser.LastName} to {newAuthorizedUser.FirstName} {newAuthorizedUser.LastName}");
                }
            }
            else
            {
                return NotFound("Authorized User not found");
            }

            return NoContent();
        }
    }
}
