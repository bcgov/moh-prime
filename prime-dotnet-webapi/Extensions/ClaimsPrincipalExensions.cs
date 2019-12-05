using System;
using System.Linq;
using System.Security.Claims;

namespace Prime
{
    public static class ClaimsPrincipalExensions
    {
        /// <summary>
        /// Returns the Guid of the logged in user. If there is no logged in user, this will return Guid.Empty
        /// </summary>
        public static Guid GetPrimeUserId(this ClaimsPrincipal User)
        {
            string userId = User?.Identity?.Name;
            return userId != null ? new Guid(userId) : Guid.Empty;
        }

        public static bool HasAssuranceLevel(this ClaimsPrincipal User, int level)
        {
            Claim assuranceLevelClaim = User?.Claims?.SingleOrDefault(c => c.Type == PrimeConstants.ASSURANCE_LEVEL_CLAIM_TYPE);

            int assuranceLevel;
            return Int32.TryParse(assuranceLevelClaim?.Value, out assuranceLevel) && assuranceLevel == level;
        }
    }
}
