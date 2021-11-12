using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentStatusReference")]
    public class EnrolmentStatusReference : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolmentStatusId { get; set; }

        [JsonIgnore]
        public EnrolmentStatus EnrolmentStatus { get; set; }

        public int? AdjudicatorNoteId { get; set; }

        public EnrolleeNote AdjudicatorNote { get; set; }

        public int? AdminId { get; set; }

        public Admin Adjudicator { get; set; }
    }
}
