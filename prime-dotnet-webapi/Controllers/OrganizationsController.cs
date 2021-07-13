using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public class OrganizationsController : PrimeControllerBase
    {
        private readonly IOrganizationService _organizationService;

        private readonly IOrganizationAgreementService _organizationAgreementService;
        private readonly IPartyService _partyService;
        private readonly IDocumentService _documentService;
        private readonly ISiteService _siteService;
        private readonly IOrganizationClaimService _organizationClaimService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IAdminService _adminService;
        private readonly IEmailService _emailService;


        public OrganizationsController(
            IOrganizationService organizationService,
            IOrganizationAgreementService organizationAgreementService,
            IPartyService partyService,
            IDocumentService documentService,
            ISiteService siteService,
            IOrganizationClaimService organizationClaimService,
            IBusinessEventService businessEventService,
            IAdminService adminService,
            IEmailService emailService)
        {
            _organizationService = organizationService;
            _organizationAgreementService = organizationAgreementService;
            _partyService = partyService;
            _documentService = documentService;
            _siteService = siteService;
            _organizationClaimService = organizationClaimService;
            _businessEventService = businessEventService;
            _adminService = adminService;
            _emailService = emailService;
        }

        // GET: api/Organizations
        /// <summary>
        /// Gets all of the Organizations.
        /// </summary>
        [HttpGet(Name = nameof(GetOrganizations))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<OrganizationSearchViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrganizationSearchViewModel>>> GetOrganizations([FromQuery] OrganizationSearchOptions search)
        {
            var organizations = await _organizationService.GetOrganizationsAsync(search);

            var notifiedIds = await _siteService.GetNotifiedSiteIdsForAdminAsync(User);
            foreach (var site in organizations.Select(o => o.Organization).SelectMany(o => o.Sites))
            {
                site.HasNotification = notifiedIds.Contains(site.Id);
            }

            return Ok(organizations);
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
        public async Task<ActionResult<Organization>> GetOrganizationById(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(organization);
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
        public async Task<ActionResult<Organization>> CreateOrganization(CreateOrganizationViewModel createOrganization)
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
        [HttpPost("claim", Name = nameof(ClaimOrganization))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ClaimOrganization(ClaimOrganizationViewModel claimOrganization)
        {
            var party = await _partyService.GetPartyAsync(claimOrganization.PartyId, PartyType.SigningAuthority);
            if (party == null)
            {
                return BadRequest("Could not claim an organization, the passed in SigningAuthority does not exist.");
            }

            if (party.UserId != HttpContext.User.GetPrimeUserId())
            {
                return BadRequest("Could not claim an organization, the passed in party does not match current user.");
            }

            var organization = await _organizationService.ClaimOrganizationAsync(claimOrganization.PartyId, claimOrganization.PEC, claimOrganization.ClaimDetail);

            return Ok(organization);
        }

        // GET: api/Organizations/claim
        /// <summary>
        /// Gets organization claim count by a given search criteria.
        /// </summary>
        [HttpGet("claims", Name = nameof(GetOrganizationClaimCount))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResultResponse<OrganizationClaim>), StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetOrganizationClaimCount([FromQuery] OrganizationClaimSearchOptions search)
        {
            // check if the organization exists with the given PEC
            if (!string.IsNullOrEmpty(search.Pec))
            {
                var organization = await _organizationService.GetOrganizationByPecAsync(search.Pec);
                if (organization == null)
                {
                    return BadRequest("Organization does not exist with the passed in site PEC.");
                }
            }
            var result = await _organizationService.GetOrganizationClaimCountAsync(search);

            return Ok(result);
        }

        // GET: api/Organizations/claim/5
        /// <summary>
        /// Find OrganizationClaim by Organization ID.
        /// </summary>
        [HttpGet("claim/{organizationId}", Name = nameof(GetOrganizationClaimByOrgId))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<OrganizationClaim>), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrganizationClaim>> GetOrganizationClaimByOrgId(int organizationId)
        {
            var claim = await _organizationClaimService.GetOrganizationClaimAsync(organizationId);
            if (claim == null)
            {
                return NotFound("No claim by a SigningAuthority exists for given Organization.");
            }
            else
            {
                return Ok(claim);
            }
        }

        // POST: api/Organizations/claim/approve
        /// <summary>
        /// Approve claim for an existing Organization.
        /// </summary>
        [HttpPost("claim/approve", Name = nameof(ApproveOrganizationClaim))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ApproveOrganizationClaim(OrganizationClaimApprovalViewModel claimApproval)
        {
            if (!await _partyService.PartyExistsAsync(claimApproval.PartyId, PartyType.SigningAuthority) ||
                !await _organizationService.OrganizationExistsAsync(claimApproval.OrganizationId))
            {
                return BadRequest("Could not approve claim for an organization, as the given SigningAuthority or Organization does not exist.");
            }
            var orgClaim = await _organizationClaimService.GetOrganizationClaimAsync(claimApproval.OrganizationId);
            if (orgClaim == null)
            {
                return NotFound("Cannot locate Claim for given Organization.");
            }
            var userId = HttpContext.User.GetPrimeUserId();
            Admin admin = await _adminService.GetAdminAsync(userId);

            if (!await _organizationService.SwitchSigningAuthorityAsync(claimApproval.OrganizationId, claimApproval.PartyId))
            {
                // TODO: Properly communicate error
                return BadRequest("Could not assign Organization to new SigningAuthority.");
            }

            await _emailService.SendOrgClaimApprovalNotificationAsync(orgClaim);

            await _businessEventService.CreateOrganizationEventAsync(claimApproval.OrganizationId, claimApproval.PartyId, $"Organization Claim (Site ID/PEC provided: {orgClaim.ProvidedSiteId}, Reason: {orgClaim.Details}) approved by {admin.LastName}, {admin.FirstName}");

            if (!await _organizationClaimService.DeleteClaimAsync(claimApproval.OrganizationId, claimApproval.PartyId))
            {
                return NotFound("Cannot remove Claim for given SigningAuthority and Organization.");
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
        public async Task<IActionResult> UpdateOrganization(int organizationId, OrganizationUpdateModel updatedOrganization)
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
        public async Task<IActionResult> UpdateOrganizationCompleted(int organizationId)
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
        public async Task<ActionResult<Organization>> DeleteOrganization(int organizationId)
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
        public async Task<ActionResult<IEnumerable<AgreementViewModel>>> GetOrganizationAgreements(int organizationId)
        {
            var agreements = await _organizationAgreementService.GetOrgAgreementsAsync(organizationId);

            return Ok(agreements);
        }

        // POST: api/Organizations/5/agreements/update
        /// <summary>
        /// Creates a new un-accepted Organization Agreement based on the type of Site supplied, if a newer version exits.
        /// Will return a reference to any existing un-accepted agreement instead of creating a new one, if able.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="siteId"></param>
        [HttpGet("{organizationId}/agreements/update", Name = nameof(UpdateOrganizationAgreement))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Agreement>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateOrganizationAgreement(int organizationId, [FromQuery] int siteId)
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

            var agreement = await _organizationService.EnsureUpdatedOrgAgreementAsync(organizationId, siteId);
            if (agreement == null)
            {
                return NotFound($"Site with ID {siteId} not found on Organization {organizationId}");
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
        public async Task<ActionResult<Agreement>> GetOrganizationAgreement(int organizationId, int agreementId, [FromQuery] bool asPdf)
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
        public async Task<ActionResult<string>> GetSignableOrganizationAgreement(int organizationId, [FromQuery] AgreementType agreementType)
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
        public async Task<IActionResult> AcceptOrganizationAgreement(int organizationId, int agreementId, [FromQuery] Guid? organizationAgreementGuid)
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
        public async Task<ActionResult<string>> GetSignedAgreementToken(int organizationId, int agreementId)
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
    }
}
