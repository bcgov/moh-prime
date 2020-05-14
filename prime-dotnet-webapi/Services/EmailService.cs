using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Prime.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Prime.Services
{
    public class EmailParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TokenUrl { get; set; }
        public int MaxViews { get => EnrolmentCertificateAccessToken.MaxViews; }
        public int ExpiryDays { get => EnrolmentCertificateAccessToken.Lifespan.Days; }
        public string ProvisionerName { get; set; }

        public EmailParams()
        {

        }

        public EmailParams(EnrolmentCertificateAccessToken token, string provisionerName = null)
        {
            FirstName = token.Enrollee.FirstName;
            LastName = token.Enrollee.LastName;
            TokenUrl = token.FrontendUrl;
            ProvisionerName = provisionerName;
        }
    }

    public class EmailService : BaseService, IEmailService
    {
        private const string PRIME_EMAIL = "no-reply-prime@gov.bc.ca";

        private readonly IRazorConverterService _razorConverterService;
        private readonly ICHESApiService _chesApiService;

        public EmailService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IRazorConverterService razorConverterService,
            ICHESApiService chesApiService)
            : base(context, httpContext)
        {
            _razorConverterService = razorConverterService;
            _chesApiService = chesApiService;
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
            return emails.All(e => IsValidEmail(e));
        }

        public async Task SendReminderEmailAsync(Enrollee enrollee)
        {
            if (!IsValidEmail(enrollee.ContactEmail))
            {
                // TODO Log invalid email, cannot send?
                return;
            }

            string subject = "PRIME Requires your Attention";
            string body = await _razorConverterService.RenderViewToStringAsync("/Views/Emails/ReminderEmail.cshtml", new EmailParams());
            await Send(PRIME_EMAIL, enrollee.ContactEmail, subject, body);
        }

        public async Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null)
        {
            if (!AreValidEmails(recipients))
            {
                // TODO Log invalid email, cannot send
                throw new ArgumentException("Cannot send provisioner link, supplied email address(es) are invalid.");
            }

            if (token.Enrollee == null)
            {
                await _context.Entry(token).Reference(t => t.Enrollee).LoadAsync();
            }

            // Always send a copy to the enrollee
            var ccEmails = new List<string>() { token.Enrollee.ContactEmail };

            string subject = "New Access Request";
            string viewName = string.IsNullOrEmpty(provisionerName)
                ? "/Views/Emails/OfficeManagerEmail.cshtml"
                : "/Views/Emails/VendorEmail.cshtml";
            string emailBody = await _razorConverterService.RenderViewToStringAsync(viewName, new EmailParams(token, provisionerName));
            await Send(PRIME_EMAIL, recipients, ccEmails, subject, emailBody);
        }

        public async Task<string> GetPharmaNetProvisionerEmailAsync(string provisionerName)
        {
            var vendor = await _context.Vendors
                .SingleOrDefaultAsync(v => v.Name == provisionerName);

            return vendor?.Email;
        }

        public async Task<IEnumerable<string>> GetPharmaNetProvisionerNamesAsync()
        {
            return await _context.Vendors
                .Select(v => v.Name)
                .ToListAsync();
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

            // If CHES Email Service is running and ENV == prod, else send through smtp
            // PrimeConstants.ENVIRONMENT_NAME == "prod"
            if (await _chesApiService.HealthCheckAsync())
            {
                await _chesApiService.SendAsync(from, to, cc, subject, body);
            }
            else
            {
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
        }


        public class EmailServiceException : Exception
        {
            public EmailServiceException() { }
            public EmailServiceException(string message) : base(message) { }
            public EmailServiceException(string message, Exception inner) : base(message, inner) { }
        }
    }
}
