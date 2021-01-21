using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DelegateDecompiler.EntityFrameworkCore;

using Prime.Models;
using Prime.HttpClients;
using Prime.Services.Razor;
using Prime.HttpClients.Mail;
using Prime.ViewModels.Emails;

namespace Prime.Services
{
    public class EmailParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TokenUrl { get; set; }
        public string ProvisionerName { get; set; }

        public DateTimeOffset? RenewalDate { get; set; }
        public Site Site { get; set; }
        public string DocumentUrl { get; set; }
        public int MaxViews { get => EnrolmentCertificateAccessToken.MaxViews; }
        public int ExpiryDays { get => EnrolmentCertificateAccessToken.Lifespan.Days; }

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

        public EmailParams(string firstName, string lastName, DateTimeOffset expiryDate)
        {
            FirstName = firstName;
            LastName = lastName;
            RenewalDate = expiryDate;
        }

        public EmailParams(Site site, string documentUrl)
        {
            Site = site;
            DocumentUrl = documentUrl;
        }

        public EmailParams(Site site)
        {
            Site = site;
        }
    }

    public class EmailService : BaseService, IEmailService
    {
        private const string PrimeEmail = "no-reply-prime@gov.bc.ca";
        private const string PrimeSupportEmail = "primesupport@gov.bc.ca";
        private const string MohEmail = "HLTH.HnetConnection@gov.bc.ca";
        private readonly IRazorConverterService _razorConverterService;
        private readonly IDocumentService _documentService;
        private readonly IPdfService _pdfService;
        private readonly IOrganizationService _organizationService;
        private readonly IChesClient _chesClient;
        private readonly ISmtpEmailClient _smtpEmailClient;
        private readonly IDocumentManagerClient _documentManagerClient;
        private readonly IDocumentAccessTokenService _documentAccessTokenService;
        private readonly ISiteService _siteService;
        private readonly IAgreementService _agreementService;
        private readonly IEmailManagementService _emailManagementService;

        public EmailService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IRazorConverterService razorConverterService,
            IDocumentService documentService,
            IPdfService pdfService,
            IOrganizationService organizationService,
            IChesClient chesClient,
            ISmtpEmailClient smtpEmailClient,
            IDocumentManagerClient documentManagerClient,
            IDocumentAccessTokenService documentAccessTokenService,
            ISiteService siteService,
            IAgreementService agreementService,
            IEmailManagementService emailManagementService)
            : base(context, httpContext)
        {
            _razorConverterService = razorConverterService;
            _documentService = documentService;
            _pdfService = pdfService;
            _organizationService = organizationService;
            _chesClient = chesClient;
            _documentManagerClient = documentManagerClient;
            _documentAccessTokenService = documentAccessTokenService;
            _smtpEmailClient = smtpEmailClient;
            _siteService = siteService;
            _agreementService = agreementService;
            _emailManagementService = emailManagementService;
        }

        public async Task SendReminderEmailAsync(int enrolleeId)
        {
            var enrolleeEmail = await _context.Enrollees
                .Where(e => e.Id == enrolleeId)
                .Select(e => e.Email)
                .SingleOrDefaultAsync();

            var email = new Email
            (
                from: PrimeEmail,
                to: enrolleeEmail,
                subject: "PRIME Requires your Attention",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.Reminder, null)
            );
            await Send(email);
        }

        public async Task SendProvisionerLinkAsync(IEnumerable<string> emails, EnrolmentCertificateAccessToken token, int careSettingCode)
        {
            var enrolleeDto = await _context.Enrollees
                .Where(e => e.Id == token.EnrolleeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Email
                })
                .SingleAsync();

            var template = careSettingCode switch
            {
                (int)CareSettingType.CommunityPharmacy => RazorTemplates.Emails.CommunityPharmacyManager,
                (int)CareSettingType.HealthAuthority => RazorTemplates.Emails.HealthAuthority,
                _ => RazorTemplates.Emails.CommunityPractice,
            };

            var viewModel = new ProvisionerAccessEmailViewModel
            {
                FullName = $"{enrolleeDto.FirstName} {enrolleeDto.LastName}",
                TokenUrl = token.FrontendUrl,
                ExpiresInDays = EnrolmentCertificateAccessToken.Lifespan.Days
            };

            var email = new Email(
                from: PrimeEmail,
                to: emails,
                cc: enrolleeDto.Email,
                subject: "New Access Request",
                body: await _razorConverterService.RenderTemplateToStringAsync(template, viewModel)
            );
            await Send(email);
        }

        public async Task SendSiteRegistrationAsync(Site site)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(site.Id);

            var email = new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: "PRIME Site Registration Submission",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteRegistrationSubmission, new EmailParams(site, downloadUrl)),
                attachments: await GetSiteRegistrationAttachments(site)
            );
            await Send(email);

            var siteRegReviewPdf = email.Attachments.Single(a => a.Filename == "SiteRegistrationReview.pdf");
            await SaveSiteRegistrationReview(site.Id, siteRegReviewPdf);
        }

        public async Task SendRemoteUsersUpdatedAsync(Site site)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(site.Id);

            var email = new Email
            (
                from: PrimeEmail,
                to: new[] { MohEmail, PrimeSupportEmail },
                subject: "Remote Practioners Added",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.UpdateRemoteUsers, new EmailParams(site, downloadUrl)),
                attachments: await GetSiteRegistrationAttachments(site)
            );
            await Send(email);
        }

        public async Task SendRemoteUserNotificationsAsync(Site site, IEnumerable<RemoteUser> remoteUsers)
        {
            var body = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RemoteUserNotification, new EmailParams(site));

            foreach (var remoteUser in remoteUsers)
            {
                var email = new Email
                (
                    from: PrimeEmail,
                    to: remoteUser.Email,
                    subject: "Remote Practitioner Notification",
                    body: body
                );
                await Send(email);
            }
        }

        public async Task SendBusinessLicenceUploadedAsync(Site site)
        {
            var downloadUrl = await GetBusinessLicenceDownloadLink(site.Id);

            var email = new Email
            (
                from: PrimeEmail,
                to: site.Adjudicator.Email,
                subject: "Site Business Licence Uploaded",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.BusinessLicenceUploaded, new EmailParams(site, downloadUrl))
            );
            await Send(email);
        }

        public async Task SendSiteApprovedPharmaNetAdministratorAsync(Site site)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: site.AdministratorPharmaNet.Email,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedPharmaNetAdministratorEmailTemplate, new EmailParams(site))
            );
            await Send(email);
        }

        public async Task SendSiteApprovedSigningAuthorityAsync(Site site)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: site.Provisioner.Email,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedSigningAuthorityEmailTemplate, new EmailParams(site))
            );
            await Send(email);
        }

        public async Task SendSiteApprovedHIBCAsync(Site site)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: MohEmail,
                subject: "Site Registration Approved",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.SiteApprovedHIBCEmailTemplate, new EmailParams(site))
            );
            await Send(email);
        }

        private async Task<string> GetBusinessLicenceDownloadLink(int siteId)
        {
            var businessLicence = await _siteService.GetBusinessLicenceAsync(siteId);
            if (businessLicence.BusinessLicenceDocument == null)
            {
                return "";
            }
            var documentAccessToken = await _documentAccessTokenService.CreateDocumentAccessTokenAsync(businessLicence.BusinessLicenceDocument.DocumentGuid);
            return documentAccessToken.DownloadUrl;
        }

        private async Task<IEnumerable<Pdf>> GetSiteRegistrationAttachments(Site site)
        {
            var organization = site.Organization;

            var organizationAgreementHtml = "";
            var organizationAgreementFilename = "OrganizationAgreement.pdf";
            var registrationReviewFilename = "SiteRegistrationReview.pdf";

            var siteRegistrationReviewHtml = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.SiteRegistrationReview, site);

            var signedOrganizationAgreementDocument = await _organizationService.GetLatestSignedAgreementAsync(organization.Id);
            if (signedOrganizationAgreementDocument != null)
            {
                var fileExt = signedOrganizationAgreementDocument.Filename.Split(".").Last();

                if (fileExt.Equals("pdf"))
                {
                    // If the file is already a pdf we can skip the conversion steps and return it.
                    var stream = await _documentService.GetStreamForLatestSignedAgreementDocument(organization.Id);
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);

                    return new []
                    {
                        new Pdf(organizationAgreementFilename, ms.ToArray()),
                        new Pdf(registrationReviewFilename, _pdfService.Generate(siteRegistrationReviewHtml))
                    };
                }

                Pdf organizationAgreementDoc;
                RazorTemplate<Document> template = RazorTemplates.Document;
                try
                {
                    var stream = await _documentService.GetStreamForLatestSignedAgreementDocument(organization.Id);
                    var ms = new MemoryStream();
                    stream.CopyTo(ms);
                    organizationAgreementDoc = new Pdf("SignedOrganizationAgreement.pdf", ms.ToArray());
                }
                catch (NullReferenceException)
                {
                    organizationAgreementDoc = new Pdf("SignedOrganizationAgreement.pdf", new byte[20]);
                    template = RazorTemplates.ApologyDocument;
                }

                organizationAgreementHtml = await _razorConverterService.RenderTemplateToStringAsync(template, organizationAgreementDoc);
            }
            else
            {
                var agreementType = _organizationService.OrgAgreementTypeForSiteSetting(site.CareSettingCode.Value);
                var agreementDate = await _context.Agreements
                    .AsNoTracking()
                    .Include(a => a.AgreementVersion)
                    .Where(a => a.OrganizationId == organization.Id
                        && a.AgreementVersion.AgreementType == agreementType
                        && a.AcceptedDate.HasValue)
                    .OrderByDescending(a => a.AcceptedDate)
                    .Select(a => a.AcceptedDate)
                    .FirstAsync();

                organizationAgreementHtml = await _agreementService.RenderOrgAgreementHtmlAsync(agreementType, organization.Name, agreementDate, true);
            }

            return new[]
            {
                new Pdf(organizationAgreementFilename, _pdfService.Generate(organizationAgreementHtml)),
                new Pdf(registrationReviewFilename, _pdfService.Generate(siteRegistrationReviewHtml))
            };
        }

        private async Task SaveSiteRegistrationReview(int siteId, Pdf pdf)
        {
            var documentGuid = await _documentManagerClient.SendFileAsync(new MemoryStream(pdf.Data), pdf.Filename, $"sites/{siteId}/site_registration_reviews");

            _context.SiteRegistrationReviewDocuments.Add(new SiteRegistrationReviewDocument(siteId, documentGuid, pdf.Filename));
            await _context.SaveChangesAsync();
        }

        public async Task SendEnrolleeRenewalEmails()
        {
            var reminderEmailsIntervals = new List<double> { 14, 7, 3, 2, 1, 0 };

            var enrollees = await _context.Enrollees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    e.ExpiryDate
                })
                .Where(e => e.ExpiryDate != null)
                .DecompileAsync()
                .ToListAsync();

            foreach (var enrollee in enrollees)
            {
                var expiryDays = (DateTime.Now.Date - enrollee.ExpiryDate.Value.Date).TotalDays;
                if (reminderEmailsIntervals.Contains(expiryDays))
                {
                    await SendRenewalRequiredAsync(enrollee.Email, enrollee.FirstName, enrollee.LastName, enrollee.ExpiryDate.Value);
                }
                if (expiryDays == -1)
                {
                    await SendRenewalPassedAsync(enrollee.Email, enrollee.FirstName, enrollee.LastName, enrollee.ExpiryDate.Value);
                }
            }
        }

        private async Task SendRenewalRequiredAsync(string enrolleeEmail, string firstName, string lastName, DateTimeOffset expiryDate)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: enrolleeEmail,
                subject: "PRIME Renewal Required",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RenewalRequired, new EmailParams(firstName, lastName, expiryDate))
            );
            await Send(email);
        }

        private async Task SendRenewalPassedAsync(string enrolleeEmail, string firstName, string lastName, DateTimeOffset expiryDate)
        {
            var email = new Email
            (
                from: PrimeEmail,
                to: enrolleeEmail,
                subject: "Your PRIME Renewal Date Has Passed",
                body: await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Emails.RenewalPassed, new EmailParams(firstName, lastName, expiryDate))
            );
            await Send(email);
        }

        private async Task Send(Email email)
        {
            if (!PrimeEnvironment.IsProduction)
            {
                email.Subject = $"THE FOLLOWING EMAIL IS A TEST: {email.Subject}";
            }

            if (PrimeEnvironment.ChesApi.Enabled && await _chesClient.HealthCheckAsync())
            {
                var msgId = await _chesClient.SendAsync(email);
                await _emailManagementService.CreateChesEmailLog(email, msgId);

                if (msgId != null)
                {
                    return;
                }
            }

            // Allways fall back to smtp
            await _smtpEmailClient.SendAsync(email);
            await _emailManagementService.CreateSmtpEmailLog(email);
        }
    }
}
