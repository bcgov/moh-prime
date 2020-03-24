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
        private readonly IEnrolmentCertificateService _certificateService;

        public EmailService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IEnrolmentCertificateService enrolmentCertificateService)
            : base(context, httpContext)
        {
            _certificateService = enrolmentCertificateService;
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

        public static bool AreValidEmails(string[] emails)
        {
            return emails.Select(e => IsValidEmail(e)).Aggregate((x, y) => x && y);
        }

        public async Task SendReminderEmailAsync(Enrollee enrollee)
        {
            if (!IsValidEmail(enrollee.ContactEmail))
            {
                // TODO Log invalid email, cannot send?
                return;
            }

            string subject = "PRIME Requires your Attention";
            string body = $"Your PRIME application status has changed since you last viewed it. Please click <a href=\"{PrimeConstants.FRONTEND_URL}\">here</a> to log into PRIME and view your status.";

            await Send(PRIME_EMAIL, enrollee.ContactEmail, subject, body);
        }

        public async Task SendProvisionerLinkAsync(string provisionerName, string provisionerEmail, EnrolmentCertificateAccessToken token)
        {
            // Always send a copy to the enrollee
            var ccEmails = new List<string>() { token.Enrollee.ContactEmail };

            if (!IsValidEmail(provisionerEmail))
            {
                throw new ArgumentException("Cannot send provisioner link, supplied provisioner email address is invalid.");
            }

            if (token.Enrollee == null)
            {
                await _context.Entry(token).Reference(t => t.Enrollee).LoadAsync();
            }

            string subject = "New Access Request";
            string vendorBody = this.GetVendorEmailBody(token.Enrollee, token, provisionerName);

            await Send(PRIME_EMAIL, new[] { provisionerEmail }, ccEmails, subject, vendorBody);
        }

        public async Task SendOfficeManagerEmailAsync(string[] officeManagerEmails, EnrolmentCertificateAccessToken token)
        {
            // Always send a copy to the enrollee
            var ccEmails = new List<string>() { token.Enrollee.ContactEmail };

            if (!AreValidEmails(officeManagerEmails))
            {
                throw new ArgumentException("Cannot send provisioner link to office manager, supplied email address(es) are invalid.");
            }

            if (token.Enrollee == null)
            {
                await _context.Entry(token).Reference(t => t.Enrollee).LoadAsync();
            }

            string subject = "New Access Request";
            string officeManagerBody = this.GetOfficeManagerEmailBody(token.Enrollee, token);

            await Send(PRIME_EMAIL, officeManagerEmails, ccEmails, subject, officeManagerBody);
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
                }

                throw;
            }
            finally
            {
                smtp.Dispose();
                mail.Dispose();
            }
        }

        private string GetVendorEmailBody(Enrollee enrollee, EnrolmentCertificateAccessToken token, string provisionerName)
        {
            var body = $"To: {provisionerName}<br><br>";
            body += $"{enrollee.FirstName} { enrollee.LastName} ";
            body += "has been approved for <b>PharmaNet</b> access. Please see <b>PRIME enrolment information</b> in URL below.<br><br>";
            body += $"<a href=\"{token.FrontendUrl}\">{token.FrontendUrl}</a>. ";
            body += $"<b>This link will expire after {_certificateService.GetMaxViews()} views or {_certificateService.GetExpiryDays()} days</b>.<br><br>";
            body += "Thank you.";
            return body;
        }

        private string GetOfficeManagerEmailBody(Enrollee enrollee, EnrolmentCertificateAccessToken token)
        {
            var body = $"To: Office Manager (person responsible for PharmaNet software at your clinic):<br><br>";
            body += $"{enrollee.FirstName} { enrollee.LastName} has been approved for <b>PharmaNet</b> access.";
            body += "To set up the user’s access, you must forward the <b>PRIME enrolment URL</b> below to your PharmaNet software vendor.<br><br>";
            body += $"<a href=\"{token.FrontendUrl}\">{token.FrontendUrl}</a>. ";
            body += $"<b>This link will expire after {_certificateService.GetMaxViews()} views or {_certificateService.GetExpiryDays()} days.</b><br><br>";
            body += "Thank you.";
            return body;
        }

        public class EmailServiceException : Exception
        {
            public EmailServiceException() { }
            public EmailServiceException(string message) : base(message) { }
            public EmailServiceException(string message, Exception inner) : base(message, inner) { }
        }
    }
}
