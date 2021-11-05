using System;
using System.Linq;
using System.Collections.Generic;

namespace Prime.HttpClients.Mail.ChesApiDefinitions
{
    public class ChesEmailRequestParams
    {
        public IEnumerable<ChesAttachment> Attachments { get; set; }
        public IEnumerable<string> Bcc { get; set; }
        public string BodyType { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public int? DelayTS { get; set; }
        public string Encoding { get; set; }
        public string From { get; set; }
        public string Priority { get; set; }
        public string Subject { get; set; }
        public string Tag { get; set; }
        public IEnumerable<string> To { get; set; }

        public ChesEmailRequestParams()
        {
            // Defaults
            Bcc = Enumerable.Empty<string>();
            BodyType = "html";
            DelayTS = 0;
            Encoding = "utf-8";
            Priority = "normal";
            Tag = "tag";
        }

        public ChesEmailRequestParams(Email email)
            : this()
        {
            Attachments = email.Attachments.Select(file => new ChesAttachment()
            {
                Content = Convert.ToBase64String(file.Data),
                ContentType = file.MediaType,
                Encoding = "base64",
                Filename = file.Filename
            });

            Body = email.Body;
            Cc = email.Cc;
            From = email.From;
            Subject = email.Subject;
            To = email.To;
        }
    }

    public class ChesAttachment
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
        public string Encoding { get; set; }
        public string Filename { get; set; }
    }

    public class EmailSuccessResponse
    {
        public IEnumerable<Message> Messages { get; set; }
        public Guid TxId { get; set; }
    }

    public class Message
    {
        public Guid MsgId { get; set; }
        public string Tag { get; set; }
        public IEnumerable<string> To { get; set; }
    }

    public class StatusResponse
    {
        public long CreatedTS { get; set; }
        public long DelayTS { get; set; }
        public Guid MsgId { get; set; }
        public string Status { get; set; }
        public IEnumerable<StatusHistoryObject> StatusHistory { get; set; }
        public string Tag { get; set; }
        public Guid TxId { get; set; }
        public long UpdatedTS { get; set; }
    }

    public class StatusHistoryObject
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public int Timestamp { get; set; }
    }

    public static class ChesStatus
    {
        public const string Accepted = "accepted";
        public const string Cancelled = "cancelled";
        public const string Completed = "completed";
        public const string Failed = "failed";
        public const string Pending = "pending";
    }
}
