using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Services;
using Prime.ViewModels;

namespace Prime.Controllers
{

    [Produces("application/json")]
    [Route("api/Sites")]
    [ApiController]
    [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
    public class SiteClaimsController : PrimeControllerBase
    {

        private readonly IOrganizationService _organizationService;
        private readonly IPartyService _partyService;
        private readonly ICommunitySiteService _communitySiteService;
        private readonly ISiteClaimService _siteClaimService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;

        public SiteClaimsController(
            IOrganizationService organizationService,
            IPartyService partyService,
            ICommunitySiteService communitySiteService,
            ISiteClaimService siteClaimService,
            IBusinessEventService businessEventService,
            IEmailService emailService
        )
        {
            _organizationService = organizationService;
            _partyService = partyService;
            _communitySiteService = communitySiteService;
            _siteClaimService = siteClaimService;
            _businessEventService = businessEventService;
            _emailService = emailService;
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
            var signingAuthority = await _partyService.GetPartyForUserIdAsync(User.GetPrimeUserId(), PartyType.SigningAuthority);
            if (signingAuthority == null)
            {
                return NotFound($"Signing authority not found with id {User.GetPrimeUserId()}");
            }

            var organizations = await _organizationService.GetOrganizationsByPartyIdAsync(signingAuthority.Id);
            if (!organizations.Any())
            {
                return BadRequest("Could not claim a site, authenticated user does not match to an organization signing authority.");
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

            await _siteClaimService.CreateCommunitySiteClaimAsync(siteClaim, communitySite,
            organizations.First().Id, signingAuthority.Id);

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

        // POST: api/Sites/5/claims/1/approve
        /// <summary>
        /// Approve a site claim.
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

            var communitySite = await _communitySiteService.GetSiteAsync(siteId);
            var oldOrganizationId = communitySite.OrganizationId;

            await _siteClaimService.UpdateSiteOrganizationAsync(siteClaim);

            // Remove existing unsigned OrgAgreements for OLD organization that reflect that site
            // Check for agreements of type pharmacy if the site is of care setting pharmacy and remove unsigned agreements of that type if that is
            // the only site of that care setting (could search first for sites of that care setting first, then remove agreements of that type)
            // do this for both care setting "Community Pharmacy" -> AgreementType.CommunityPharmacyOrgAgreement
            // and care setting "Private Community Health Practice" -> AgreementType.CommunityPracticeOrgAgreement
            await _communitySiteService.RemoveUnsignedOrganizationAgreementsAsync(communitySite, oldOrganizationId);

            // Step 1: Check that the user has already signed relevant (matches claimed site) org agreement matching that care setting/agreement type,
            // has been signed, check that expiry date or accepted date is not null
            // Step 2: Where signing is required, flag pendingTransfer on org
            await _communitySiteService.FlagPendingTransferIfOrganizationAgreementsRequireSignaturesAsync(communitySite);

            await _siteClaimService.DeleteClaimAsync(siteClaim.Id);

            await _businessEventService.CreateSiteEventAsync(siteId, $"Site Claim (Site ID/PEC provided: {siteClaim.ProvidedSiteId}, Reason: {siteClaim.Details}) approved.");

            await _emailService.SendSiteClaimApprovalNotificationAsync(siteClaim);

            return NoContent();
        }
    }
}
