using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Prime.Services
{
    public class OrganizationClaimService : BaseService, IOrganizationClaimService
    {
        public OrganizationClaimService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext) { }

        public async Task<bool> IsOrganizationUnderReviewAsync(int organizationId)
        {
            return await _context.OrganizationClaims
                .AsNoTracking()
                .AnyAsync(oc => oc.OrganizationId == organizationId);
        }

        public async Task<bool> DeleteClaimAsync(int organizationId, int partyId)
        {
            var claim = await _context.OrganizationClaims
                .SingleOrDefaultAsync(oc => oc.OrganizationId == organizationId && oc.PartyId == partyId);
            if (claim == null)
            {
                return false;
            }

            _context.OrganizationClaims.Remove(claim);
            int numAffected = await _context.SaveChangesAsync();
            return (numAffected == 1);
        }
    }
}

