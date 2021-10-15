using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace Prime.HttpClients.Mail
{
    public class SmtpEmailClient : ISmtpEmailClient
    {
        public async Task SendAsync(Email email)
        {
            var mail = ConvertToMailMessage(email);

            SmtpClient smtp = new SmtpClient(PrimeConfiguration.Current.MailServer.Url, PrimeConfiguration.Current.MailServer.Port);
            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException
                    || ex is SmtpException
                    || ex is SmtpFailedRecipientException
                    || ex is SmtpFailedRecipientsException)
                {
                    // TODO log mail exception
                    Console.WriteLine($"SmtpEmailClient exception: {ex}");
                }

                throw;
            }
            finally
            {
                smtp.Dispose();
                mail.Dispose();
            }
        }

        private static MailMessage ConvertToMailMessage(Email email)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };

            foreach (var address in email.To)
            {
                mail.To.Add(address);
            }

            foreach (var address in email.Cc)
            {
                mail.CC.Add(address);
            }

            foreach (var attachment in email.Attachments)
            {
                mail.Attachments.Add(new Attachment(new MemoryStream(attachment.Data), attachment.Filename, attachment.MediaType));
            }

            return mail;
        }
    }
}
