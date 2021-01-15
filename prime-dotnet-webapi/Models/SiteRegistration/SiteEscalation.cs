using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("SiteEscalation")]
    public class SiteEscalation : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SiteRegistrationNoteId { get; set; }

        public SiteRegistrationNote SiteRegistrationNote { get; set; }

        public int AdminId { get; set; }

        public Admin Admin { get; set; }

        public int AssigneeId { get; set; }

        public Admin Assignee { get; set; }
    }
}
