using System;
using System.Security.Claims;

namespace Prime
{
    public static class PrimeUtils
    {
        public static Guid PrimeUserId(ClaimsPrincipal User)
        {
            string userId = User?.Identity?.Name;
            return userId != null ? new Guid(userId) : Guid.Empty;
        }
    }
}