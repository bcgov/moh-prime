using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Prime.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AuthConstants.USER_POLICY, Roles = AuthConstants.FEATURE_SITE_REGISTRATION)]
    public class SitesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISiteService _siteService;
        private readonly IPartyService _partyService;
        private readonly IOrganizationService _organizationService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;

        public SitesController(
            IMapper mapper,
            ISiteService siteService,
            IPartyService partyService,
            IOrganizationService organizationService,
            IRazorConverterService razorConverterService,
            IEmailService emailService,
            IDocumentService documentService,
            IAdminService adminService,
            IBusinessEventService businessEventService)
        {
            _mapper = mapper;
            _siteService = siteService;
            _partyService = partyService;
            _organizationService = organizationService;
            _razorConverterService = razorConverterService;
            _emailService = emailService;
            _documentService = documentService;
            _adminService = adminService;
            _businessEventService = businessEventService;
        }

        // GET: api/Sites
        /// <summary>
        /// Gets all of the Sites for an organization, or all sites if user has ADMIN role
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="verbose"></param>
        [HttpGet("/api/organizations/{organizationId:int}/sites", Name = nameof(GetSites))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Site>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Site>>> GetSites(int organizationId, [FromQuery] bool verbose)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            var sites = await _siteService.GetSitesAsync(organizationId);

            if (verbose)
            {
                return Ok(ApiResponse.Result(sites));
            }
            else
            {
                return Ok(ApiResponse.Result(_mapper.Map<IEnumerable<SiteListViewModel>>(sites)));
            }
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

            if (!site.Provisioner.PermissionsRecord().ViewableBy(User))
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
        [HttpPut("{siteId}", Name = nameof(UpdateSite))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSite(int siteId, SiteUpdateModel updatedSite)
        {
            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().EditableBy(User))
            {
                return Forbid();
            }

            await _siteService.UpdateSiteAsync(siteId, updatedSite);

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

            if (!site.Provisioner.PermissionsRecord().EditableBy(User))
            {
                return Forbid();
            }

            await _siteService.UpdateCompletedAsync(siteId);

            return NoContent();
        }

        // PUT: api/Sites/5/adjudicator
        /// <summary>
        /// Add a site's assigned adjudicator.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="adjudicatorId"></param>
        [HttpPut("{siteId}/adjudicator", Name = nameof(SetSiteAdjudicator))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Site>> SetSiteAdjudicator(int siteId, [FromQuery] int? adjudicatorId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}."));
            }

            if ((adjudicatorId.HasValue && !await _adminService.AdminExistsAsync(adjudicatorId.Value)))
            {
                return NotFound(ApiResponse.Message($"Admin not found with id {adjudicatorId.Value}."));
            }

            Admin admin = (adjudicatorId.HasValue)
                ? await _adminService.GetAdminAsync(adjudicatorId.Value)
                : await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var updatedSite = await _siteService.UpdateSiteAdjudicator(site, admin);
            // TODO implement business events for sites
            // await _businessEventService.CreateAdminActionEventAsync(siteId, "Admin claimed site");

            return Ok(ApiResponse.Result(updatedSite));
        }

        // DELETE: api/Site/5/adjudicator
        /// <summary>
        /// Remove an site's assigned adjudicator.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpDelete("{siteId}/adjudicator", Name = nameof(RemoveSiteAdjudicator))]
        [Authorize(Policy = AuthConstants.ADMIN_POLICY)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Site>> RemoveSiteAdjudicator(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}."));
            }

            var updatedSite = await _siteService.UpdateSiteAdjudicator(site.Id);
            // TODO implement business events for sites
            // await _businessEventService.CreateAdminActionEventAsync(siteId, "Admin disclaimed site");

            return Ok(ApiResponse.Result(updatedSite));
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
            if (!site.Provisioner.PermissionsRecord().EditableBy(User))
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
            if (!site.Provisioner.PermissionsRecord().EditableBy(User))
            {
                return Forbid();
            }

            site = await _siteService.SubmitRegistrationAsync(siteId);
            await _emailService.SendSiteRegistrationAsync(site);
            await _emailService.SendRemoteUsersNotificationAsync(site, site.RemoteUsers);

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
            if (!site.Provisioner.PermissionsRecord().EditableBy(User))
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
            if (!site.Provisioner.PermissionsRecord().ViewableBy(User))
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

            if (!site.Provisioner.PermissionsRecord().EditableBy(User))
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
            if (!site.Provisioner.PermissionsRecord().EditableBy(User))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForLatestBusinessLicenceDocument(siteId);

            return Ok(ApiResponse.Result(token));
        }

        // POST: api/Sites/5/remote-users-email
        /// <summary>
        /// Send HIBC an email when remote users are updated for a submitted site
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/remote-users-email", Name = nameof(sendRemoteUsersEmail))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> sendRemoteUsersEmail(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            await _emailService.SendRemoteUsersUpdatedAsync(site);
            return NoContent();
        }

        // POST: api/Sites/5/remote-users-email-admin
        /// <summary>
        /// Send HIBC an email when remote users are updated for a submitted site
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/remote-users-email-admin", Name = nameof(sendRemoteUsersEmailAdmin))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> sendRemoteUsersEmailAdmin(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().ViewableBy(User))
            {
                return Forbid();
            }

            await _emailService.SendRemoteUsersUpdatedAsync(site);
            return NoContent();
        }

        // POST: api/Sites/5/remote-users-email-user
        /// <summary>
        /// Send user an email when they are declared as a remote user against a site
        /// so they can sequence requesting Remote Access on their PRIME practitioner enrolment
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="remoteUsers"></param>
        [HttpPost("{siteId}/remote-users-email-user", Name = nameof(sendRemoteUsersEmailUser))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> sendRemoteUsersEmailUser(int siteId, IEnumerable<RemoteUser> remoteUsers)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().ViewableBy(User))
            {
                return Forbid();
            }

            await _emailService.SendRemoteUsersNotificationAsync(site, remoteUsers);
            return NoContent();
        }

    }
}
