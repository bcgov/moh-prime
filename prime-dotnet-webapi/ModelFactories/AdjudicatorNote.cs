using FactoryGirlCore;
using system;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{
    [Table("AdjudicatorNote")]
    public class AdjudicatorNote : IDefinable, IEnrolleeNote
    {
        [Key]
        public int? Id ,

        public int EnrolleeId ,

        [JsonIgnore]
        public Enrollee Enrollee ,

        [Required]
        public string Note ,

        [Required]
        public DateTime NoteDate ,
    }
}
