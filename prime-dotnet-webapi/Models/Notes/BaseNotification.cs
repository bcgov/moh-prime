using System.ComponentModel.DataAnnotations;

namespace Prime.Models
{
    public abstract class BaseNotification : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int AdminId { get; set; }

        public Admin Admin { get; set; }

        public int AssigneeId { get; set; }

        public Admin Assignee { get; set; }
    }
}
