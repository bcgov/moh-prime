using System;
using System.Security.Claims;
using Newtonsoft.Json;

using Prime.Auth;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeCreateModel
    {
        public DateTime DateOfBirth { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string SmsPhone { get; set; }

        // Properties conditionally set by the backend based on Identity Provider
        [JsonIgnore]
        public string HPDID { get; set; }

        [JsonIgnore]
        public string FirstName { get; set; }

        [JsonIgnore]
        public string LastName { get; set; }

        [JsonIgnore]
        public string GivenNames { get; set; }

        [JsonIgnore]
        public PhysicalAddress PhysicalAddress { get; set; }

        // Properties always set by the backend from the JWT token (we cannot trust these properties from the frontend)
        [JsonIgnore]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public int IdentityAssuranceLevel { get; set; }

        [JsonIgnore]
        public string IdentityProvider { get; set; }

        public bool IsBcServicesCard()
        {
            return IdentityProvider == AuthConstants.BC_SERVICES_CARD;
        }

        public bool IsUnder18()
        {
            return DateOfBirth > DateTime.Today.AddYears(-18);
        }

        public void MapConditionalProperties(ClaimsPrincipal user)
        {
            UserId = user.GetPrimeUserId();
            IdentityProvider = user.GetIdentityProvider();
            IdentityAssuranceLevel = user.GetIdentityAssuranceLevel();

            if (IdentityProvider == AuthConstants.BC_SERVICES_CARD)
            {
                HPDID = user.GetStringClaim("preferred_username");
                FirstName = user.GetStringClaim("given_name");
                LastName = user.GetStringClaim("family_name");
                GivenNames = user.GetStringClaim("given_names");
                PhysicalAddress = user.GetPhysicalAddress();
            }
            else
            {
                FirstName = PreferredFirstName;
                LastName = PreferredLastName;
                GivenNames = $"{PreferredFirstName} {PreferredMiddleName}";
                PhysicalAddress = new PhysicalAddress
                {
                    CountryCode = MailingAddress.CountryCode,
                    ProvinceCode = MailingAddress.ProvinceCode,
                    Street = MailingAddress.Street,
                    Street2 = MailingAddress.Street2,
                    City = MailingAddress.City,
                    Postal = MailingAddress.Postal
                };
            }
        }
    }
}
