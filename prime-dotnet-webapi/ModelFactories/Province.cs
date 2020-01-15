using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("ProvinceLookup")]
    public class Province : IDefinable, ILookup<string>
    {
        public readonly static string BRITISH_COLUMBIA_CODE = "BC";

        [Key]
        public string Code ,

        [Required]
        public string CountryCode ,

        [JsonIgnore]
        public Country Country ,

        [Required]
        public string Name ,
    }
}
