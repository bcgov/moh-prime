using System;
using System.Security.Claims;
using Newtonsoft.Json;

using Prime.Auth;
using Prime.Models;

namespace Prime.ViewModels.Labtech
{
    public class LabtechCreateModel
    {

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        // Properties set by the backend from the JWT
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public string HPDID { get; set; }

        [JsonIgnore]
        public string FirstName { get; set; }

        [JsonIgnore]
        public string LastName { get; set; }

        [JsonIgnore]
        public string GivenNames { get; set; }

        [JsonIgnore]
        public DateTime DateOfBirth { get; set; }

        [JsonIgnore]
        public PhysicalAddress PhysicalAddress { get; set; }

        [JsonIgnore]
        public int IdentityAssuranceLevel { get; set; }

        [JsonIgnore]
        public string IdentityProvider { get; set; }

        public void MapUserProperties(ClaimsPrincipal user)
        {
            UserId = user.GetPrimeUserId();
            HPDID = user.FindFirstValue(Claims.PreferredUsername);
            FirstName = user.FindFirstValue(Claims.GivenName);
            LastName = user.FindFirstValue(Claims.FamilyName);
            GivenNames = user.FindFirstValue(Claims.GivenNames);
            DateOfBirth = user.GetDateOfBirth().Value;
            PhysicalAddress = user.GetPhysicalAddress();
            IdentityAssuranceLevel = user.GetIdentityAssuranceLevel();
            IdentityProvider = user.FindFirstValue(Claims.IdentityProvider);
        }
    }
}
