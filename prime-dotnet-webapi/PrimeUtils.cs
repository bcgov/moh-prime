using System.Linq;
using System.Security.Claims;

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