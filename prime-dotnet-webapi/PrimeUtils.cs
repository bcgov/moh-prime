using System;
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

        // TODO - remove this temp method which add an admin role to a User
        public static void AddAdminRoleToUser(ClaimsPrincipal User)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, PrimeConstants.PRIME_ADMIN_ROLE));
            User.AddIdentity(claimsIdentity);
        }
    }
}