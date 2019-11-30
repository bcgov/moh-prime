using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.Models
{

    [Table("Certification")]
    public class Certification : BaseAuditable, IEnrolmentNavigationProperty
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolmentId { get; set; }

        [JsonIgnore]
        public Enrolment Enrolment { get; set; }

        [Required]
        public short CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        [Required]
        [RegularExpression(@"([0-9]+)", ErrorMessage = "License Number should not contain characters")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "License Number must be 5 digits")]
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
    }
}
