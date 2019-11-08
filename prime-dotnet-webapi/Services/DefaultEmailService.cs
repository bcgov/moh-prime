using System;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace Prime.Services
{
    public class DefaultEmailService : BaseService, IEmailService
    {
        public DefaultEmailService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public void Send(string from, string to, string subject, string body)
        {
            var fromAddress = new MailAddress(from);
            var toAddress = new MailAddress(to);

            MailMessage mail = new MailMessage(from: fromAddress, to: toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            SmtpClient smtp = new SmtpClient("127.0.0.1", 1025);
            try
            {
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException
                 || ex is SmtpException
                 || ex is SmtpFailedRecipientException
                 || ex is SmtpFailedRecipientsException)
                {
                    Console.Write(ex.Message);
                    return;
                }

                throw;
            }
        }
    }
}
