using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
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
    public class OrganizationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationService _organizationService;

        private readonly IAgreementService _agreementService;
        private readonly IPartyService _partyService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IDocumentService _documentService;

        private readonly IPdfService _pdfService;

        public OrganizationsController(
            IMapper mapper,
            IOrganizationService organizationService,
            IAgreementService agreementService,
            IPartyService partyService,
            IDocumentService documentService,
            IRazorConverterService razorConverterService,
            IPdfService pdfService)
        {
            _mapper = mapper;
            _organizationService = organizationService;
            _agreementService = agreementService;
            _partyService = partyService;
            _razorConverterService = razorConverterService;
            _documentService = documentService;
            _pdfService = pdfService;
        }

        // GET: api/Organizations
        /// <summary>
        /// Gets all of the Organizations for a user, or all organizations if user has ADMIN role
        /// </summary>
        /// <param name="verbose"></param>
        [HttpGet(Name = nameof(GetOrganizations))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Organization>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations([FromQuery] bool verbose)
        {
            IEnumerable<Organization> organizations = null;

            if (User.HasAdminView())
            {
                organizations = await _organizationService.GetOrganizationsAsync();
            }
            else
            {
                var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

                organizations = (party != null)
                    ? await _organizationService.GetOrganizationsAsync(party.Id)
                    : Enumerable.Empty<Organization>();
            }

            if (verbose)
            {
                return Ok(ApiResponse.Result(organizations));
            }
            else
            {
                return Ok(ApiResponse.Result(_mapper.Map<IEnumerable<OrganizationListViewModel>>(organizations)));
            }
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

            if (!organization.SigningAuthority.PermissionsRecord().EditableBy(User))
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
        public async Task<ActionResult<Organization>> CreateOrganization(Party party)
        {
            if (party == null)
            {
                this.ModelState.AddModelError("Party", "Could not create an organization, the passed in Party cannot be null.");
                return BadRequest(ApiResponse.BadRequest(this.ModelState));
            }

            var createdOrganizationId = await _organizationService.CreateOrganizationAsync(party);

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
            if (!organization.SigningAuthority.PermissionsRecord().EditableBy(User))
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
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Agreement>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Agreement>>> GetOrganizationAgreements(int organizationId)
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
            if (!organization.SigningAuthority.PermissionsRecord().EditableBy(User))
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
                    new { organizationId = organizationId, agreementId = agreement.Id },
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

        // PUT: api/Organizations/5/agreements/7
        /// <summary>
        /// Accept an organization agreement
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="agreementId"></param>
        /// <param name="documentGuid"></param>
        [HttpPut("{organizationId}/agreements/{agreementId}", Name = nameof(AcceptOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptOrganizationAgreement(int organizationId, int agreementId, [FromQuery] Guid? documentGuid)
        {
            var organization = await _organizationService.GetOrganizationNoTrackingAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }
            if (!organization.SigningAuthority.PermissionsRecord().EditableBy(User))
            {
                return Forbid();
            }

            if (documentGuid.HasValue)
            {
                var signedAgreement = await _organizationService.AddSignedAgreementAsync(organizationId, agreementId, documentGuid.Value);
                if (signedAgreement == null)
                {
                    this.ModelState.AddModelError("documentGuid", "Signed Organization Agreement could not be created; network error or upload is already submitted");
                    return BadRequest(ApiResponse.BadRequest(this.ModelState));
                }
            }

            await _organizationService.AcceptOrgAgreementAsync(organizationId, agreementId);

            return NoContent();
        }

        // POST: api/organizations/5/submission
        /// <summary>
        /// Submits the given organization for adjudication.
        /// </summary>
        [HttpPost("{organizationId}/submission", Name = nameof(SubmitOrganizationRegistration))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<Organization>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Organization>> SubmitOrganizationRegistration(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }
            if (!organization.SigningAuthority.PermissionsRecord().EditableBy(User))
            {
                return Forbid();
            }

            organization = await _organizationService.SubmitRegistrationAsync(organizationId);
            return Ok(ApiResponse.Result(organization));
        }

        // Get: api/organizations/5/latest-signed-agreement
        /// <summary>
        /// Gets a download token for the latest signed agreement on an organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}/latest-signed-agreement", Name = nameof(GetLatestSignedAgreement))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetLatestSignedAgreement(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }
            if (!organization.SigningAuthority.PermissionsRecord().EditableBy(User))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForLatestSignedAgreementDocument(organizationId);

            return Ok(ApiResponse.Result(token));
        }

        // GET: api/Organizations/organization-agreement-digital-signed
        /// <summary>
        /// Get the digitally signed organization agreement.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}/organization-agreement-digital-signed", Name = nameof(GetSignedDigitalOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetSignedDigitalOrganizationAgreement(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);
            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            var html = await _razorConverterService.RenderViewToStringAsync("/Views/OrganizationAgreementPdf.cshtml", organization);
            var agreement = _pdfService.Generate(html);

            return Ok(ApiResponse.Result(agreement));
        }
    }
}
