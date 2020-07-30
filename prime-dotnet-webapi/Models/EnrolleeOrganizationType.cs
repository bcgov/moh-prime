using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeOrganizationType")]
    public class EnrolleeOrganizationType : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int OrganizationTypeCode { get; set; }

        [JsonIgnore]
        public OrganizationType OrganizationType { get; set; }
    }
}
