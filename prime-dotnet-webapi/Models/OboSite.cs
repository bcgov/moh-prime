using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("OboSite")]
    public abstract class OboSite : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        public PhysicalAddress PhysicalAddress { get; set; }
    }

    public class CommunityPracticeSite : OboSite
    {
        public string SiteName { get; set; }
        public string PEC { get; set; }
    }

    public class CommunityPharmacySite : OboSite
    {
        public string SiteName { get; set; }
        public string PEC { get; set; }
    }

    public class HealthAuthoritySite : OboSite
    {
        public string Facility { get; set; }
    }
}

