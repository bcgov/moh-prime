using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Organization")]
    public class Organization
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolmentId { get; set; }

        [JsonIgnore]
        public Enrolment Enrolment { get; set; }

        [Required]
        public string Name { get; set; }

        public short OrganizationTypeCode { get; set; }

        [JsonIgnore]
        public OrganizationType OrganizationType { get; set; }

        public string City { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}