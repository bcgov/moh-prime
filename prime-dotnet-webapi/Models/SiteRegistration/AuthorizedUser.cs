using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public class AuthorizedUser : BaseAuditable
    {
        public string EmploymentIdentifier { get; set; }

        [Required]
        public HealthAuthorityCode HealthAuthorityCode { get; set; }

        [JsonIgnore]
        public HealthAuthority HealthAuthority { get; set; }

        public AccessStatusType Status { get; set; }

        public Party Party { get; set; }
    }
}
