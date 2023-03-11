using System;
using System.Security.Claims;

namespace Prime.Models
{
    /// <summary>
    /// A DTO used to determine if a UserBound Model a) exists and b) can be accessed by the current user.
    /// </summary>
    public class PermissionsRecord
    {
        public string Username { get; set; }

        public bool AccessableBy(ClaimsPrincipal user)
        {
            return user.IsAdministrant() || MatchesUserIdOf(user);
        }

        public bool MatchesUserIdOf(ClaimsPrincipal user)
        {
            return user.GetPrimeUserId().Equals(UserId);
        }
    }
}
