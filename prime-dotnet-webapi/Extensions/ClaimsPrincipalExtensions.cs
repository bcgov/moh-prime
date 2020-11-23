using System;
using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;

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
            return User.IsInRole(Roles.PrimeAdmin);
        }

        public static bool HasAdminView(this ClaimsPrincipal User)
        {
            return User.IsInRole(Roles.PrimeReadonlyAdmin);
        }

        public static int GetIdentityAssuranceLevel(this ClaimsPrincipal User)
        {
            Claim assuranceLevelClaim = User?.Claims?.SingleOrDefault(c => c.Type == Claims.AssuranceLevel);

            Int32.TryParse(assuranceLevelClaim?.Value, out int assuranceLevel);

            return assuranceLevel;
        }

        public static string GetIdentityProvider(this ClaimsPrincipal User)
        {
            return User.GetStringClaim(Claims.IdentityProvider);
        }

        public static bool HasVCIssuance(this ClaimsPrincipal User)
        {
            return User.IsInRole(FeatureFlags.VCIssuance);
        }

        public static string GetStringClaim(this ClaimsPrincipal User, string claimType)
        {
            return User?.FindFirstValue(claimType);
        }

        public static PhysicalAddress GetPhysicalAddress(this ClaimsPrincipal User)
        {
            Claim addressClaim = User?.Claims?.SingleOrDefault(c => c.Type == Claims.Address);

            var address = JsonConvert.DeserializeObject<TokenAddress>(addressClaim?.Value);

            return address?.ToModel();
        }

        private class TokenAddress
        {
            public string Street_address { get; set; }
            public string Locality { get; set; }
            public string Region { get; set; }
            public string Postal_code { get; set; }
            public string Country { get; set; }

            public PhysicalAddress ToModel()
            {
                return new PhysicalAddress
                {
                    CountryCode = Country,
                    ProvinceCode = Region,
                    Street = Street_address,
                    City = Locality,
                    Postal = Postal_code,
                };
            }
        }
    }
}
