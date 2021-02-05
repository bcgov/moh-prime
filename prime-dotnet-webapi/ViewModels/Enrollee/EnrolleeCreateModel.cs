using System;
using System.Security.Claims;
using Newtonsoft.Json;

using Prime.Auth;
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

        public void MapUserClaims(ClaimsPrincipal user)
        {
            IdentityProvider = user.FindFirstValue(Claims.IdentityProvider);
            IdentityAssuranceLevel = user.GetIdentityAssuranceLevel();

            if (IdentityProvider != AuthConstants.BCServicesCard)
            {
                // TODO: Send this up from FE rather than mapping it
                GivenNames = FirstName;
            }
        }

        public bool Validate(ClaimsPrincipal user)
        {
            if (UserId != user.GetPrimeUserId()
                || FirstName != user.FindFirstValue(Claims.GivenName)
                || LastName != user.FindFirstValue(Claims.FamilyName)
                || IsUnder18())
            {
                return false;
            }

            if (IdentityProvider == AuthConstants.BCServicesCard)
            {
                return HPDID == user.FindFirstValue(Claims.PreferredUsername)
                    && GivenNames == user.FindFirstValue(Claims.GivenNames)
                    && DateOfBirth == user.GetDateOfBirth()
                    && VerifiedAddress == user.GetVerifiedAddress();
            }
            else
            {
                return VerifiedAddress == null
                    && Email == user.FindFirstValue(Claims.Email);
            }
        }
    }
}
