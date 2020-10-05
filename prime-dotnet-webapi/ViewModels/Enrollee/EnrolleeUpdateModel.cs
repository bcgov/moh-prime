using System.Collections.Generic;
using Newtonsoft.Json;
using Prime.Infrastructure;
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

    }
}
