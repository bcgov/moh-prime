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
        private readonly IPartyService _partyService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IDocumentService _documentService;

        public OrganizationsController(
            IMapper mapper,
            IOrganizationService organizationService,
            IPartyService partyService,
            IDocumentService documentService,
            IRazorConverterService razorConverterService)
        {
            _mapper = mapper;
            _organizationService = organizationService;
            _partyService = partyService;
            _razorConverterService = razorConverterService;
            _documentService = documentService;
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

            if (!User.CanEdit(organization.SigningAuthority))
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

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

            if (!User.CanEdit(party))
            {
                return Forbid();
            }

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

            var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

            if (!User.CanEdit(party))
            {
                return Forbid();
            }

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

            if (!User.CanEdit(organization.SigningAuthority))
            {
                return Forbid();
            }

            await _organizationService.DeleteOrganizationAsync(organizationId);

            return Ok(ApiResponse.Result(organization));
        }

        // GET: api/Organizations/organization-agreement
        /// <summary>
        /// Get the organization agreement.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}/organization-agreement", Name = nameof(GetOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetOrganizationAgreement(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            var agreement = await _razorConverterService.RenderViewToStringAsync("/Views/OrganizationAgreement.cshtml", organization);

            return Ok(ApiResponse.Result(agreement));
        }

        // PUT: api/Organizations/5/organization-agreement
        /// <summary>
        /// Accept an organization agreement
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpPut("{organizationId}/organization-agreement", Name = nameof(AcceptCurrentOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AcceptCurrentOrganizationAgreement(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationNoTrackingAsync(organizationId);

            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            if (!User.CanEdit(organization.SigningAuthority))
            {
                return Forbid();
            }

            await _organizationService.AcceptCurrentOrganizationAgreementAsync(organization.Id);

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

            if (!User.CanEdit(organization.SigningAuthority))
            {
                return Forbid();
            }

            organization = await _organizationService.SubmitRegistrationAsync(organizationId);
            return Ok(ApiResponse.Result(organization));
        }

        // POST: api/organizations/5/signed-agreement
        /// <summary>
        /// Adds a new signed agreement to an organization.
        /// </summary>
        /// <param name="documentGuid"></param>
        /// <param name="filename"></param>
        /// <param name="organizationId"></param>
        [HttpPost("{organizationId}/signed-agreement", Name = nameof(CreateSignedAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<SignedAgreementDocument>), StatusCodes.Status201Created)]
        public async Task<ActionResult<SignedAgreementDocument>> CreateSignedAgreement(int organizationId, [FromQuery] Guid documentGuid, [FromQuery] string filename)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            if (!User.CanEdit(organization.SigningAuthority))
            {
                return Forbid();
            }

            var agreement = await _organizationService.AddSignedAgreementAsync(organization.Id, documentGuid, filename);

            return CreatedAtAction(
                nameof(GetSignedAgreement),
                new { organizationId = organization.Id },
                ApiResponse.Result(agreement)
            );
        }

        // Get: api/organizations/5/signed-agreement
        /// <summary>
        /// Gets the signed agreement for a organization.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}/signed-agreement", Name = nameof(GetSignedAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<SignedAgreementDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SignedAgreementDocument>>> GetSignedAgreement(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            if (!User.CanEdit(organization.SigningAuthority))
            {
                return Forbid();
            }

            var agreements = await _organizationService.GetSignedAgreementsAsync(organization.Id);

            return Ok(ApiResponse.Result(agreements));
        }

        // Get: api/organizations/5/latest-signed-agreement
        /// <summary>
        /// Gets the latest signed agreement by organization download token.
        /// </summary>
        /// <param name="organizationId"></param>
        [HttpGet("{organizationId}/latest-signed-agreement", Name = nameof(GetLatestSignedAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<SignedAgreementDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SignedAgreementDocument>>> GetLatestSignedAgreement(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (organization == null)
            {
                return NotFound(ApiResponse.Message($"Organization not found with id {organizationId}"));
            }

            if (!User.CanEdit(organization.SigningAuthority))
            {
                return Forbid();
            }

            var token = await _documentService.GetDownloadTokenForLatestSignedAgreementDocument(organizationId);

            return Ok(ApiResponse.Result(token));
        }

        // GET: api/Organizations/organization-agreement-document
        /// <summary>
        /// Get the organization agreement document.
        /// </summary>
        [HttpGet("organization-agreement-document", Name = nameof(OrganizationAgreementDocument))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public ActionResult<string> OrganizationAgreementDocument()
        {
            var fileName = "Organization-Agreement.pdf";
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(fileName));

            string base64;
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (var reader = new MemoryStream())
            {
                stream.CopyTo(reader);
                base64 = Convert.ToBase64String(reader.ToArray());
            }

            return Ok(ApiResponse.Result(base64));
        }
    }
}
