using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.Models
{

    [Table("Certification")]
    public class Certification : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public short CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9]+)", ErrorMessage = "License Number should be alpha numeric characters")]
        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string LicenseNumber { get; set; }

        [Required]
        public short LicenseCode { get; set; }

        [JsonIgnore]
        public License License { get; set; }

        [Required]
        public DateTime RenewalDate { get; set; }

        public short? PracticeCode { get; set; }

        [JsonIgnore]
        public Practice Practice { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string FullLicenseNumber { get { return $"{College?.Prefix}-{LicenseNumber}"; } }
    }
}
