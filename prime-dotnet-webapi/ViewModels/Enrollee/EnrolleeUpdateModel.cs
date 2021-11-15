using System.Security.Claims;
using System.Collections.Generic;
using Newtonsoft.Json;

using Prime.Configuration.Auth;
using Prime.Models;
using Prime.Infrastructure;

namespace Prime.ViewModels
{
    public class EnrolleeUpdateModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public VerifiedAddress VerifiedAddress { get; set; }

        public string Email { get; set; }

        public string SmsPhone { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public ICollection<Certification> Certifications { get; set; }

        public ICollection<EnrolleeRemoteUser> EnrolleeRemoteUsers { get; set; }

        public ICollection<RemoteAccessSiteUpdateModel> RemoteAccessSites { get; set; }

        public ICollection<RemoteAccessLocation> RemoteAccessLocations { get; set; }

        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }

        public ICollection<OboSite> OboSites { get; set; }

        public ICollection<EnrolleeHealthAuthority> EnrolleeHealthAuthorities { get; set; }

        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string DeviceProviderNumber { get; set; }

        public bool? IsInsulinPumpProvider { get; set; }

        public ICollection<SelfDeclaration> SelfDeclarations { get; set; }

        // These properties are set by the backend from the JWT token; we cannot trust these properties from the frontend
        [JsonIgnore]
        public int IdentityAssuranceLevel { get; set; }

        [JsonIgnore]
        public string IdentityProvider { get; set; }

        public void SetPropertiesFromToken(ClaimsPrincipal user)
        {
            IdentityProvider = user.FindFirstValue(Claims.IdentityProvider);
            IdentityAssuranceLevel = user.GetIdentityAssuranceLevel();
        }

        public bool Validate(ClaimsPrincipal user)
        {
            return FirstName == user.FindFirstValue(Claims.GivenName)
                && LastName == user.FindFirstValue(Claims.FamilyName)
                && GivenNames == user.FindFirstValue(Claims.GivenNames)
                && Equals(VerifiedAddress, user.GetVerifiedAddress());
        }
    }
}
