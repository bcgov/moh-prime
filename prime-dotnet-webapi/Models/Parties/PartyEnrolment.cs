using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("PartyEnrolment")]
    public class PartyEnrolment : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PartyId { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }

        public PartyType PartyType { get; set; }
    }
}
