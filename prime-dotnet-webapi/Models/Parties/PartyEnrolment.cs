using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
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
