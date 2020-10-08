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
            return User.IsInRole(AuthConstants.PRIME_ADMIN_ROLE);
        }

        public static bool HasAdminView(this ClaimsPrincipal User)
        {
            return User.IsInRole(AuthConstants.PRIME_READONLY_ADMIN);
        }

        public static int GetIdentityAssuranceLevel(this ClaimsPrincipal User)
        {
            Claim assuranceLevelClaim = User?.Claims?.SingleOrDefault(c => c.Type == AuthConstants.ASSURANCE_LEVEL_CLAIM_TYPE);

            int assuranceLevel = 0;
            Int32.TryParse(assuranceLevelClaim?.Value, out assuranceLevel);

            return assuranceLevel;
        }

        public static string GetIdentityProvider(this ClaimsPrincipal User)
        {
            return User.GetStringClaim(AuthConstants.IDENTITY_PROVIDER_CLAIM_TYPE);
        }

        public static bool hasVCIssuance(this ClaimsPrincipal User)
        {
            return User.IsInRole(AuthConstants.FEATURE_VC_ISSUANCE);
        }

        public static string GetStringClaim(this ClaimsPrincipal User, string claimType)
        {
            Claim claim = User?.Claims?.SingleOrDefault(c => c.Type == claimType);

            return claim?.Value;
        }

        public static PhysicalAddress GetPhysicalAddress(this ClaimsPrincipal User)
        {
            Claim addressClaim = User?.Claims?.SingleOrDefault(c => c.Type == "address");

            var address = JsonConvert.DeserializeObject<TokenAddress>(addressClaim?.Value);

            return address?.ToModel();
        }

        private class TokenAddress
        {
            public string street_address { get; set; }
            public string locality { get; set; }
            public string region { get; set; }
            public string postal_code { get; set; }
            public string country { get; set; }

            public PhysicalAddress ToModel()
            {
                return new PhysicalAddress
                {
                    CountryCode = country,
                    ProvinceCode = region,
                    Street = street_address,
                    City = locality,
                    Postal = postal_code,
                };
            }
        }
    }
}
