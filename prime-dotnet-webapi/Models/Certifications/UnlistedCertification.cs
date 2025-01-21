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
        public string CollegeName { get; set; }
        public string LicenceNumber { get; set; }
        public string LicenceClass { get; set; }
        public DateTimeOffset? RenewalDate { get; set; }
        public Enrollee Enrollee { get; set; }
    }
}
