using System;
using System.Security.Claims;

namespace Prime.Models
{
    /// <summary>
    /// A DTO used to determine if a UserBound Model a) exists and b) can be accessed by the current user.
    /// </summary>
    public class PermissionsRecord
    {
        public Guid UserId { get; set; }

        public bool EditableBy(ClaimsPrincipal user)
        {
            return user.IsAdmin() || MatchesUserIdOf(user);
        }

        public bool ViewableBy(ClaimsPrincipal user)
        {
            // TODO: Tighten restrictions
            return user.HasViewEnrollee() || user.HasViewSite() || MatchesUserIdOf(user);
        }

        public bool MatchesUserIdOf(ClaimsPrincipal user)
        {
            return user.GetPrimeUserId().Equals(UserId);
        }
    }
}
