using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("OrganizationTypeLookup")]
    public class OrganizationType : BaseAuditable, ILookup<int>
    {
        public readonly static int HealthAuthority = 1;
        public readonly static int CommunityPractice = 2;
        public readonly static int CommunityPharmacy = 3;
        public readonly static int DeviceProvider = 4;

        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<EnrolleeOrganizationType> EnrolleeOrganizationTypes { get; set; }

        [JsonIgnore]
        public ICollection<LicenseClassClauseMapping> LicenseClassClauseMappings { get; set; }
    }
}
