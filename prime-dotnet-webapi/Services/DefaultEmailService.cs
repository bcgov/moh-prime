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

            if (PrimeConstants.ENVIRONMENT_NAME != "prod")
            {
                subject = $"THE FOLLOWING EMAIL IS A TEST: {subject}";
            }

            MailMessage mail = new MailMessage(from: fromAddress, to: toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            SmtpClient smtp = new SmtpClient(PrimeConstants.MAIL_SERVER_URL);
            // try
            // {
                smtp.Send(mail);
            // }
            // catch (Exception ex)
            // {
            //     if (ex is InvalidOperationException
            //      || ex is SmtpException
            //      || ex is SmtpFailedRecipientException
            //      || ex is SmtpFailedRecipientsException)
            //     {
            //         // TODO: Log failure
            //         return;
            //     }

            //     throw;
            // }
        }
    }
}
