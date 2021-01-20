using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("EnrolmentEscalation")]
    public class EnrolmentEscalation : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int EnrolleeNoteId { get; set; }

        [JsonIgnore]
        public EnrolleeNote EnrolleeNote { get; set; }

        public int AdminId { get; set; }

        public Admin Admin { get; set; }

        public int AssigneeId { get; set; }

        public Admin Assignee { get; set; }
    }
}
