using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolleeCareSetting")]
    public class EnrolleeCareSetting : BaseAuditable, IEnrolleeNavigationProperty
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int CareSettingCode { get; set; }

        [JsonIgnore]
        public CareSetting CareSetting { get; set; }

        public bool IsType(CareSettingType type)
        {
            return CareSettingCode == (int)type;
        }
    }
}
