using System;
using System.Security.Claims;
using System.Collections.Generic;
using Newtonsoft.Json;

using Prime.Infrastructure;
using Prime.Auth;
using Prime.Models;

namespace Prime.ViewModels
{
    public class EnrolleeUpdateModel
    {
        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public string Email { get; set; }

        public string SmsPhone { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        public ICollection<Job> Jobs { get; set; }

        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }

        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        [JsonIgnore]
        // This property is set by the backend from the JWT token; we cannot trust this property from the frontend
        public int IdentityAssuranceLevel { get; set; }

        [JsonIgnore]
        // This property is set by the backend from the JWT token; we cannot trust this property from the frontend
        public string IdentityProvider { get; set; }

        [JsonIgnore]
        public string FirstName { get; set; }

        [JsonIgnore]
        public string LastName { get; set; }

        [JsonIgnore]
        public string GivenNames { get; set; }

        [JsonIgnore]
        public PhysicalAddress PhysicalAddress { get; set; }

        public void MapConditionalProperties(ClaimsPrincipal user)
        {
            if (IdentityProvider != AuthConstants.BC_SERVICES_CARD)
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
