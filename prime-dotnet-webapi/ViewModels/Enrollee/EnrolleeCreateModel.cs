using System;
using System.Security.Claims;
using Newtonsoft.Json;

using Prime.Configuration.Auth;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeCreateModel
    {
        public Guid UserId { get; set; }

        public string HPDID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public VerifiedAddress VerifiedAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string SmsPhone { get; set; }

        // Properties always set by the backend from the JWT token (we cannot trust these properties from the frontend)
        [JsonIgnore]
        public int IdentityAssuranceLevel { get; set; }

        [JsonIgnore]
        public string IdentityProvider { get; set; }

        public bool IsBcServicesCard()
        {
            return IdentityProvider == AuthConstants.BCServicesCard;
        }

        public bool IsUnder18()
        {
            return DateOfBirth > DateTime.Today.AddYears(-18);
        }

        public void SetPropertiesFromToken(ClaimsPrincipal user)
        {
            IdentityProvider = user.FindFirstValue(Claims.IdentityProvider);
            IdentityAssuranceLevel = user.GetIdentityAssuranceLevel();
        }

        public bool Validate(ClaimsPrincipal user)
        {
            return UserId == user.GetPrimeUserId()
                && HPDID == user.FindFirstValue(Claims.PreferredUsername)
                && FirstName == user.FindFirstValue(Claims.GivenName)
                && LastName == user.FindFirstValue(Claims.FamilyName)
                && GivenNames == user.FindFirstValue(Claims.GivenNames)
                && DateOfBirth == user.GetDateOfBirth()
                && !IsUnder18()
                && Equals(VerifiedAddress, user.GetVerifiedAddress());
        }
    }
}
