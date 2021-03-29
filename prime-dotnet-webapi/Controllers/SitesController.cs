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
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class SitesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISiteService _siteService;
        private readonly IOrganizationService _organizationService;
        private readonly IEmailService _emailService;
        private readonly IDocumentService _documentService;
        private readonly IAdminService _adminService;

        public SitesController(
            IMapper mapper,
            ISiteService siteService,
            IOrganizationService organizationService,
            IEmailService emailService,
            IDocumentService documentService,
            IAdminService adminService)
        {
            _mapper = mapper;
            _siteService = siteService;
            _organizationService = organizationService;
            _emailService = emailService;
            _documentService = documentService;
            _adminService = adminService;
        }

        // GET: api/Sites
        /// <summary>
        /// Gets all of the Sites for an organization, or all sites if user has ADMIN role
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="verbose"></param>
        [HttpGet("/api/organizations/{organizationId:int}/sites", Name = nameof(GetSites))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Site>> GetSiteById(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
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

            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _siteService.UpdateSiteAsync(siteId, updatedSite);

            return NoContent();
        }

        // PUT: api/Sites/5/completed
        /// <summary>
        /// Set a sites completed state.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/completed", Name = nameof(SetSiteCompleted))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SetSiteCompleted(int siteId)
        {
            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _siteService.UpdateCompletedAsync(siteId, true);

            return NoContent();
        }

        // DELETE: api/Sites/5/completed
        /// <summary>
        /// Remove a sites completed state.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpDelete("{siteId}/completed", Name = nameof(RemoveSiteCompleted))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveSiteCompleted(int siteId)
        {
            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _siteService.UpdateCompletedAsync(siteId, false);

            return NoContent();
        }

        // PUT: api/Sites/5/adjudicator
        /// <summary>
        /// Add a site's assigned adjudicator.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="adjudicatorId"></param>
        [HttpPut("{siteId}/adjudicator", Name = nameof(SetSiteAdjudicator))]
        [Authorize(Roles = Roles.EditSite)]
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

            Admin admin = (adjudicatorId.HasValue)
                ? await _adminService.GetAdminAsync(adjudicatorId.Value)
                : await _adminService.GetAdminAsync(User.GetPrimeUserId());

            if (admin == null)
            {
                return NotFound(ApiResponse.Message($"Admin not found with id {adjudicatorId.Value}."));
            }

            var updatedSite = await _siteService.UpdateSiteAdjudicator(site.Id, admin.Id);
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
        [Authorize(Roles = Roles.EditSite)]
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
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
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
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            site = await _siteService.SubmitRegistrationAsync(siteId);
            await _emailService.SendSiteRegistrationSubmissionAsync(siteId);
            await _emailService.SendRemoteUserNotificationsAsync(site, site.RemoteUsers);

            return Ok(ApiResponse.Result(site));
        }

        // POST: api/sites/5/business-licence
        /// <summary>
        /// Creates a new Business Licence.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="businessLicence"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/business-licence", Name = nameof(CreateBusinessLicence))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResultResponse<BusinessLicence>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BusinessLicence>> CreateBusinessLicence(int siteId, BusinessLicence businessLicence, [FromQuery] Guid documentGuid)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (site.BusinessLicence != null)
            {
                return Conflict(ApiResponse.Message($"Business Licence exists for site with id {siteId}"));
            }

            var licence = await _siteService.AddBusinessLicenceAsync(siteId, businessLicence, documentGuid);
            if (licence == null)
            {
                ModelState.AddModelError("documentGuid", "Business Licence could not be created; network error or upload is already submitted");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            return Ok(ApiResponse.Result(licence));
        }

        // PUT: api/sites/5/business-licence
        /// <summary>
        /// Updates an existing Business Licence.
        /// </summary>
        /// <param name="businessLicence"></param>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/business-licence", Name = nameof(UpdateBusinessLicence))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResultResponse<BusinessLicence>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BusinessLicence>> UpdateBusinessLicence(int siteId, BusinessLicence businessLicence)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (site.BusinessLicence.BusinessLicenceDocument != null)
            {
                return Conflict(ApiResponse.Message($"Business licence already uploaded, update not allowed."));
            }

            var licence = await _siteService.UpdateBusinessLicenceAsync(site.Id, businessLicence);

            return Ok(ApiResponse.Result(licence));
        }

        // POST: api/sites/5/business-licence/document
        /// <summary>
        /// Creates a new Business Licence Document.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/business-licence/document", Name = nameof(CreateBusinessLicenceDocument))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResultResponse<BusinessLicenceDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BusinessLicenceDocument>> CreateBusinessLicenceDocument(int siteId, [FromQuery] Guid documentGuid)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (site.BusinessLicence == null)
            {
                return NotFound(ApiResponse.Message($"Business Licence not found on site with id {siteId}"));
            }
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (site.BusinessLicence.BusinessLicenceDocument != null && site.SubmittedDate != null)
            {
                return Conflict(ApiResponse.Message($"Business Licence Document exists for submitted site with id {siteId}"));
            }

            var document = await _siteService.AddOrReplaceBusinessLicenceDocumentAsync(site.BusinessLicence.Id, documentGuid);
            if (document == null)
            {
                ModelState.AddModelError("documentGuid", "Business Licence Document could not be created; network error or upload is already submitted");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            await _emailService.SendSiteRegistrationSubmissionAsync(siteId);

            // Send an notifying email to the adjudicator
            // if the site is calimed by a adjudicator, is a community pharmacy,
            // and previsouly deferred the business licence document.
            if (site.Adjudicator != null
                && site.CareSetting.Code == (int)CareSettingType.CommunityPharmacy
                && !string.IsNullOrEmpty(site.BusinessLicence.DeferredLicenceReason))
            {
                await _emailService.SendBusinessLicenceUploadedAsync(site);
            }

            return Ok(ApiResponse.Result(document));
        }

        // DELETE: api/sites/5/business-licence/document
        /// <summary>
        /// Deletes a sites business Licence Document.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpDelete("{siteId}/business-licence/document", Name = nameof(RemoveBusinessLicenceDocument))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveBusinessLicenceDocument(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (site.BusinessLicence == null)
            {
                return NotFound(ApiResponse.Message($"Business Licence not found on site with id {siteId}"));
            }
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (site.SubmittedDate != null)
            {
                return Conflict(ApiResponse.Message($"Unable to remove document once site has been submitted"));
            }

            await _siteService.DeleteBusinessLicenceDocumentAsync(siteId);
            return Ok();
        }

        // Get: api/sites/5/business-licence
        /// <summary>
        /// Gets a new Business Licence for a site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/business-licence", Name = nameof(CreateBusinessLicence))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BusinessLicence>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BusinessLicence>> GetBusinessLicence(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var licence = await _siteService.GetBusinessLicenceAsync(site.Id);

            return Ok(ApiResponse.Result(licence));
        }

        // POST: api/sites/5/adjudication-documents
        /// <summary>
        /// Creates a new site adjudication document for a site.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/adjudication-documents", Name = nameof(CreateSiteAdjudicationDocument))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<SiteAdjudicationDocument>> CreateSiteAdjudicationDocument(int siteId, [FromQuery] Guid documentGuid)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var document = await _siteService.AddSiteAdjudicationDocumentAsync(site.Id, documentGuid, admin.Id);
            if (document == null)
            {
                ModelState.AddModelError("documentGuid", "Site Adjudication Document could not be created; network error or upload is already submitted");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            return Ok(ApiResponse.Result(document));
        }

        // GET: api/sites/5/adjudication-documents
        /// <summary>
        /// Gets all site adjudication documents for a site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/adjudication-documents", Name = nameof(GetSiteAdjudicationDocuments))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SiteAdjudicationDocument>>> GetSiteAdjudicationDocuments(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var documents = await _siteService.GetSiteAdjudicationDocumentsAsync(site.Id);

            return Ok(ApiResponse.Result(documents));
        }

        // GET: api/Sites/{siteId}/adjudication-documents/{documentId}
        /// <summary>
        /// Get the site adjudication documents download token.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="documentId"></param>
        [HttpGet("{siteId}/adjudication-documents/{documentId}", Name = nameof(GetSiteAdjudicationDocument))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetSiteAdjudicationDocument(int siteId, int documentId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var token = await _documentService.GetDownloadTokenForSiteAdjudicationDocument(documentId);

            return Ok(ApiResponse.Result(token));
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
                ModelState.AddModelError("Site.PEC", "PEC Code was not provided");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var site = await _siteService.GetSiteNoTrackingAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var updatedSite = await _siteService.UpdatePecCode(siteId, pecCode);

            return Ok(ApiResponse.Result(updatedSite));
        }

        // Get: api/site/5/latest-business-licence
        /// <summary>
        /// Gets a download token for the latest business licence on a site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/business-licence/document/token", Name = nameof(GetBusinessLicenceDocumentToken))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetBusinessLicenceDocumentToken(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (site.BusinessLicence?.BusinessLicenceDocument == null)
            {
                return NotFound(ApiResponse.Message($"No business licence document found for site with id {siteId}"));
            }

            var token = await _documentService.GetDownloadTokenForBusinessLicenceDocument(siteId);

            return Ok(ApiResponse.Result(token));
        }

        // POST: api/Sites/5/remote-users-email
        /// <summary>
        /// Send HIBC an email when remote users are updated for a submitted site
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/remote-users-email", Name = nameof(SendRemoteUsersEmail))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendRemoteUsersEmail(int siteId)
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
        [HttpPost("{siteId}/remote-users-email-admin", Name = nameof(SendRemoteUsersEmailAdmin))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendRemoteUsersEmailAdmin(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
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
        [HttpPost("{siteId}/remote-users-email-user", Name = nameof(SendRemoteUsersEmailUser))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendRemoteUsersEmailUser(int siteId, IEnumerable<RemoteUser> remoteUsers)
        {
            var site = await _siteService.GetSiteAsync(siteId);

            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            if (!site.Provisioner.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _emailService.SendRemoteUserNotificationsAsync(site, remoteUsers);
            return NoContent();
        }

        // PUT: api/Sites/5/approve
        /// <summary>
        /// Approve a site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/approve", Name = nameof(ApproveSite))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Site>> ApproveSite(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var updatedSite = await _siteService.ApproveSite(siteId);
            await _emailService.SendSiteApprovedPharmaNetAdministratorAsync(site);
            await _emailService.SendSiteApprovedSigningAuthorityAsync(site);
            await _emailService.SendSiteApprovedHIBCAsync(site);

            return Ok(ApiResponse.Result(updatedSite));
        }

        // PUT: api/Sites/5/decline
        /// <summary>
        /// Decline a site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/decline", Name = nameof(DeclineSite))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Site>> DeclineSite(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var updatedSite = await _siteService.DeclineSite(siteId);
            return Ok(ApiResponse.Result(updatedSite));
        }

        // POST: api/Sites/5/site-registration-notes
        /// <summary>
        /// Creates a new site registration note on a site.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="note"></param>
        [HttpPost("{siteId}/site-registration-notes", Name = nameof(CreateSiteRegistrationNote))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteRegistrationNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult<SiteRegistrationNote>> CreateSiteRegistrationNote(int siteId, FromBodyText note)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            if (string.IsNullOrWhiteSpace(note))
            {
                ModelState.AddModelError("note", "site registration notes can't be null or empty.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var createdSiteRegistrationNote = await _siteService.CreateSiteRegistrationNoteAsync(siteId, note, admin.Id);

            return Ok(ApiResponse.Result(createdSiteRegistrationNote));
        }

        // GET: api/Sites/5/site-registration-notes
        /// <summary>
        /// Gets all of the site registration notes for a specific site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/site-registration-notes", Name = nameof(GetSiteRegistrationNotes))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Status>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SiteRegistrationNote>>> GetSiteRegistrationNotes(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var siteRegistrationNotes = await _siteService.GetSiteRegistrationNotesAsync(site);

            return Ok(ApiResponse.Result(siteRegistrationNotes));
        }

        // POST: api/Sites/remote-users
        /// <summary>
        /// Searches for Remote User Certifications by College Code + Licence Number and returns related Site data
        /// </summary>
        /// <param name="certifications"></param>
        [HttpPost("remote-users", Name = nameof(GetSitesByRemoteUserInfo))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<RemoteAccessSearchViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RemoteAccessSearchViewModel>>> GetSitesByRemoteUserInfo(IEnumerable<CertSearchViewModel> certifications)
        {
            var info = await _siteService.GetRemoteUserInfoAsync(certifications);
            return Ok(ApiResponse.Result(info));
        }

        // GET: api/Sites/5/events?businessEventTypeCodes=1&businessEventTypeCodes=2
        /// <summary>
        /// Gets all of the site registration notes for a specific site.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="businessEventTypeCodes"></param>
        [HttpGet("{siteId}/events", Name = nameof(GetSiteBusinessEvents))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BusinessEvent>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BusinessEvent>>> GetSiteBusinessEvents(int siteId, [FromQuery] IEnumerable<int> businessEventTypeCodes)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var events = await _siteService.GetSiteBusinessEventsAsync(siteId, businessEventTypeCodes);

            return Ok(ApiResponse.Result(events));
        }

        // DELETE: api/Sites/{siteId}/adjudication-documents/{documentId}
        /// <summary>
        /// Delete the site's adjudication document
        /// </summary>
        /// <param name="documentId"></param>
        [HttpDelete("{siteId}/adjudication-documents/{documentId}", Name = nameof(DeleteSiteAdjudicationDocument))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<EnrolleeViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<SiteAdjudicationDocument>> DeleteSiteAdjudicationDocument(int documentId)
        {
            var document = await _siteService.GetSiteAdjudicationDocumentAsync(documentId);
            if (document == null)
            {
                return NotFound(ApiResponse.Message($"Document not found with id {documentId}"));
            }

            await _siteService.DeleteSiteAdjudicationDocumentAsync(documentId);

            return Ok(ApiResponse.Result(document));
        }

        // POST: api/sites/5/site-registration-notes/6/notification
        /// <summary>
        /// Creates a new site notification on a site registration note.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="siteRegistrationNoteId"></param>
        /// <param name="assigneeId"></param>
        [HttpPost("{siteId}/site-registration-notes/{siteRegistrationNoteId}/notification", Name = nameof(CreateSiteNotification))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteNotification>), StatusCodes.Status200OK)]
        public async Task<ActionResult<SiteNotification>> CreateSiteNotification(int siteId, int siteRegistrationNoteId, FromBodyData<int> assigneeId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            var note = await _siteService.GetSiteRegistrationNoteAsync(siteId, siteRegistrationNoteId);
            if (note == null)
            {
                return NotFound(ApiResponse.Message($"Site Registration Note not found with id {siteRegistrationNoteId}"));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var notification = await _siteService.CreateSiteNotificationAsync(note.Id, admin.Id, assigneeId);

            return Ok(ApiResponse.Result(notification));
        }

        // DELETE: api/Enrollees/5/site-registration-notes/6/notification
        /// <summary>
        /// deletes the notification on an site registration note.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="siteRegistrationNoteId"></param>
        [HttpDelete("{siteId}/site-registration-notes/{siteRegistrationNoteId}/notification", Name = nameof(DeleteSiteNotification))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteSiteNotification(int siteId, int siteRegistrationNoteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }
            var note = await _siteService.GetSiteRegistrationNoteAsync(siteId, siteRegistrationNoteId);
            if (note == null || note.SiteNotification == null)
            {
                return NotFound(ApiResponse.Message($"Site Registration Note with notification not found with id {siteRegistrationNoteId}"));
            }

            await _siteService.RemoveSiteNotificationAsync(note.SiteNotification.Id);

            return Ok();
        }

        // Get: api/sites/5/notifications
        /// <summary>
        /// Get the site registration notes on an enrollee that has a notification for current admin user.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/notifications", Name = nameof(GetSiteNotifications))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteRegistrationNoteViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<SiteRegistrationNoteViewModel>> GetSiteNotifications(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var notes = await _siteService.GetNotificationsAsync(siteId, admin.Id);

            return Ok(ApiResponse.Result(notes));
        }

        // Delete: api/sites/5/notifications
        /// <summary>
        /// Delete all notifications on a site
        /// </summary>
        /// <param name="siteId"></param>
        [HttpDelete("{siteId}/notifications", Name = nameof(DeleteSiteNotifications))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<EnrolleeNoteViewModel>> DeleteSiteNotifications(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound(ApiResponse.Message($"Site not found with id {siteId}"));
            }

            await _siteService.RemoveNotificationsAsync(siteId);

            return Ok();
        }
    }
}
