using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;

namespace Prime.Controllers
{

    [Produces("application/json")]
    [Route("api/sites")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class SiteClaimsController : PrimeControllerBase
    {

        private readonly IOrganizationService _organizationService;
        private readonly IPartyService _partyService;
        private readonly ICommunitySiteService _communitySiteService;

        private readonly ISiteClaimService _siteClaimService;

        public SiteClaimsController(
            IOrganizationService organizationService,
            IPartyService partyService,
            ICommunitySiteService communitySiteService,
            ISiteClaimService siteClaimService
        )
        {
            _organizationService = organizationService;
            _partyService = partyService;
            _communitySiteService = communitySiteService;
            _siteClaimService = siteClaimService;
        }


        // POST: api/sites/claims
        /// <summary>
        /// Claim an existing site.
        /// </summary>
        [HttpPost("claims", Name = nameof(ClaimSite))]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<int>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ClaimSite(SiteClaimViewModel siteClaim)
        {
            var party = await _partyService.GetPartyAsync(siteClaim.PartyId, PartyType.SigningAuthority);
            if (party == null)
            {
                return BadRequest("Could not claim a site, the passed in SigningAuthority does not exist.");
            }

            if (party.UserId != User.GetPrimeUserId())
            {
                return BadRequest("Could not claim a site, the passed in party does not match current user.");
            }

            // TODO: Verify user logged in has an organization: check party.id matches an organization.SigningAuthorityId,
            // return organization Id
            var currentOrganizationId = await _organizationService.GetOrganizationBySigningAuthority(party.Id);
            if (currentOrganizationId.HasValue)
            {
                return BadRequest("Could not claim a site, the passed in party does not match an organization signing authority.");
            }

            var communitySite = await _communitySiteService.GetCommunitySiteAsync(siteClaim.PEC);
            if (communitySite == null)
            {
                return BadRequest("Could not claim a site, the passed in PEC did not locate a community site.");
            }

            var siteClaimSearch = await _siteClaimService.GetSiteClaimBySiteIdAsync(communitySite.Id);
            if (siteClaimSearch != null)
            {
                return BadRequest("Could not claim a site which has already been claimed.");
            }

            await _siteClaimService.CreateCommunitySiteClaimAsync(siteClaim, communitySite, currentOrganizationId.GetValueOrDefault());

            return NoContent();
        }

        // GET: api/Sites/5/claims
        /// <summary>
        /// Find SiteClaim by Site ID.
        /// </summary>
        [HttpGet("{siteId}/claims", Name = nameof(GetSiteClaimBySiteId))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<OrganizationClaim>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetSiteClaimBySiteId(int siteId)
        {
            var claim = await _siteClaimService.GetSiteClaimBySiteIdAsync(siteId);
            if (claim == null)
            {
                return NotFound("No claim exists for given Site.");
            }
            return Ok(claim);
        }

        // POST: api/Organizations/5/claims/1/approve
        /// <summary>
        /// Approve claim for an existing Organization.
        /// </summary>
        [HttpPost("{siteId}/claims/{claimId}/approve", Name = nameof(ApproveSiteClaim))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> ApproveSiteClaim(int siteId, int claimId)
        {
            var siteClaim = await _siteClaimService.GetSiteClaimAsync(claimId);
            if (siteClaim == null || siteId != siteClaim.SiteId)
            {
                return NotFound("Cannot locate Claim for given Site.");
            }

            if (siteClaim.NewSigningAuthority.UserId != User.GetPrimeUserId())
            {
                return BadRequest("Could not claim a site, the passed in party does not match current user.");
            }

            await _siteClaimService.UpdateSiteOrganizationAsync(siteClaim);

            // TODO: remove existing unsigned OrgAgreements for OLD organization that reflect that site
            // await _organizationService.RemoveUnsignedOrganizationAgreementsAsync(organizationId);

            // TODO: call the following for the NEW organization
            // await _organizationService.FlagPendingTransferIfOrganizationAgreementsRequireSignaturesAsync(organizationId);

            await _siteClaimService.DeleteClaimAsync(siteClaim.Id);

            // TODO: handle notification
            // await _businessEventService.CreateSiteEventAsync(siteId, $"Site Claim (Site ID/PEC provided: {orgClaim.ProvidedSiteId}, Reason: {orgClaim.Details}) approved.");
            // await _emailService.SendOrgClaimApprovalNotificationAsync(orgClaim);

            return NoContent();
        }
    }
}
