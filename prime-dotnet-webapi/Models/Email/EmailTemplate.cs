using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("EmailTemplate")]
    public class EmailTemplate : BaseAuditable
    {
        [Key]
        public int Id { get; set; }
        public string Template { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public EmailTemplateType EmailType { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Recipient { get; set; }

        public string VersionedName()
        {
            return $"{EmailType}_{ModifiedDate.ToUnixTimeMilliseconds()}";
        }
    }
}
