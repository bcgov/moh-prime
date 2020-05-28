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
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IPartyService _partyService;
        private readonly IRazorConverterService _razorConverterService;

        public OrganizationsController(
            IOrganizationService organizationService,
            IPartyService partyService,
            IRazorConverterService razorConverterService)
        {
            _organizationService = organizationService;
            _partyService = partyService;
            _razorConverterService = razorConverterService;
        }

        // GET: api/Organizations
        /// <summary>
        /// Gets all of the Organizations for a user, or all organizations if user has ADMIN role
        /// </summary>
        [HttpGet(Name = nameof(GetOrganizations))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<Organization>>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
        {
            IEnumerable<Organization> organizations = null;

            if (User.IsAdmin() || User.HasAdminView())
            {
                organizations = await _organizationService.GetOrganizationsAsync();
            }
            else
            {
                var party = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId());

                organizations = (party != null)
                    ? await _organizationService.GetOrganizationsAsync(party.Id)
                    : new List<Organization>();
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
        /// <param name="isCompleted"></param>
        [HttpPut("{organizationId}", Name = nameof(UpdateOrganization))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrganization(int organizationId, Organization updatedOrganization, [FromQuery] bool isCompleted)
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

            await _organizationService.UpdateOrganizationAsync(organizationId, updatedOrganization, isCompleted);

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
        [HttpGet("organization-agreement", Name = nameof(GetOrganizationAgreement))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetOrganizationAgreement()
        {
            var agreement = await _razorConverterService.RenderViewToStringAsync("/Views/OrganizationAgreement.cshtml", new Organization());

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
    }
}
