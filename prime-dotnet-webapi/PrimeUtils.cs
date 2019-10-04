using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Prime
{
    public static class PrimeUtils
    {
        public static string PrimeUserId(ClaimsPrincipal User)
        {
            return User.Claims.FirstOrDefault(x => x.Type == PrimeConstants.PRIME_USER_ID_KEY)?.Value;
        }

    }
}