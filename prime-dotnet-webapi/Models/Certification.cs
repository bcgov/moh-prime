using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Certification")]
    public class Certification : BaseAuditable
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolmentId { get; set; }

        [JsonIgnore]
        public Enrolment Enrolment { get; set; }

        public short CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

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