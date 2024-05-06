using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.Models
{

    [Table("RemoteUserCertification")]
    public class RemoteUserCertification : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int RemoteUserId { get; set; }

        [JsonIgnore]
        public RemoteUser RemoteUser { get; set; }

        public int CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        [RegularExpression(@"([a-zA-Z0-9]+)", ErrorMessage = "License Number should be alpha numeric characters")]
        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string LicenseNumber { get; set; }

        [RegularExpression(@"([a-zA-Z0-9]{5})", ErrorMessage = "Practitioner ID should be 5 alphanumeric characters")]
        public string PractitionerId { get; set; }

        public int LicenseCode { get; set; }

        [JsonIgnore]
        public License License { get; set; }
    }
}
