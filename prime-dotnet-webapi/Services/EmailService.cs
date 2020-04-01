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
            return emails.Select(e => IsValidEmail(e)).All(x => x);
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

        public async Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null)
        {
            if (!AreValidEmails(recipients))
            {
                throw new ArgumentException("Cannot send provisioner link, supplied email address(es) are invalid.");
            }

            if (token.Enrollee == null)
            {
                await _context.Entry(token).Reference(t => t.Enrollee).LoadAsync();
            }

            // Always send a copy to the enrollee
            var ccEmails = new List<string>() { token.Enrollee.ContactEmail };

            string subject = "New Access Request";
            string emailBody = (string.IsNullOrEmpty(provisionerName))
                ? this.GetClinicManagerEmailBody(token.Enrollee, token)
                : this.GetVendorEmailBody(token.Enrollee, token, provisionerName);

            await Send(PRIME_EMAIL, recipients, ccEmails, subject, emailBody);
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

        private string GetClinicManagerEmailBody(Enrollee enrollee, EnrolmentCertificateAccessToken token)
        {
            var body = "To: Clinic Manager (person responsible for coordinating PharmaNet access):<br><br>";

            body += $"{enrollee.FirstName} {enrollee.LastName} has been approved for Community Practice Access to PharmaNet.<br><br>";

            body += "<b>To set up their access, you must forward this PRIME Enrolment Confirmation";
            body += " and the information specified below to your <u>PharmaNet Software Vendor</u>.</b><br><br>";

            body += "<ol>";

            body += "<li style='margin-bottom:0.75rem;'>";
            body += "Name of Medical Clinic:";
            body += "</li>";

            body += "<li style='margin-bottom:0.75rem;'>";
            body += "Clinic Address:";
            body += "</li>";

            body += "<li style='margin-bottom:0.75rem;'>";
            body += "Pharmacy Equivalency Code (PEC): <i>(this is your PharmaNet site ID - ask your Vendor, if you are unsure)</i>";
            body += "</li>";

            body += "<li style='margin-bottom:0.75rem;'>";
            body += "For <b>Physicians, Pharmacists, and Nurse Practitioners:</b><br><br>";
            body += "College Name and College ID of this user: ";
            body += "<i>(leave this blank if this user is not a Physician, Pharmacist or Nurse Practitioner)</i>";
            body += "</li>";

            body += "<li style='margin-bottom:0.75rem;'>";
            body += "For users who work <b>On Behalf Of</b> a Physician, Pharmacist, or Nurse Practitioner:<br><br>";
            body += "College Name(s) and College ID(s) of the Physicians, Pharmacists or Nurse Practitioners ";
            body += "who this user will access PharmaNet on behalf of: ";
            body += "<i>(leave this blank if this user is a Pharmacist, Nurse Practitioner or Physician)</i>";
            body += "</li>";

            body += "</ol>";

            body += $"<a href=\"{token.FrontendUrl}\">{token.FrontendUrl}</a>. ";
            body += $"<b>This link will expire after {_certificateService.GetMaxViews()} views or {_certificateService.GetExpiryDays()} days.</b><br><br>";

            body += "Thank you,<br><br>";

            body += "PRIME<br><br>";
            //TODO: Is this stored as an attribute in the backend?
            body += "1-844-39PRIME<br><br>";
            //TODO: Is this stored as an attribute in the backend?
            body += "<a href='mailto:PRIMEsupport@gov.bc.ca' target='_top'>PRIMEsupport@gov.bc.ca</a>";

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
