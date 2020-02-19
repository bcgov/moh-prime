using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Prime.Models;

namespace Prime.Services
{
    public class EmailService : BaseService, IEmailService
    {
        private const string PRIME_EMAIL = "no-reply-prime@gov.bc.ca";

        public EmailService(ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

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

        public async Task SendReminderEmailAsync(Enrollee enrollee)
        {
            if (!IsValidEmail(enrollee.ContactEmail))
            {
                // TODO Log invalid email, cannot send?
                return;
            }

            string subject = "Prime requires your attention";
            string body = $"Your Prime application status has changed since you last viewed it. Please click <a href=\"{PrimeConstants.FRONTEND_URL}\">here</a> to log into Prime and view your status.";

            await Send(PRIME_EMAIL, enrollee.ContactEmail, subject, body);
        }

        public async Task SendProvisionerLinkAsync(string provisionerEmail, EnrolmentCertificateAccessToken token)
        {
            if (!IsValidEmail(provisionerEmail))
            {
                throw new ArgumentException("Cannot send provisioner link, supplied email address is invalid.");
            }

            if (token.Enrollee == null)
            {
                await _context.Entry(token).Reference(t => t.Enrollee).LoadAsync();
            }

            string subject = "New access request";
            string body = $"This user has been approved for PharmaNet access. Please click <a href=\"{token.FrontendUrl}\">here</a> to view their information.";

            await Send(PRIME_EMAIL, new[] { provisionerEmail }, new[] { token.Enrollee.ContactEmail }, subject, body);
        }

        private async Task Send(string from, string to, string subject, string body)
        {
            await Send(from, new[] { to }, new string[0], subject, body);
        }

        private async Task Send(string from, IEnumerable<string> to, IEnumerable<string> cc, string subject, string body)
        {
            if (!to.Any())
            {
                throw new ArgumentException("Must specify at least one \"To\" email address.");
            }

            var fromAddress = new MailAddress(from);
            var toAddresses = to.Select(addr => new MailAddress(addr));
            var ccAddresses = cc.Select(addr => new MailAddress(addr));

            if (PrimeConstants.ENVIRONMENT_NAME != "prod")
            {
                subject = $"THE FOLLOWING EMAIL IS A TEST: {subject}";
            }

            MailMessage mail = new MailMessage()
            {
                From = fromAddress,
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            foreach (var address in toAddresses)
            {
                mail.To.Add(address);
            }

            foreach (var address in ccAddresses)
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
                    // TODO log mail exception, perhaps in a table in the database?
                    throw new EmailServiceException("Email service failed", ex);
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
