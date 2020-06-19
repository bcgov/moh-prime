using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Prime.Services
{
    public class SMTPService : BaseService, ISMTPService
    {
        public SMTPService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
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
            var toAddresses = to.Select(addr => new MailAddress(addr));
            var ccAddresses = cc.Select(addr => new MailAddress(addr));

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

            SmtpClient smtp = new SmtpClient(PrimeConstants.MAIL_SERVER_URL, PrimeConstants.MAIL_SERVER_PORT);
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
                }

                throw;
            }
            finally
            {
                smtp.Dispose();
                mail.Dispose();
            }
        }

        public class EmailServiceException : Exception
        {
            public EmailServiceException() { }
            public EmailServiceException(string message) : base(message) { }
            public EmailServiceException(string message, Exception inner) : base(message, inner) { }
        }
    }
}
