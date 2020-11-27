using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("EmailLog")]
    public class EmailLog : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string SendType { get; set; }

        public Guid? MsgId { get; set; }

        public string SentTo { get; set; }

        public string Cc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTimeOffset? DateSent { get; set; }

        public string LatestStatus { get; set; }

        public string StatusMessage { get; set; }
    }
}
