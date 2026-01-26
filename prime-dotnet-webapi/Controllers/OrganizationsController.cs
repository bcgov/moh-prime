using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class OrganizationsController : PrimeControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IBusinessEventService _businessEventService;
        private readonly ICommunitySiteService _communitySiteService;
        private readonly IDocumentService _documentService;
        private readonly IEmailService _emailService;
        private readonly IOrganizationAgreementService _organizationAgreementService;
        private readonly IOrganizationClaimService _organizationClaimService;
        private readonly IOrganizationService _organizationService;
        private readonly ISiteService _siteService;
        private readonly IPartyService _partyService;
        private readonly IDocumentManagerClient _documentManagerClient;


        public OrganizationsController(
            IAdminService adminService,
            IBusinessEventService businessEventService,
            ICommunitySiteService communitySiteService,
            IDocumentService documentService,
            IEmailService emailService,
            IOrganizationAgreementService organizationAgreementService,
            IOrganizationClaimService organizationClaimService,
            IOrganizationService organizationService,
            ISiteService siteService,
            IPartyService partyService,
            IDocumentManagerClient documentManagerClient)
        {
            _adminService = adminService;
            _businessEventService = businessEventService;
            _communitySiteService = communitySiteService;
            _documentService = documentService;
            _emailService = emailService;
            _organizationAgreementService = organizationAgreementService;
            _organizationClaimService = organizationClaimService;
            _organizationService = organizationService;
            _siteService = siteService;
            _partyService = partyService;
            _documentManagerClient = documentManagerClient;
        }

        // GET: api/Organizations/5
        /// <summary>
        /// Gets a specific Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}", Name = nameof(GetOrganizationById))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationById(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound();
            }

            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(organization);
        }

        // GET: api/Organizations/name/string
        /// <summary>
        /// Gets a specific Organization.
        /// </summary>
        /// <param name="organizationName"></param>
        [HttpGet("name/{organizationName}", Name = nameof(GetOrganizationByName))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<List<Organization>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationByName(string organizationName)
        {
            var organizations = await _organizationService.GetOrganizationByNameAsync(organizationName);

            return Ok(organizations);
        }


        // GET: api/organization/sites/siteid/abc
        /// <summary>
        /// Return site predecessor
        /// </summary>
        /// <param name="siteId"></param>
        [HttpGet("sites/siteid/{siteId}", Name = nameof(GetOrganizationSiteBySiteID))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationSiteBySiteID(string siteId)
        {
            var organizations = await _organizationService.GetOrganizationSiteBySiteIdAsync(siteId);
            return organizations == null || organizations.Length == 0
                ? NotFound($"Organization Site not found with site id: {siteId}")
                : Ok(organizations);
        }

        // GET: api/organization/sites/sitename/sitename
        /// <summary>
        /// Return site predecessor
        /// </summary>
        /// <param name="sitename"></param>
        [HttpGet("sites/sitename/{sitename}", Name = nameof(GetOrganizationSiteBySiteName))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationSiteBySiteName(string sitename)
        {
            var organizations = await _organizationService.GetOrganizationSiteBySiteNameAsync(sitename);
            return (organizations == null || organizations.Length == 0)
                ? NotFound($"Organization Site not found with site name: {sitename}")
                : Ok(organizations);
        }

        // POST: api/Organizations
        /// <summary>
        /// Creates a new Organization.
        /// </summary>
        [HttpPost(Name = nameof(CreateOrganization))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateOrganization(CreateOrganizationViewModel createOrganization)
        {
            if (!await _partyService.PartyExistsAsync(createOrganization.PartyId, PartyType.SigningAuthority))
            {
                return BadRequest("Could not create an organization, the passed in SigningAuthority does not exist.");
            }

            var createdOrganizationId = await _organizationService.CreateOrganizationAsync(createOrganization.PartyId);
            var createdOrganization = await _organizationService.GetOrganizationAsync(createdOrganizationId);

            return CreatedAtAction(
                nameof(GetOrganizationById),
                new { organizationId = createdOrganizationId },
                createdOrganization
            );
        }

        // POST: api/Organizations/claim
        /// <summary>
        /// Claim an existing Organization.
        /// </summary>
        [HttpPost("claims", Name = nameof(ClaimOrganization))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ClaimOrganization(OrganizationClaimViewModel organizationClaim)
        {
            var party = await _partyService.GetPartyAsync(organizationClaim.PartyId, PartyType.SigningAuthority);
            if (party == null)
            {
                return BadRequest("Could not claim an organization, the passed in SigningAuthority does not exist.");
            }

            if (party.Username != User.GetPrimeUsername())
            {
                return BadRequest("Could not claim an organization, the passed in party does not match current user.");
            }

            var organization = await _organizationService.GetOrganizationByPecAsync(organizationClaim.PEC);
            if (organization == null)
            {
                return BadRequest("Could not claim an organization, the passed in PEC did not locate an organization.");
            }

            var orgClaim = await _organizationClaimService.GetOrganizationClaimByOrgIdAsync(organization.Id);
            if (orgClaim != null)
            {
                return BadRequest("Could not claim an organization which has already been claimed.");
            }

            var organizationId = await _organizationClaimService.CreateOrganizationClaimAsync(organizationClaim, organization);

            return Ok(organizationId);
        }

        // GET: api/Organizations/claims
        /// <summary>
        /// Check if organization claim exists by a given search criteria.
        /// </summary>
        [HttpGet("claims", Name = nameof(OrganizationClaimExists))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<OrganizationClaim>), StatusCodes.Status200OK)]
        public async Task<ActionResult> OrganizationClaimExists([FromQuery] OrganizationClaimSearchOptions search)
        {
            var result = await _organizationClaimService.OrganizationClaimExistsAsync(search);

            return Ok(result);
        }

        // GET: api/Organizations/5/claims
        /// <summary>
        /// Find OrganizationClaim by Organization ID.
        /// </summary>
        [HttpGet("{organizationId}/claims", Name = nameof(GetOrganizationClaimByOrgId))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<OrganizationClaim>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationClaimByOrgId(int organizationId)
        {
            var claim = await _organizationClaimService.GetOrganizationClaimByOrgIdAsync(organizationId);
            if (claim == null)
            {
                return NotFound("No claim by a SigningAuthority exists for given Organization.");
            }
            return Ok(claim);
        }

        // POST: api/Organizations/5/claims/1/approve
        /// <summary>
        /// Approve claim for an existing Organization.
        /// </summary>
        [HttpPost("{organizationId}/claims/{claimId}/approve", Name = nameof(ApproveOrganizationClaim))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ApproveOrganizationClaim(int organizationId, int claimId)
        {
            var orgClaim = await _organizationClaimService.GetOrganizationClaimAsync(claimId);
            if (orgClaim == null || organizationId != orgClaim.OrganizationId)
            {
                return NotFound("Cannot locate Claim for given Organization.");
            }

            var existingSigningAuthorityId = await _organizationService.GetOrganizationSigningAuthorityIdAsync(organizationId);
            var notificationRequired = existingSigningAuthorityId != orgClaim.NewSigningAuthorityId;

            await _organizationService.SwitchSigningAuthorityAsync(organizationId, orgClaim.NewSigningAuthorityId);
            await _communitySiteService.UpdateSigningAuthorityForOrganization(organizationId, orgClaim.NewSigningAuthorityId);
            await _organizationService.RemoveUnsignedOrganizationAgreementsAsync(organizationId);
            await _organizationService.FlagPendingTransferIfOrganizationAgreementsRequireSignaturesAsync(organizationId);

            await _organizationClaimService.DeleteClaimAsync(orgClaim.Id);

            if (notificationRequired)
            {
                var organizations = await _organizationService.GetOrganizationsByPartyIdAsync(existingSigningAuthorityId);
                if (organizations.Count() == 0)
                {
                    //if no organization belong to existing signing authority, remove Party Enrolment record
                    await _partyService.RemovePartyEnrolmentAsync(existingSigningAuthorityId, PartyType.SigningAuthority);
                }
                await _businessEventService.CreateOrganizationEventAsync(organizationId, orgClaim.NewSigningAuthorityId, $"Organization Claim (Site ID/PEC provided: {orgClaim.ProvidedSiteId}, Reason: {orgClaim.Details}) approved.");
                await _emailService.SendOrgClaimApprovalNotificationAsync(orgClaim);
                await _businessEventService.CreateOrganizationEventAsync(organizationId, orgClaim.NewSigningAuthorityId, "Sent organization claim approval notification");
            }

            return NoContent();
        }

        // PUT: api/Organizations/5
        /// <summary>
        /// Updates a specific Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="updatedOrganization"></param>
        [HttpPut("{organizationId}", Name = nameof(UpdateOrganization))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateOrganization(int organizationId, OrganizationUpdateModel updatedOrganization)
        {
            if (!await _organizationService.OrganizationExistsAsync(organizationId))
            {
                return NotFound($"Organization not found with id {organizationId}");
            }

            // TODO: fix
            // return Forbid();

            await _organizationService.UpdateOrganizationAsync(organizationId, updatedOrganization);

            return NoContent();
        }

        // PUT: api/Organizations/5/completed
        /// <summary>
        /// Updates an organizations state
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpPut("{organizationId}/completed", Name = nameof(UpdateOrganizationCompleted))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateOrganizationCompleted(int organizationId)
        {
            if (!await _organizationService.OrganizationExistsAsync(organizationId))
            {
                return NotFound($"Organization not found with id {organizationId}");
            }

            // TODO: fix
            // return Forbid();

            await _organizationService.UpdateCompletedAsync(organizationId);

            return NoContent();
        }

        // DELETE: api/Organizations/5
        /// <summary>
        /// Deletes a specific Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpDelete("{organizationId}", Name = nameof(DeleteOrganization))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteOrganization(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _organizationService.DeleteOrganizationAsync(organizationId);

            return Ok(organization);
        }

        // PUT: api/Organizations/5/archive
        /// <summary>
        /// Archive a specific Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpPut("{organizationId}/archive", Name = nameof(ArchiveOrganization))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ArchiveOrganization(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _organizationService.ArchiveOrganizationAsync(organizationId);

            return Ok(organization);
        }

        // PUT: api/Organizations/5/restore
        /// <summary>
        /// Restore a archived Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpPut("{organizationId}/restore", Name = nameof(RestoreArchivedOrganization))]
        [Authorize(Roles = Roles.PrimeSuperAdmin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status200OK)]
        public async Task<ActionResult> RestoreArchivedOrganization(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _organizationService.RestoreArchivedOrganizationAsync(organizationId);

            return Ok(organization);
        }

        // GET: api/Organizations/5/agreements
        // TODO: security?
        /// <summary>
        /// Gets all agreements for a specific Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}/agreements", Name = nameof(GetOrganizationAgreements))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AgreementViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationAgreements(int organizationId)
        {
            var agreements = await _organizationAgreementService.GetOrgAgreementsAsync(organizationId);

            return Ok(agreements);
        }

        // POST: api/Organizations/5/agreements/care-settings/2
        /// <summary>
        /// Creates a new un-accepted Organization Agreement based on the Care Setting supplied, if a newer version exits
        /// or if the signing authority has changed.
        /// Will return a reference to any existing un-accepted agreement instead of creating a new one, if able.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="careSettingCode"></param>
        [HttpPost("{organizationId}/agreements/care-settings/{careSettingCode}", Name = nameof(UpdateOrganizationAgreement))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Agreement>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateOrganizationAgreement(int organizationId, int careSettingCode)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var agreement = await _organizationService.EnsureUpdatedOrgAgreementAsync(organizationId, careSettingCode, organization.SigningAuthorityId);
            if (agreement == null)
            {
                return NotFound($"Care Setting Code {careSettingCode} not found on any site on Organization {organizationId}");
            }

            if (agreement.AcceptedDate.HasValue)
            {
                return NoContent();
            }
            else
            {
                return CreatedAtAction(
                    nameof(GetOrganizationAgreement),
                    new { organizationId, agreementId = agreement.Id },
                    agreement
                );
            }
        }

        // GET: api/Organizations/5/care-settings/pending-transfer
        /// <summary>
        /// Get the care setting codes for an organization that require agreements
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}/care-settings/pending-transfer", Name = nameof(GetCareSettingForPendingTransferAgreements))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<CareSettingType>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetCareSettingForPendingTransferAgreements(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var careSettingCodes = await _organizationService.GetCareSettingCodesForPendingTransferAsync(organizationId, organization.SigningAuthorityId);

            return Ok(careSettingCodes);
        }

        // GET: api/Organizations/5/agreements/7
        // TODO: load text from DB, security
        /// <summary>
        /// Get the organization agreement text.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <param name="asPdf"></param>
        [HttpGet("{organizationId}/agreements/{agreementId}", Name = nameof(GetOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Agreement>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationAgreement(int organizationId, int agreementId, [FromQuery] bool asPdf)
        {
            if (!await _organizationService.OrganizationExistsAsync(organizationId))
            {
                return NotFound($"Organization not found with id {organizationId}");
            }

            var agreement = await _organizationAgreementService.GetOrgAgreementAsync(organizationId, agreementId, asPdf);
            if (agreement == null)
            {
                return NotFound($"Agreement with ID {agreementId} not found on Organization {organizationId}");
            }

            return Ok(agreement);
        }

        // GET: api/Organizations/5/agreements/7/signable
        // TODO: security
        /// <summary>
        /// Get the organization agreement as a signable PDF, Base 64 encoded.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementType"></param>
        [HttpGet("{organizationId}/signable", Name = nameof(GetSignableOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSignableOrganizationAgreement(int organizationId, [FromQuery] AgreementType agreementType)
        {
            if (!await _organizationService.OrganizationExistsAsync(organizationId))
            {
                return NotFound($"Organization not found with id {organizationId}");
            }

            if (!agreementType.IsOrganizationAgreement())
            {
                return BadRequest($"Agreement with type {agreementType} not allowed");
            }

            var pdf = await _organizationAgreementService.GetSignableOrgAgreementAsync(organizationId, agreementType);

            return Ok(pdf);
        }

        // PUT: api/Organizations/5/agreements/7
        /// <summary>
        /// Accept an organization agreement, optionally with a Document GUID of the wet-signed agreement upload
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <param name="organizationAgreementGuid"></param>
        [HttpPut("{organizationId}/agreements/{agreementId}", Name = nameof(AcceptOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AcceptOrganizationAgreement(int organizationId, int agreementId, [FromQuery] Guid? organizationAgreementGuid)
        {
            var organization = await _organizationService.GetOrganizationNoTrackingAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            if (organizationAgreementGuid.HasValue)
            {
                var signedAgreement = await _organizationService.AddSignedAgreementAsync(organizationId, agreementId, organizationAgreementGuid.Value);
                if (signedAgreement == null)
                {
                    return BadRequest("Signed Organization Agreement could not be created; network error or upload is already submitted");
                }
            }

            await _organizationService.AcceptOrgAgreementAsync(organizationId, agreementId);

            if (organization.PendingTransfer && await _organizationService.IsOrganizationTransferCompleteAsync(organizationId))
            {
                await _organizationService.FinalizeTransferAsync(organizationId);
            }


            if (!organizationAgreementGuid.HasValue)
            {
                var filename = "Organization-Agreement.pdf";
                // get the agreement
                var agreement = await _organizationAgreementService.GetOrgAgreementAsync(organizationId, agreementId, true);
                // store it into document manager
                var documentGuid = await _documentManagerClient.SendFileAsync(new System.IO.MemoryStream(Convert.FromBase64String(agreement.AgreementContent)), filename, DestinationFolders.SignedOrgAgreements);
                // add a record in signed organization agreement table, treat it as uploaded agreement
                var signedAgreement = await _organizationService.AddSignedAgreementAsync(organizationId, agreementId, documentGuid, filename);
                if (signedAgreement == null)
                {
                    return BadRequest("Signed Organization Agreement could not be created; network error or upload is already submitted");
                }
            }

            return NoContent();
        }

        // Get: api/organizations/5/agreements/7/signed
        /// <summary>
        /// Gets a download token for the uploaded wet-signed Agreement Document (if exists).
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        [HttpGet("{organizationId}/agreements/{agreementId}/signed", Name = nameof(GetSignedAgreementToken))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSignedAgreementToken(int organizationId, int agreementId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {organizationId}");
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (!organization.Agreements.Any(a => a.Id == agreementId))
            {
                return NotFound($"Agreement with ID {agreementId} not found on Organization {organizationId}");
            }

            var token = await _documentService.GetDownloadTokenForSignedAgreementDocument(agreementId);

            return Ok(token);
        }

        // GET: api/Organizations/admin-view
        /// <summary>
        /// Gets a specific Organization.
        /// </summary>
        /// <param name="searchText"></param>
        [HttpGet("admin-view", Name = nameof(GetOrganizationAdminView))]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<OrganizationAdminListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationAdminView(string searchText)
        {
            var organizations = await _organizationService.GetOrganizationAdminListViewAsync(searchText);

            return Ok(organizations);
        }

        // GET: api/Organizations/admin-view/1
        /// <summary>
        /// Gets a specific Organization.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("admin-view/{id}", Name = nameof(GetOrganizationAdminViewById))]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<OrganizationAdminListViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrganizationAdminViewById(int id)
        {
            var organization = await _organizationService.GetOrganizationAdminListViewByIdAsync(id);

            return Ok(organization);
        }



        // POST: api/Organizations/1/editable
        /// <summary>
        /// Sets the Organization details to editable.
        /// </summary>
        /// <param name="id"></param>
        [HttpPost("{id}/editable", Name = nameof(SetOrganizationDetailEditable))]
        [Authorize(Roles = Roles.PrimeAdministrant)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SetOrganizationDetailEditable(int id)
        {

            var organization = await _organizationService.GetOrganizationAsync(id);
            if (organization == null)
            {
                return NotFound($"Organization not found with id {id}");
            }

            await _organizationService.SetOrganizationDetailEditable(id);

            return NoContent();
        }
    }
}
