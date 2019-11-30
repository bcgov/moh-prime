using System;
using System.Linq;
using System.Security.Claims;

namespace Prime
{
    public static class PrimeUtils
    {
        /// <summary>
        /// Returns the Guid of the logged in user. If there is no logged in user, this will return Guid.Empty
        /// </summary>
        public static Guid PrimeUserId(ClaimsPrincipal User)
        {
            string userId = User?.Identity?.Name;
            return userId != null ? new Guid(userId) : Guid.Empty;
        }

        public static bool UserHasAssuranceLevel(ClaimsPrincipal User, int levelToCheck)
        {
            Claim assuranceLevelClaim = User?.Claims?.SingleOrDefault(c => c.Type == PrimeConstants.ASSURANCE_LEVEL_CLAIM_TYPE);
            int? assuranceLevel = ConvertStringToInt(assuranceLevelClaim?.Value);
            return levelToCheck.Equals(assuranceLevel);
        }

        public static int? ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : (int?)null);
        }
    }
}
