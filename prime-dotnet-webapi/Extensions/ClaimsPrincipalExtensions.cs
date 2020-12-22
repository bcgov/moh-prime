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
            string assuranceLevel = User?.FindFirstValue(Claims.AssuranceLevel);

            int.TryParse(assuranceLevel, out int assuranceLevelParsed);

            return assuranceLevelParsed;
        }

        public static bool HasVCIssuance(this ClaimsPrincipal User)
        {
            return User.IsInRole(FeatureFlags.VCIssuance);
        }

        public static PhysicalAddress GetPhysicalAddress(this ClaimsPrincipal User)
        {
            string addressClaim = User?.FindFirstValue(Claims.Address);
            if (addressClaim == null)
            {
                return null;
            }

            var address = JsonConvert.DeserializeObject<TokenAddress>(addressClaim);

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

        public static DateTime? GetDateOfBirth(this ClaimsPrincipal user)
        {
            var birthdate = user.FindFirstValue(Claims.Birthdate);

            if (DateTime.TryParse(birthdate, out DateTime parsed))
            {
                return parsed;
            }
            else
            {
                return null;
            }
        }
    }
}
