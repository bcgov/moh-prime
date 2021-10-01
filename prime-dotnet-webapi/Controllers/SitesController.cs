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
using Prime.Engines;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class SitesController : PrimeControllerBase
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
        public async Task<ActionResult> GetSites(int organizationId, [FromQuery] bool verbose)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }

            var sites = await _siteService.GetSitesAsync(organizationId);

            if (verbose)
            {
                return Ok(sites);
            }
            else
            {
                return Ok(_mapper.Map<IEnumerable<SiteListViewModel>>(sites));
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
        public async Task<ActionResult> GetSiteById(int siteId)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteAsync(siteId);
            return Ok(site);
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
        public async Task<ActionResult> CreateSite(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            var createdSiteId = await _siteService.CreateSiteAsync(organizationId);

            var createdSite = await _siteService.GetSiteAsync(createdSiteId);

            return CreatedAtAction(
                nameof(GetSiteById),
                new { siteId = createdSiteId },
                createdSite
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
        public async Task<ActionResult> UpdateSite(int siteId, SiteUpdateModel updatedSite)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteNoTrackingAsync(siteId);

            // stop update if site is non health authority and PEC is not unique
            if (site.CareSettingCode != null
                && (CareSettingType)site.CareSettingCode != CareSettingType.HealthAuthority
                && !string.IsNullOrWhiteSpace(updatedSite.PEC) && site.PEC != updatedSite.PEC
                && await _siteService.PecExistsAsync(siteId, updatedSite.PEC))
            {
                return BadRequest("PEC already exists");
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
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
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
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
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
        public async Task<ActionResult> SetSiteAdjudicator(int siteId, [FromQuery] int? adjudicatorId)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }

            Admin admin = (adjudicatorId.HasValue)
                ? await _adminService.GetAdminAsync(adjudicatorId.Value)
                : await _adminService.GetAdminAsync(User.GetPrimeUserId());

            if (admin == null)
            {
                return NotFound($"Admin not found with id {adjudicatorId.Value}.");
            }

            var updatedSite = await _siteService.UpdateSiteAdjudicator(siteId, admin.Id);
            // TODO implement business events for sites
            // await _businessEventService.CreateAdminActionEventAsync(siteId, "Admin claimed site");

            return Ok(updatedSite);
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
        public async Task<ActionResult> RemoveSiteAdjudicator(int siteId)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var updatedSite = await _siteService.UpdateSiteAdjudicator(siteId);
            // TODO implement business events for sites
            // await _businessEventService.CreateAdminActionEventAsync(siteId, "Admin disclaimed site");

            return Ok(updatedSite);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteSite(int siteId)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            await _siteService.DeleteSiteAsync(siteId);

            return NoContent();
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
        public async Task<ActionResult> SubmitSiteRegistration(int siteId)
        {

            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteAsync(siteId);
            if (!SiteStatusStateEngine.AllowableStatusChange(SiteRegistrationAction.Submit, site.Status))
            {
                return BadRequest("Action could not be performed.");
            }
            site = await _siteService.SubmitRegistrationAsync(siteId);
            await _emailService.SendSiteRegistrationSubmissionAsync(siteId, site.BusinessLicence.Id, (CareSettingType)site.CareSettingCode);
            await _emailService.SendRemoteUserNotificationsAsync(site, site.RemoteUsers);

            return Ok(site);
        }

        // POST: api/sites/5/business-licences
        /// <summary>
        /// Creates a new Business Licence.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="businessLicence"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/business-licences", Name = nameof(CreateBusinessLicence))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResultResponse<BusinessLicence>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBusinessLicence(int siteId, BusinessLicence businessLicence, [FromQuery] Guid documentGuid)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var licence = await _siteService.AddBusinessLicenceAsync(siteId, businessLicence, documentGuid);
            if (licence == null)
            {
                return BadRequest("Business Licence could not be created; network error or upload is already submitted");
            }

            return Ok(licence);
        }

        // PUT: api/sites/5/business-licences/5
        /// <summary>
        /// Updates an existing Business Licence.
        /// </summary>
        /// <param name="businessLicence"></param>
        /// <param name="siteId"></param>
        /// <param name="businessLicenceId"></param>
        [HttpPut("{siteId}/business-licences/{businessLicenceId}", Name = nameof(UpdateBusinessLicence))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResultResponse<BusinessLicence>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BusinessLicence>> UpdateBusinessLicence(int siteId, int businessLicenceId, BusinessLicence businessLicence)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var licence = await _siteService.UpdateBusinessLicenceAsync(businessLicenceId, businessLicence);

            return Ok(licence);
        }

        // POST: api/sites/5/business-licences/5/document
        /// <summary>
        /// Creates a new Business Licence Document.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="siteId"></param>
        /// <param name="businessLicenceId"></param>
        [HttpPost("{siteId}/business-licences/{businessLicenceId}/document", Name = nameof(CreateBusinessLicenceDocument))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResultResponse<BusinessLicenceDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BusinessLicenceDocument>> CreateBusinessLicenceDocument(int siteId, int businessLicenceId, [FromQuery] Guid documentGuid)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteAsync(siteId);
            if (site.BusinessLicences == null)
            {
                return NotFound($"Business Licence not found on site with id {siteId}");
            }
            if (site.BusinessLicence.BusinessLicenceDocument != null && site.SubmittedDate != null)
            {
                return Conflict($"Business Licence Document exists for submitted site with id {siteId}");
            }

            var document = await _siteService.AddOrReplaceBusinessLicenceDocumentAsync(businessLicenceId, documentGuid);
            if (document == null)
            {
                return BadRequest("Business Licence Document could not be created; network error or upload is already submitted");
            }

            if (site.SubmittedDate != null)
            {
                await _emailService.SendSiteRegistrationSubmissionAsync(siteId, businessLicenceId, (CareSettingType)site.CareSettingCode);
            }

            // Send an notifying email to the adjudicator
            // if the site is claimed by a adjudicator, is a community pharmacy,
            // and previously deferred the business licence document.
            if (site.Adjudicator != null
                && site.CareSetting.Code == (int)CareSettingType.CommunityPharmacy
                && !string.IsNullOrEmpty(site.BusinessLicence.DeferredLicenceReason))
            {
                await _emailService.SendBusinessLicenceUploadedAsync(site);
            }

            return Ok(document);
        }

        // DELETE: api/sites/5/business-licences/5/document
        /// <summary>
        /// Deletes a sites business Licence Document.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="businessLicenceId"></param>
        [HttpDelete("{siteId}/business-licences/{businessLicenceId}/document", Name = nameof(RemoveBusinessLicenceDocument))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> RemoveBusinessLicenceDocument(int siteId, int businessLicenceId)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteAsync(siteId);
            if (site.BusinessLicence == null)
            {
                return NotFound($"Business Licence not found on site with id {siteId}");
            }
            if (site.SubmittedDate != null)
            {
                return Conflict($"Unable to remove document once site has been submitted");
            }

            await _siteService.DeleteBusinessLicenceDocumentAsync(businessLicenceId);
            return Ok();
        }

        // Get: api/sites/5/business-licences
        /// <summary>
        /// Gets all business Licences for a site or the latest business licence.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="latest"></param>
        [HttpGet("{siteId}/business-licences", Name = nameof(CreateBusinessLicence))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<BusinessLicence>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBusinessLicence(int siteId, [FromQuery] bool latest = false)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            return latest == true
                ? Ok(await _siteService.GetLatestBusinessLicenceAsync(siteId))
                : Ok(await _siteService.GetBusinessLicencesAsync(siteId));

        }

        // POST: api/sites/5/adjudication-documents
        /// <summary>
        /// Creates a new site adjudication document for a site.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/adjudication-documents", Name = nameof(CreateSiteAdjudicationDocument))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateSiteAdjudicationDocument(int siteId, [FromQuery] Guid documentGuid)
        {
            if (!await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var document = await _siteService.AddSiteAdjudicationDocumentAsync(siteId, documentGuid, admin.Id);
            if (document == null)
            {
                return BadRequest("Site Adjudication Document could not be created; network error or upload is already submitted");
            }

            return Ok(document);
        }

        // GET: api/sites/5/adjudication-documents
        /// <summary>
        /// Gets all site adjudication documents for a site.
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("{siteId}/adjudication-documents", Name = nameof(GetSiteAdjudicationDocuments))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteAdjudicationDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSiteAdjudicationDocuments(int siteId)
        {
            if (!await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var documents = await _siteService.GetSiteAdjudicationDocumentsAsync(siteId);

            return Ok(documents);
        }

        // GET: api/Sites/{siteId}/adjudication-documents/{documentId}
        /// <summary>
        /// Get the site adjudication documents download token.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="documentId"></param>
        [HttpGet("{siteId}/adjudication-documents/{documentId}", Name = nameof(GetSiteAdjudicationDocument))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSiteAdjudicationDocument(int siteId, int documentId)
        {
            if (!await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var token = await _documentService.GetDownloadTokenForSiteAdjudicationDocument(documentId);

            return Ok(token);
        }

        // PUT: api/Sites/5/pec
        /// <summary>
        /// Update the PEC code.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="pecCode"></param>
        [HttpPut("{siteId}/pec", Name = nameof(UpdatePecCode))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePecCode(int siteId, FromBodyText pecCode)
        {
            if (string.IsNullOrWhiteSpace(pecCode))
            {
                return BadRequest("PEC Code was not provided");
            }

            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteNoTrackingAsync(siteId);

            // stop update if site is non health authority and PEC is not unique
            if (site.CareSettingCode != null
                && (CareSettingType)site.CareSettingCode != CareSettingType.HealthAuthority
                && await _siteService.PecExistsAsync(siteId, pecCode))
            {
                return BadRequest("PEC already exists");
            }

            var updatedSite = await _siteService.UpdatePecCode(siteId, pecCode);

            return Ok(updatedSite);
        }

        // Get: api/site/5/business-licences/5/document/token
        /// <summary>
        /// Gets a download token for the latest business licence on a site.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="businessLicenceId"></param>
        [HttpGet("{siteId}/business-licences/{businessLicenceId}/document/token", Name = nameof(GetBusinessLicenceDocumentToken))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetBusinessLicenceDocumentToken(int siteId, int businessLicenceId)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteAsync(siteId);
            if (site.BusinessLicence?.BusinessLicenceDocument == null)
            {
                return NotFound($"No business licence document found for site with id {siteId}");
            }

            var token = await _documentService.GetDownloadTokenForBusinessLicenceDocument(siteId);

            return Ok(token);
        }

        // POST: api/Sites/5/remote-users-email
        /// <summary>
        /// Send HIBC an email when remote users are updated for a submitted site
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPost("{siteId}/remote-users-email", Name = nameof(SendRemoteUsersEmail))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendRemoteUsersEmail(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendRemoteUsersEmailAdmin(int siteId)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteAsync(siteId);
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
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendRemoteUsersEmailUser(int siteId, IEnumerable<RemoteUser> remoteUsers)
        {
            var record = await _siteService.GetPermissionsRecordAsync(siteId);
            if (record == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!record.AccessableBy(User))
            {
                return Forbid();
            }

            var site = await _siteService.GetSiteAsync(siteId);
            await _emailService.SendRemoteUserNotificationsAsync(site, remoteUsers);
            return NoContent();
        }

        // POST: api/Sites/5/site-reviewed-email
        /// <summary>
        /// Send site reviewed notification email to provider enrolment team
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost("{siteId}/site-reviewed-email", Name = nameof(SendSiteReviewedNotificationEmail))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendSiteReviewedNotificationEmail(int siteId, FromBodyText note)
        {
            if (!await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            await _emailService.SendSiteReviewedNotificationAsync(siteId, note);
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
        public async Task<ActionResult> ApproveSite(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!SiteStatusStateEngine.AllowableStatusChange(SiteRegistrationAction.Approve, site.Status))
            {
                return BadRequest("Action could not be performed.");
            }

            var updatedSite = await _siteService.ApproveSite(siteId);
            await _emailService.SendSiteApprovedPharmaNetAdministratorAsync(site);
            await _emailService.SendSiteApprovedSigningAuthorityAsync(site);
            await _emailService.SendSiteApprovedHIBCAsync(site);

            return Ok(updatedSite);
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
        public async Task<ActionResult> DeclineSite(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!SiteStatusStateEngine.AllowableStatusChange(SiteRegistrationAction.Reject, site.Status))
            {
                return BadRequest("Action could not be performed.");
            }

            var updatedSite = await _siteService.DeclineSite(siteId);
            return Ok(updatedSite);
        }

        // PUT: api/Sites/5/enable-editing
        /// <summary>
        /// Enable editing a site
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/enable-editing", Name = nameof(EnableEditingSite))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult> EnableEditingSite(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!SiteStatusStateEngine.AllowableStatusChange(SiteRegistrationAction.RequestChange, site.Status))
            {
                return BadRequest("Action could not be performed.");
            }

            var updatedSite = await _siteService.EnableEditingSite(siteId);
            return Ok(updatedSite);
        }

        // PUT: api/Sites/5/unreject
        /// <summary>
        /// Unreject a site
        /// </summary>
        /// <param name="siteId"></param>
        [HttpPut("{siteId}/unreject", Name = nameof(UnrejectSite))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Site>), StatusCodes.Status200OK)]
        public async Task<ActionResult> UnrejectSite(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (!SiteStatusStateEngine.AllowableStatusChange(SiteRegistrationAction.Unreject, site.Status))
            {
                return BadRequest("Action could not be performed.");
            }

            var updatedSite = await _siteService.UnrejectSite(siteId);
            return Ok(updatedSite);
        }

        // POST: api/Sites/5/site-registration-notes
        /// <summary>
        /// Creates a new site registration note on a site.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="note"></param>
        [HttpPost("{siteId}/site-registration-notes", Name = nameof(CreateSiteRegistrationNote))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SiteRegistrationNote>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateSiteRegistrationNote(int siteId, FromBodyText note)
        {
            if (string.IsNullOrWhiteSpace(note))
            {
                return BadRequest("site registration notes can't be null or empty.");
            }

            if (await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var createdSiteRegistrationNote = await _siteService.CreateSiteRegistrationNoteAsync(siteId, note, admin.Id);

            return Ok(createdSiteRegistrationNote);
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
        public async Task<ActionResult> GetSiteRegistrationNotes(int siteId)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var siteRegistrationNotes = await _siteService.GetSiteRegistrationNotesAsync(site);

            return Ok(siteRegistrationNotes);
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
        public async Task<ActionResult> GetSitesByRemoteUserInfo(IEnumerable<CertSearchViewModel> certifications)
        {
            var info = await _siteService.GetRemoteUserInfoAsync(certifications);
            return Ok(info);
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
        public async Task<ActionResult> GetSiteBusinessEvents(int siteId, [FromQuery] IEnumerable<int> businessEventTypeCodes)
        {
            if (await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var events = await _siteService.GetSiteBusinessEventsAsync(siteId, businessEventTypeCodes);

            return Ok(events);
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
        public async Task<ActionResult> DeleteSiteAdjudicationDocument(int documentId)
        {
            var document = await _siteService.GetSiteAdjudicationDocumentAsync(documentId);
            if (document == null)
            {
                return NotFound($"Document not found with id {documentId}");
            }

            await _siteService.DeleteSiteAdjudicationDocumentAsync(documentId);

            return Ok(document);
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
        public async Task<ActionResult> CreateSiteNotification(int siteId, int siteRegistrationNoteId, FromBodyData<int> assigneeId)
        {
            if (await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var note = await _siteService.GetSiteRegistrationNoteAsync(siteId, siteRegistrationNoteId);
            if (note == null)
            {
                return NotFound($"Site Registration Note not found with id {siteRegistrationNoteId}");
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());
            var notification = await _siteService.CreateSiteNotificationAsync(note.Id, admin.Id, assigneeId);

            return Ok(notification);
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
            if (await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var note = await _siteService.GetSiteRegistrationNoteAsync(siteId, siteRegistrationNoteId);
            if (note == null || note.SiteNotification == null)
            {
                return NotFound($"Site Registration Note with notification not found with id {siteRegistrationNoteId}");
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
        public async Task<ActionResult> GetSiteNotifications(int siteId)
        {
            if (await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            var admin = await _adminService.GetAdminAsync(User.GetPrimeUserId());

            var notes = await _siteService.GetNotificationsAsync(siteId, admin.Id);

            return Ok(notes);
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
        public async Task<ActionResult> DeleteSiteNotifications(int siteId)
        {
            if (await _siteService.SiteExists(siteId))
            {
                return NotFound($"Site not found with id {siteId}");
            }

            await _siteService.RemoveNotificationsAsync(siteId);

            return Ok();
        }

        // PUT: api/sites/5/flag
        /// <summary>
        /// Sets a site's flag, which serves as a reminder
        /// for an adjudicator to come back to this site
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="flagged"></param>
        [HttpPut("{siteId}/flag", Name = nameof(FlagSite))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> FlagSite(int siteId, FromBodyData<bool> flagged)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            await _siteService.UpdateSiteFlag(siteId, flagged);
            return Ok(site);
        }

        // GET: api/sites/1/pec-exists
        /// <summary>
        /// Check if a given PEC already exists, only applicable to non health authority site
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="pec"></param>
        /// <returns></returns>
        [HttpGet("{siteId}/pec-exists", Name = nameof(PecExists))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PecExists(int siteId, string pec)
        {
            var site = await _siteService.GetSiteAsync(siteId);
            if (site == null)
            {
                return NotFound($"Site not found with id {siteId}");
            }
            if (string.IsNullOrWhiteSpace(pec))
            {
                return BadRequest("PEC cannot be empty.");
            }

            var exist = await _siteService.PecExistsAsync(siteId, pec);
            return Ok(exist);
        }
    }
}
