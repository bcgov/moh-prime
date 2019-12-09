using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("AdjudicatorNotes")]
    public class AdjudicatorNote : BaseAuditable
    {
        [Key]
        public int? Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public string Note { get; set; }
    }
}
