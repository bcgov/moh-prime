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
    public class OrganizationClaimService : BaseService, IOrganizationClaimService
    {
        private readonly IBusinessEventService _businessEventService;

        public OrganizationClaimService(
            ApiDbContext context,
            ILogger<OrganizationClaimService> logger,
            IBusinessEventService businessEventService)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
        }

        public async Task<bool> HasClaimAsync(int organizationId)
        {
            return await _context.OrganizationClaims
                .AsNoTracking()
                .AnyAsync(oc => oc.OrganizationId == organizationId);
        }

        public async Task DeleteClaimAsync(int claimId)
        {
            var claim = await _context.OrganizationClaims
                .SingleOrDefaultAsync(oc => oc.Id == claimId);
            if (claim == null)
            {
                return;
            }

            _context.OrganizationClaims.Remove(claim);
            await _context.SaveChangesAsync();
        }

        public async Task<OrganizationClaim> GetOrganizationClaimAsync(int claimId)
        {
            return await _context.OrganizationClaims
                .SingleOrDefaultAsync(oc => oc.Id == claimId);
        }

        public async Task<OrganizationClaim> GetOrganizationClaimByOrgIdAsync(int organizationId)
        {
            return await _context.OrganizationClaims
                .SingleOrDefaultAsync(oc => oc.OrganizationId == organizationId);
        }

        public async Task<int> CreateOrganizationClaimAsync(OrganizationClaimViewModel organizationClaimVm, Organization organization)
        {
            var organizationClaim = new OrganizationClaim
            {
                OrganizationId = organization.Id,
                NewSigningAuthorityId = organizationClaimVm.PartyId,
                ProvidedSiteId = organizationClaimVm.PEC,
                Details = organizationClaimVm.ClaimDetail
            };

            _context.OrganizationClaims.Add(organizationClaim);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateOrganizationEventAsync(organization.Id, organizationClaimVm.PartyId, "Organization Claim Created");

            return organization.Id;
        }

        public async Task<bool> OrganizationClaimExistsAsync(OrganizationClaimSearchOptions searchOptions)
        {
            // return false if searchOptions is invalid
            if (searchOptions == null || string.IsNullOrEmpty(searchOptions.Pec) && searchOptions.UserId == Guid.Empty)
            {
                return false;
            }

            // return false if orgnization does not exists,
            if (!string.IsNullOrEmpty(searchOptions.Pec))
            {
                var orgExists = await _context.Organizations.AnyAsync(o => o.Sites.Any(s => s.PEC == searchOptions.Pec));
                if (!orgExists)
                {
                    return false;
                }
            }

            return await _context.OrganizationClaims
                .AsNoTracking()
                .If(!string.IsNullOrEmpty(searchOptions.Pec), q => q
                    .Where(o => o.Organization.Sites.Any(s => s.PEC == searchOptions.Pec))
                )
                .If(searchOptions.UserId != Guid.Empty, q => q
                    .Where(o => o.NewSigningAuthority.UserId == searchOptions.UserId)
                )
                .AnyAsync();
        }
    }
}

