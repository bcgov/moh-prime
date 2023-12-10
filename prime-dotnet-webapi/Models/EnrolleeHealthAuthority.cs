using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class EnrolleeHealthAuthority : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public HealthAuthorityCode HealthAuthorityCode { get; set; }

        [JsonIgnore]
        public HealthAuthority HealthAuthority { get; set; }
    }
}
