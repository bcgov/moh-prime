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
            return userId != null ? new Guid(userId) : Guid.Empty;
        }

        /// <summary>
        /// Returns true if the logged in user is an admin, or if the user has the same UserId as the record
        /// </summary>
        public static bool CanEdit(this ClaimsPrincipal User, IUserBoundModel party)
        {
            if (User.IsAdmin())
            {
                return true;
            }

            Guid PrimeUserId = User.GetPrimeUserId();
            return !PrimeUserId.Equals(Guid.Empty) && PrimeUserId.Equals(party.UserId);
        }

        /// <summary>
        /// Returns true if the logged in user is an RO_admin, admin, or if the user has the same UserId as the record
        /// </summary>
        public static bool CanView(this ClaimsPrincipal User, Enrollee enrollee)
        {
            return User.HasAdminView() || User.CanEdit(enrollee);
        }

        public static bool IsAdmin(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.Admin);
        }

        public static bool HasAdminView(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.ReadonlyAdmin);
        }

        public static string GetAudience(this ClaimsPrincipal User)
        {
            return User.FindFirstValue(Claims.Audience);
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
    }
}
