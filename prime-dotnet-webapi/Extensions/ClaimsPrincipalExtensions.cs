using System;
using System.Linq;
using System.Security.Claims;

using Prime.Models;
using Prime.Auth;

namespace Prime
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Returns the Guid of the logged in user. If there is no logged in user, this will return Guid.Empty
        /// </summary>
        public static Guid GetPrimeUserId(this ClaimsPrincipal User)
        {
            string userId = User?.Identity?.Name;
            return userId == null ? Guid.Empty : new Guid(userId);
        }

        public static bool IsAdmin(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.Admin);
        }

        public static bool HasAdminView(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.ReadonlyAdmin);
        }

        public static string GetAuthorizedParty(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(Claims.AuthorizedParty);
        }

        public static int GetIdentityAssuranceLevel(this ClaimsPrincipal User)
        {
            var claimValue = User.FindFirstValue(Claims.AssuranceLevel);

            int assuranceLevel = 0;
            Int32.TryParse(claimValue, out assuranceLevel);

            return assuranceLevel;
        }

        public static string GetIdentityProvider(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(Claims.IdentityProvider);
        }

        public static bool HasVCIssuance(this ClaimsPrincipal User)
        {
            return User.IsInRole(FeatureFlags.CredentialIssuance);
        }
    }
}
