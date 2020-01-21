using System;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Prime.Models;

namespace Prime.Services
{
    public class EmailService : BaseService, IEmailService
    {
        public EmailService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public void Send(string from, string to, string subject, string body)
        {
            // Throws if email is invalid
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

            SmtpClient smtp = new SmtpClient(PrimeConstants.MAIL_SERVER_URL, PrimeConstants.MAIL_SERVER_PORT);
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
                    // TODO log mail exception, parhaps in a table in the database?
                }

                throw;
            }
        }

        public void SendReminderEmail(Enrollee enrollee)
        {
            if (!IsValidEmail(enrollee.ContactEmail))
            {
                // TODO Log invalid email, cannot send?
                return;
            }

            string from = "noreply@prime.gov.bc.ca";
            string subject = "Prime requires your attention";
            string body = $"Your Prime application status has changed since you last viewed it. Please click <a href=\"{PrimeConstants.FRONTEND_URL}\">here</a> to log into Prime and view your status.";

            Send(from, enrollee.ContactEmail, subject, body);
        }

        private bool IsValidEmail(string email)
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
    }
}
