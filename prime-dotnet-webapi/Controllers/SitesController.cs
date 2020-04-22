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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AuthConstants.USER_POLICY)]
    public class SitesController : ControllerBase
    {
        private readonly ISiteService _siteService;
        private readonly IPartyService _partyService;
        private readonly IRazorConverterService _razorConverterService;

        public SitesController(
            ISiteService siteService,
            IPartyService partyService,
            IRazorConverterService razorConverterService)
        {
            _siteService = siteService;
            _partyService = partyService;
            _razorConverterService = razorConverterService;
        }

        // GET: api/Sites
        /// <summary>
        /// Gets all of the Sites for a user.
        /// </summary>
        [HttpGet(Name = nameof(GetSites))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Site>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Site>>> GetSites()
        {
            if (!User.HasSiteRegistrationFeature())
            {
                return Forbid();
            }

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

            IEnumerable<Site> sites = (party != null)
                ? await _siteService.GetSitesAsync(party.Id)
                : new List<Site>();

            return Ok(ApiResponse.Result(sites));
        }

        // GET: api/Sites/5
        /// <summary>
        /// Gets a specific Site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}", Name = nameof(GetSiteById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Site>> GetSiteById(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (!User.HasSiteRegistrationFeature() || !User.PartyCanEdit(site.Provisioner))
            {
                return Forbid();
            }

            return Ok(ApiResponse.Result(site));
        }

        // POST: api/Sites
        /// <summary>
        /// Creates a new Site.
        /// </summary>
        [HttpPost(Name = nameof(CreateSite))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Site>> CreateSite(Party party)
        {
            if (party == null)
            {
                this.ModelState.AddModelError("Party", "Could not create an site, the passed in Party cannot be null.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            if (!User.HasSiteRegistrationFeature())
            {
                return Forbid();
            }

            var createdSiteId = await _siteService.CreateSiteAsync(party);

            var createdSite = await _siteService.GetSiteAsync(createdSiteId);

            return CreatedAtAction(
                nameof(GetSiteById),
                new { siteId = createdSiteId },
                ApiResponse.Result(createdSite)
            );
        }

        // PUT: api/Sites/5
        /// <summary>
        /// Updates a specific Site.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="updatedSite"></param>
        /// <param name="isCompleted"></param>
        [HttpPut("{siteId}", Name = nameof(UpdateSite))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSite(int siteId, Site updatedSite, [FromQuery]bool isCompleted)
        {
            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!User.HasSiteRegistrationFeature() || !User.PartyCanEdit(site.Provisioner))
            {
                return Forbid();
            }

            await _siteService.UpdateSiteAsync(siteId, updatedSite, isCompleted);

            return NoContent();
        }

        // DELETE: api/Sites/5
        /// <summary>
        /// Deletes a specific Site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpDelete("{siteId}", Name = nameof(DeleteSite))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Site>> DeleteSite(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!User.HasSiteRegistrationFeature() || !User.PartyCanEdit(site.Provisioner))
            {
                return Forbid();
            }

            await _siteService.DeleteSiteAsync(siteId);

            return Ok(ApiResponse.Result(site));
        }

        // GET: api/Sites/organization-agreement
        /// <summary>
        /// Get the organization agreement.
        /// </summary>
        [HttpGet("organization-agreement", Name = nameof(GetOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetOrganizationAgreement()
        {
            var agreement = await _razorConverterService.RenderViewToStringAsync("/Views/OrganizationAgreement.cshtml", new Site());

            return Ok(ApiResponse.Result(agreement));
        }


        // PUT: api/Sites/5/organization-agreement
        /// <summary>
        /// Accept an organization agreement
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/organization-agreement", Name = nameof(AcceptCurrentOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptCurrentOrganizationAgreement(int siteId)
        {
            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!User.HasSiteRegistrationFeature() || !User.PartyCanEdit(site.Provisioner))
            {
                return Forbid();
            }

            await _siteService.AcceptCurrentOrganizationAgreementAsync(site.Location.Organization.SigningAuthorityId);

            return NoContent();
        }
    }
}
