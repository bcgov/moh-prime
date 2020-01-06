using System;
using System.Linq;
using System.Security.Claims;

using Prime.Models;

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

        /// <summary>
        /// Returns true if the logged in user is an admin, or if the user has the same UserId as the record
        /// </summary>
        public static bool CanAccess(this ClaimsPrincipal User, Enrollee enrollee)
        {
            if (User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE))
            {
                return true;
            }

            Guid PrimeUserId = User.GetPrimeUserId();
            return !PrimeUserId.Equals(Guid.Empty) && PrimeUserId.Equals(enrollee.UserId);
        }

        public static bool HasAssuranceLevel(this ClaimsPrincipal User, int level)
        {
            Claim assuranceLevelClaim = User?.Claims?.SingleOrDefault(c => c.Type == PrimeConstants.ASSURANCE_LEVEL_CLAIM_TYPE);

            int assuranceLevel;
            return Int32.TryParse(assuranceLevelClaim?.Value, out assuranceLevel) && assuranceLevel == level;
        }
    }
}
