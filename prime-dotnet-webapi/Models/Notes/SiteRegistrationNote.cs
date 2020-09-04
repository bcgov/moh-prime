using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("SiteRegistrationNote")]
    public class SiteRegistrationNote : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int SiteId { get; set; }

        [JsonIgnore]
        public Site Site { get; set; }

        public int AdjudicatorId { get; set; }

        public Admin Adjudicator { get; set; }

        [Required]
        public string Note { get; set; }

        public DateTimeOffset NoteDate { get; set; }
    }
}
