using System.ComponentModel.DataAnnotations;

namespace Prime.Models
{
    public class PartyEnrolment
    {
        [Key]
        public int Id { get; set;  }

        public int PartyId { get; set; }

        public Party Party { get; set; }

        public PartyType PartyType { get; set; }
    }
}
