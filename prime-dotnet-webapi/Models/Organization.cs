using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Organization")]
    public class Organization : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public short OrganizationTypeCode { get; set; }

        [JsonIgnore]
        public OrganizationType OrganizationType { get; set; }
    }
}
