using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.HttpClients.Mail;

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

        public static EmailLog FromEmail(Email email, string sendType, Guid? msgId)
        {
            return new EmailLog
            {
                Body = email.Body,
                Cc = string.Join(",", email.Cc),
                DateSent = DateTimeOffset.Now,
                MsgId = msgId,
                SendType = sendType,
                SentTo = string.Join(",", email.To),
                Subject = email.Subject,
            };
        }
    }
}
