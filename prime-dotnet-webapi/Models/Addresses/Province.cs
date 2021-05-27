using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("ProvinceLookup")]
    public class Province : ILookup<string>
    {
        public readonly static string BRITISH_COLUMBIA_CODE = "BC";

        [Key]
        public string Code { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
