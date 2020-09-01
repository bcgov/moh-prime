using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.IO;

namespace Prime.Services
{
    public class SmtpEmailClient : ISmtpEmailClient
    {
        public SmtpEmailClient()
        { }

        public async Task SendAsync(
            string from,
            IEnumerable<string> to,
            IEnumerable<string> cc,
            string subject,
            string body,
            IEnumerable<(string Filename, byte[] Content)> attachments
        )
        {
            var fromAddress = new MailAddress(from);

            IEnumerable<Attachment> attachmentsList = attachments.Select(pdf => new Attachment(new MemoryStream(pdf.Content), pdf.Filename, "application/pdf"));

            MailMessage mail = new MailMessage()
            {
                From = fromAddress,
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            foreach (var attachment in attachmentsList)
            {
                mail.Attachments.Add(attachment);
            }
            foreach (var address in to)
            {
                mail.To.Add(address);
            }

            foreach (var address in cc)
            {
                mail.CC.Add(address);
            }

            SmtpClient smtp = new SmtpClient(PrimeEnvironment.MAIL_SERVER_URL, PrimeEnvironment.MAIL_SERVER_PORT);
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
    }
}
