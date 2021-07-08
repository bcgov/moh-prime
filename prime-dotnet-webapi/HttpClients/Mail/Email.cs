using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;

using Prime.Models.Documents;

namespace Prime.HttpClients.Mail
{
    public class Email
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<Pdf> Attachments { get; set; }

        public Email(string from, string to, string subject, string body)
            : this(from, new[] { to }, Enumerable.Empty<string>(), subject, body, Enumerable.Empty<Pdf>())
        { }

        public Email(string from, IEnumerable<string> to, string subject, string body)
            : this(from, to, Enumerable.Empty<string>(), subject, body, Enumerable.Empty<Pdf>())
        { }

        public Email(string from, IEnumerable<string> to, string cc, string subject, string body)
            : this(from, to, new[] { cc }, subject, body, Enumerable.Empty<Pdf>())
        { }

        public Email(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body, IEnumerable<Pdf> attachments)
        {
            ValidateEmails(from, to, cc);

            From = from;
            To = to;
            Cc = cc;
            Subject = subject;
            Body = body;
            Attachments = attachments;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerable<string> ParseCommaSeparatedEmails(string emailString)
        {
            if (string.IsNullOrWhiteSpace(emailString))
            {
                return Enumerable.Empty<string>();
            }

            var emails = emailString.Split(",")
                .Select(s => s.Trim());
            if (emails.All(e => IsValidEmail(e)))
            {
                return emails;
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        private static void ValidateEmails(string from, IEnumerable<string> to, IEnumerable<string> cc)
        {
            if (!IsValidEmail(from))
            {
                throw new ArgumentException($"\"From\" email {from} is invalid");
            }

            foreach (var email in to)
            {
                if (!IsValidEmail(email))
                {
                    throw new ArgumentException($"\"To\" email {email} is invalid");
                }
            }

            foreach (var email in cc)
            {
                if (!IsValidEmail(email))
                {
                    throw new ArgumentException($"\"CC\" email {email} is invalid");
                }
            }
        }
    }
}
