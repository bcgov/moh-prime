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
using Prime.ViewModels.Parties;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        private readonly IAgreementService _agreementService;
        private readonly IPartyService _partyService;
        private readonly IDocumentService _documentService;
        private readonly ISiteService _siteService;

        public OrganizationsController(
            IOrganizationService organizationService,
            IAgreementService agreementService,
            IPartyService partyService,
            IDocumentService documentService,
            ISiteService siteService)
        {
            _organizationService = organizationService;
            _agreementService = agreementService;
            _partyService = partyService;
            _documentService = documentService;
            _siteService = siteService;
        }

        // GET: api/Organizations
        /// <summary>
        /// Gets all of the Organizations for a user, or all organizations if user has ADMIN role
        /// </summary>
        [HttpGet(Name = nameof(GetOrganizations))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<OrganizationListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrganizationListViewModel>>> GetOrganizations()
        {
            IEnumerable<OrganizationListViewModel> organizations;

            if (User.IsAdministrant())
            {
                var notifiedIds = await _siteService.GetNotifiedSiteIdsForAdminAsync(User);
                organizations = await _organizationService.GetOrganizationsAsync();
                foreach (var organization in organizations)
                {
                    organization.Sites = organization.Sites.Select(s => s.SetNotification(notifiedIds.Contains(s.Id)));
                }
            }
            else
            {
                var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

                organizations = (party != null)
                    ? await _organizationService.GetOrganizationsByPartyIdAsync(party.Id)
                    : Enumerable.Empty<OrganizationListViewModel>();
            }

            return Ok(ApiResponse.Result(organizations));
        }

        // GET: api/Organizations/5
        /// <summary>
        /// Gets a specific Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}", Name = nameof(GetOrganizationById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Organization>> GetOrganizationById(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            return Ok(ApiResponse.Result(organization));
        }

        // POST: api/Organizations
        /// <summary>
        /// Creates a new Organization.
        /// </summary>
        [HttpPost(Name = nameof(CreateOrganization))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Organization>> CreateOrganization(SigningAuthorityChangeModel signingAuthority)
        {
            if (signingAuthority == null)
            {
                ModelState.AddModelError("SigningAuthority", "Could not create an organization, the passed in Signing Authority cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createdOrganizationId = await _organizationService.CreateOrganizationAsync(signingAuthority, User);

            var createdOrganization = await _organizationService.GetOrganizationAsync(createdOrganizationId);

            return CreatedAtAction(
                nameof(GetOrganizationById),
                new { organizationId = createdOrganizationId },
                ApiResponse.Result(createdOrganization)
            );
        }

        // PUT: api/Organizations/5
        /// <summary>
        /// Updates a specific Organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="updatedOrganization"></param>
        [HttpPut("{organizationId}", Name = nameof(UpdateOrganization))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrganization(int organizationId, OrganizationUpdateModel updatedOrganization)
        {
            var organization = await _organizationService.GetOrganizationNoTrackingAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
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
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrganizationCompleted(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationNoTrackingAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
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
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            await _organizationService.DeleteOrganizationAsync(organizationId);

            return Ok(ApiResponse.Result(organization));
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
            var agreements = await _agreementService.GetOrgAgreementsAsync(organizationId);

            return Ok(ApiResponse.Result(agreements));
        }

        // POST: api/Organizations/5/agreements/update
        /// <summary>
        /// Creates a new un-accepted Oganization Agreement based on the type of Site supplied, if a newer version exits.
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
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }

            var agreement = await _organizationService.EnsureUpdatedOrgAgreementAsync(organizationId, siteId);
            if (agreement == null)
            {
                return NotFound(ApiResponse.Message($"Site with ID {siteId} not found on Organization {organizationId}"));
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
                    ApiResponse.Result(agreement)
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
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Agreement>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Agreement>> GetOrganizationAgreement(int organizationId, int agreementId, [FromQuery] bool asPdf)
        {
            var organization = await _organizationService.GetOrganizationNoTrackingAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            var agreement = await _agreementService.GetOrgAgreementAsync(organizationId, agreementId, asPdf);
            if (agreement == null)
            {
                return NotFound(ApiResponse.Message($"Agreement with ID {agreementId} not found on Organization {organizationId}"));
            }

            return Ok(ApiResponse.Result(agreement));
        }

        // GET: api/Organizations/5/agreements/7/signable
        // TODO: security
        /// <summary>
        /// Get the organization agreement as a signable PDF, Base 64 encoded.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementType"></param>
        [HttpGet("{organizationId}/signable", Name = nameof(GetSignableOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetSignableOrganizationAgreement(int organizationId, [FromQuery] AgreementType agreementType)
        {
            var allowedAgreementTypes = new[] { AgreementType.CommunityPracticeOrgAgreement, AgreementType.CommunityPharmacyOrgAgreement };
            if (!allowedAgreementTypes.Contains(agreementType))
            {
                return BadRequest(ApiResponse.Message($"Agreement with type {agreementType} not allowed"));
            }

            var pdf = await _agreementService.GetSignableOrgAgreementAsync(organizationId, agreementType);
            if (pdf == null)
            {
                return NotFound(ApiResponse.Message($"Agreement for Organization {organizationId} not found"));
            }

            return Ok(ApiResponse.Result(pdf));
        }

        // PUT: api/Organizations/5/agreements/7
        /// <summary>
        /// Accept an organization agreement, optionally with a Document GUID of the wet-signed agreement upload
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <param name="organizationAgreementGuid"></param>
        [HttpPut("{organizationId}/agreements/{agreementId}", Name = nameof(AcceptOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptOrganizationAgreement(int organizationId, int agreementId, [FromQuery] Guid? organizationAgreementGuid)
        {
            var organization = await _organizationService.GetOrganizationNoTrackingAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
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
                    ModelState.AddModelError(nameof(organizationAgreementGuid), "Signed Organization Agreement could not be created; network error or upload is already submitted");
                    return BadRequest(ApiResponse.BadRequest(ModelState));
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
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }
            if (!organization.SigningAuthority.PermissionsRecord().AccessableBy(User))
            {
                return Forbid();
            }
            if (!organization.Agreements.Any(a => a.Id == agreementId))
            {
                return NotFound(ApiResponse.Message($"Agreement with ID {agreementId} not found on Organization {organizationId}"));
            }

            var token = await _documentService.GetDownloadTokenForSignedAgreementDocument(agreementId);

            return Ok(ApiResponse.Result(token));
        }
    }
}
