using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Infrastructure;

namespace Prime.Models
{
    [Table("PartyCertification")]
    public class PartyCertification : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int PartyId { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }

        public int CollegeCode { get; set; }

        [JsonIgnore]
        public College College { get; set; }

        public int LicenseCode { get; set; }

        [JsonIgnore]
        public License License { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9]+)", ErrorMessage = "License Number should be alpha numeric characters")]
        [JsonConverter(typeof(EmptyStringToNullJsonConverter))]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// 5-digit numeric number that the PharmaNet College API expects
        /// </summary>
        [RegularExpression(@"([0-9]{5})", ErrorMessage = "Practitioner ID should be 5 numeric characters")]
        public string PractitionerId { get; set; }

        public DateTimeOffset RenewalDate { get; set; }

        public int? PracticeCode { get; set; }

        /// <summary>
        /// E.g. Reproductive Health - Sexually Transmitted Infections
        /// </summary>
        [JsonIgnore]
        public Practice Practice { get; set; }
    }
}