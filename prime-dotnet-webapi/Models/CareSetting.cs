using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("CareSettingLookup")]
    public class CareSetting : ILookup<int>
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }
    }
}
