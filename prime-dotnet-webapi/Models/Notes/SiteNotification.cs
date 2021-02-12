using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("SiteNotification")]
    public class SiteNotification : BaseNotification
    {
        public int SiteRegistrationNoteId { get; set; }
        public SiteRegistrationNote SiteRegistrationNote { get; set; }
    }
}
