using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Services
{
    public class SiteClaimService : BaseService, ISiteClaimService
    {
        private readonly IBusinessEventService _businessEventService;

        private readonly ICommunitySiteService _communitySiteService;

        public SiteClaimService(
            ApiDbContext context,
            ILogger<OrganizationClaimService> logger,
            IBusinessEventService businessEventService,
            ICommunitySiteService communitySiteService)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _communitySiteService = communitySiteService;
        }


        public async Task<SiteClaim> GetSiteClaimBySiteIdAsync(int siteId)
        {
            return await _context.SiteClaims
                .SingleOrDefaultAsync(sc => sc.SiteId == siteId);
        }

        public async Task<int> CreateCommunitySiteClaimAsync(SiteClaimViewModel siteClaimVm, CommunitySite communitySite,
        int newOrganizationId, int newSigningAuthorityId)
        {
            var siteClaim = new SiteClaim
            {
                SiteId = communitySite.Id,
                NewOrganizationId = newOrganizationId,
                NewSigningAuthorityId = newSigningAuthorityId,
                ProvidedSiteId = siteClaimVm.PEC,
                Details = siteClaimVm.ClaimDetail
            };

            _context.SiteClaims.Add(siteClaim);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(communitySite.Id, "Community Site Claim Created");

            return communitySite.Id;
        }

        public async Task<SiteClaim> GetSiteClaimAsync(int claimId)
        {
            return await _context.SiteClaims
                .Include(sc => sc.NewSigningAuthority)
                .SingleOrDefaultAsync(sc => sc.Id == claimId);
        }

        public async Task UpdateSiteOrganizationAsync(SiteClaim siteClaim)
        {
            await _communitySiteService.UpdateOrganizationAsync(siteClaim.SiteId, siteClaim.NewOrganizationId);
        }

        public async Task DeleteClaimAsync(int claimId)
        {
            var claim = await _context.SiteClaims
                .SingleOrDefaultAsync(sc => sc.Id == claimId);
            if (claim == null)
            {
                return;
            }

            _context.SiteClaims.Remove(claim);

            await _context.SaveChangesAsync();
        }
    }
}

