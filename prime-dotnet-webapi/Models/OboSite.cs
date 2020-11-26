using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("OboSite")]
    public class OboSite : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int CareSettingCode { get; set; }

        [JsonIgnore]
        public CareSetting CareSetting { get; set; }

        public string SiteName { get; set; }

        public string PEC { get; set; }

        public string Facility { get; set; }

        [Required]
        public PhysicalAddress PhysicalAddress { get; set; }
    }

}

