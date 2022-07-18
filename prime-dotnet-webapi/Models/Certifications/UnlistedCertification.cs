using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Prime.Models
{
    [Table("UnlistedCertification")]
    public class UnlistedCertification : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public int EnrolleeId { get; set; }
        public string UnlistedCollegeName { get; set; }
        public string UnlistedCollegeCode { get; set; }
        public DateTimeOffset? UnlistedRenewalDate { get; set; }
        public Enrollee Enrollee { get; set; }
    }
}
