using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;

namespace Prime.Services
{
    public class OrganizationClaimService : BaseService, IOrganizationClaimService
    {
        private readonly IBusinessEventService _businessEventService;

        public OrganizationClaimService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IBusinessEventService businessEventService)
            : base(context, httpContext)
        {
            _businessEventService = businessEventService;
        }

        public async Task<bool> HasClaimAsync(int organizationId)
        {
            return await _context.OrganizationClaims
                .AsNoTracking()
                .AnyAsync(oc => oc.OrganizationId == organizationId);
        }

        public async Task<bool> DeleteClaimAsync(int claimId)
        {
            var claim = await _context.OrganizationClaims
                .SingleOrDefaultAsync(oc => oc.Id == claimId);
            if (claim == null)
            {
                return false;
            }

            _context.OrganizationClaims.Remove(claim);
            int numAffected = await _context.SaveChangesAsync();
            return (numAffected == 1);
        }

        public Task<OrganizationClaim> GetOrganizationClaimAsync(int organizationId)
        {
            return _context.OrganizationClaims
                .SingleOrDefaultAsync(oc => oc.OrganizationId == organizationId);
        }
        public async Task<Organization> CreateOrganizationClaimAsync(OrganizationClaimViewModel organizationClaim, Organization organization)
        {
            var organizationCLaim = new OrganizationClaim
            {
                OrganizationId = organization.Id,
                NewSigningAuthorityId = organizationClaim.PartyId,
                ProvidedSiteId = organizationClaim.PEC,
                Details = organizationClaim.ClaimDetail
            };

            _context.OrganizationClaims.Add(organizationCLaim);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateOrganizationEventAsync(organization.Id, organizationClaim.PartyId, "Organization Claim Created");

            return organization;
        }

        public async Task<bool> OrganizationClaimExistsAsync(OrganizationClaimSearchOptions searchOptions)
        {
            // return false if searchOptions is invalid
            if (searchOptions == null || string.IsNullOrEmpty(searchOptions.Pec) && searchOptions.UserId == Guid.Empty)
            {
                return false;
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

