using System;
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
    [Authorize(Policy = AuthConstants.USER_POLICY, Roles = AuthConstants.FEATURE_SITE_REGISTRATION)]
    public class SitesController : ControllerBase
    {
        private readonly ISiteService _siteService;
        private readonly IPartyService _partyService;
        private readonly IOrganizationService _organizationService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;
        public SitesController(
            ISiteService siteService,
            IPartyService partyService,
            IOrganizationService organizationService,
            IRazorConverterService razorConverterService,
            IEmailService emailService,
            IDocumentService documentService)
        {
            _siteService = siteService;
            _partyService = partyService;
            _organizationService = organizationService;
            _razorConverterService = razorConverterService;
            _emailService = emailService;
            _documentService = documentService;
        }

        // Temporary endpoint for admins until fruit loops
        // James avert your eyes!  This is not the code you're looking for...
        // GET: api/Sites
        /// <summary>
        /// Gets all of the Sites.
        /// </summary>
        [HttpGet(Name = nameof(GetAllSites))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Site>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Site>>> GetAllSites()
        {
            // JAMES! Why are you still looking at this code :)
            // If you see this code, and you're not James, and fruitloops is released, and this code still exists, DELETE IT QUICK!
            return Ok(ApiResponse.Result(await _siteService.GetSitesAsync()));
        }

        // GET: api/Sites
        /// <summary>
        /// Gets all of the Sites for an organization, or all sites if user has ADMIN role
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("/api/organizations/{organizationId:int}/sites", Name = nameof(GetSites))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Site>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Site>>> GetSites(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            IEnumerable<Site> sites = null;

            if (User.IsAdmin() || User.HasAdminView())
            {
                sites = await _siteService.GetSitesAsync();
            }
            else
            {
                sites = await _siteService.GetSitesAsync(organizationId);
            }

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

            if (!User.CanEdit(site.Provisioner))
            {
                return Forbid();
            }

            return Ok(ApiResponse.Result(site));
        }

        // POST: api/Sites
        /// <summary>
        /// Creates a new Site.
        /// <param name="organizationId"></param>
        /// </summary>
        [HttpPost("/api/organizations/{organizationId:int}/sites", Name = nameof(CreateSite))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Site>> CreateSite(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            var createdSiteId = await _siteService.CreateSiteAsync(organizationId);

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
        public async Task<IActionResult> UpdateSite(int siteId, Site updatedSite, [FromQuery] bool isCompleted)
        {
            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

            if (!User.CanEdit(party))
            {
                return Forbid();
            }

            await _siteService.UpdateSiteAsync(siteId, updatedSite, isCompleted);

            return NoContent();
        }

        // PUT: api/Sites/5/completed
        /// <summary>
        /// Updates a sites state
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/completed", Name = nameof(UpdateSiteCompleted))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSiteCompleted(int siteId)
        {
            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

            if (!User.CanEdit(party))
            {
                return Forbid();
            }

            await _siteService.UpdateSiteCompletedAsync(siteId);

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

            if (!User.CanEdit(site.Provisioner))
            {
                return Forbid();
            }

            await _siteService.DeleteSiteAsync(siteId);

            return Ok(ApiResponse.Result(site));
        }

        // POST: api/sites/5/submission
        /// <summary>
        /// Submits the given site for adjudication.
        /// </summary>
        [HttpPost("{siteId}/submission", Name = nameof(SubmitSiteRegistration))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Site>> SubmitSiteRegistration(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!User.CanEdit(site.Provisioner))
            {
                return Forbid();
            }

            site = await _siteService.SubmitRegistrationAsync(siteId);
            await _emailService.SendSiteRegistrationAsync(site);

            return Ok(ApiResponse.Result(site));
        }

        // POST: api/sites/5/business-licence
        /// <summary>
        /// Creates a new Business Licence for a site.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="filename"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/business-licence", Name = nameof(CreateBusinessLicence))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status201Created)]
        public async Task<ActionResult<BusinessLicenceDocument>> CreateBusinessLicence(int siteId, [FromQuery] Guid documentGuid, [FromQuery] string filename)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!User.CanEdit(site.Provisioner))
            {
                return Forbid();
            }

            var licence = await _siteService.AddBusinessLicenceAsync(site.Id, documentGuid, filename);

            return Ok(ApiResponse.Result(licence));
        }

        // Get: api/sites/5/business-licence
        /// <summary>
        /// Gets a new Business Licence for a site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/business-licence", Name = nameof(CreateBusinessLicence))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status201Created)]
        public async Task<ActionResult<IEnumerable<BusinessLicenceDocument>>> GetBusinessLicence(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!User.CanEdit(site.Provisioner))
            {
                return Forbid();
            }

            var licences = await _siteService.GetBusinessLicencesAsync(site.Id);

            return Ok(ApiResponse.Result(licences));
        }

        // PUT: api/Sites/5/pec
        /// <summary>
        /// Update the PEC code.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="pecCode"></param>
        [HttpPut("{siteId}/pec", Name = nameof(UpdatePecCode))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePecCode(int siteId, FromBodyText pecCode)
        {
            if (string.IsNullOrWhiteSpace(pecCode))
            {
                this.ModelState.AddModelError("Site.PEC", "PEC Code was not provided");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

            if (!User.CanEdit(party))
            {
                return Forbid();
            }

            var updatedSite = await _siteService.UpdatePecCode(siteId, pecCode);

            return Ok(ApiResponse.Result(updatedSite));
        }

        // Get: api/site/5/latest-business-licence
        /// <summary>
        /// Gets the latest business licence by site download token.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/latest-business-licence", Name = nameof(GetLatestBusinessLicenceDownloadToken))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<SignedAgreementDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetLatestBusinessLicenceDownloadToken(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!User.CanEdit(site.Provisioner))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForLatestBusinessLicenceDocument(siteId);

            return Ok(ApiResponse.Result(token));
        }
    }
}
