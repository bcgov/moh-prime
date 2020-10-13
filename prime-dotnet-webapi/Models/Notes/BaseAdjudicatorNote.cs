using System;
using System.ComponentModel.DataAnnotations;

namespace Prime.Models
{
    public class BaseAdjudicatorNote : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        [Required]
        public string Note { get; set; }

        public DateTimeOffset NoteDate { get; set; }
    }
}
