using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Organization")]
    public class Organization : BaseAuditable, IEnrolmentNavigationProperty
    {
        [Key]
        public int? Id { get; set; }

        [JsonIgnore]
        public int EnrolmentId { get; set; }

        [JsonIgnore]
        public Enrolment Enrolment { get; set; }

        public short OrganizationTypeCode { get; set; }

        [JsonIgnore]
        public OrganizationType OrganizationType { get; set; }
    }
}
