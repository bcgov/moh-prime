using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.HttpClients;
using Prime.Models;
using Prime.Models.Documents;
using Prime.Services.Razor;
using Prime.ViewModels.SiteRegistration;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Services.EmailInternal
{
    public class EmailDocumentsService : BaseService, IEmailDocumentsService
    {
        private readonly IDocumentAccessTokenService _documentAccessTokenService;
        private readonly IDocumentManagerClient _documentClient;
        private readonly IOrganizationAgreementService _organizationAgreementService;
        private readonly IOrganizationService _organizationService;
        private readonly IPdfService _pdfService;
        private readonly IRazorConverterService _razorConverterService;

        public EmailDocumentsService(
            ApiDbContext context,
            ILogger<EmailDocumentsService> logger,
            IDocumentAccessTokenService documentAccessTokenService,
            IDocumentManagerClient documentClient,
            IOrganizationAgreementService organizationAgreementService,
            IOrganizationService organizationService,
            IPdfService pdfService,
            IRazorConverterService razorConverterService)
            : base(context, logger)
        {
            _documentAccessTokenService = documentAccessTokenService;
            _documentClient = documentClient;
            _organizationAgreementService = organizationAgreementService;
            _organizationService = organizationService;
            _pdfService = pdfService;
            _razorConverterService = razorConverterService;
        }

        public async Task<IEnumerable<Pdf>> GenerateSiteRegistrationSubmissionAttachmentsAsync(int siteId)
        {
            return new[]
            {
                await GenerateRegistrationReviewAttachmentAsync(siteId),
                await GenerateOrganizationAgreementAttachmentAsync(siteId)
            };
        }

        public async Task<string> GetBusinessLicenceDownloadLink(int businessLicenceId)
        {
            var document = await _context.BusinessLicenceDocuments
                .SingleOrDefaultAsync(doc => doc.BusinessLicence.Id == businessLicenceId);

            if (document == null)
            {
                return null;
            }

            var documentAccessToken = await _documentAccessTokenService.CreateDocumentAccessTokenAsync(document.DocumentGuid);
            return documentAccessToken.DownloadUrl;
        }

        public async Task SaveSiteRegistrationReview(int siteId, Pdf pdf)
        {
            var documentGuid = await _documentClient.SendFileAsync(new System.IO.MemoryStream(pdf.Data), pdf.Filename, $"sites/{siteId}/site_registration_reviews");

            _context.SiteRegistrationReviewDocuments.Add(new SiteRegistrationReviewDocument(siteId, documentGuid, pdf.Filename));
            await _context.SaveChangesAsync();
        }

        public async Task<Pdf> GenerateHealthAuthoritySiteRegistrationSubmissionAttachmentsAsync(int healthAuthoritSiteId)
        {
            var model = await _context.HealthAuthoritySites
                .Where(has => has.Id == healthAuthoritSiteId)
                .Select(has => new HealthAuthoritySiteSubmissionViewModel
                {
                    SiteName = has.SiteName,
                    SiteAddress = has.PhysicalAddress,
                    // AuthorizedUser
                    // HealthAuthority
                    PEC = has.PEC,
                    CareType = has.CareType,
                    IsNew = string.IsNullOrWhiteSpace(has.SiteId),
                    Vendor = has.VendorCode,
                    // PharmaNetAdministrator
                })
                .SingleAsync();

            var html = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.HealthAuthoritySiteRegistrationReview, model);
            return new Pdf("HealthAuthoritySiteRegistrationReview.pdf", _pdfService.Generate(html));
        }

        private async Task<Pdf> GenerateRegistrationReviewAttachmentAsync(int siteId)
        {
            // TODO use Automapper
            var model = await _context.Sites
                .Where(s => s.Id == siteId)
                .Select(s => new SiteRegistrationReviewViewModel
                {
                    OrganizationName = s.Organization.Name,
                    OrganizationRegistrationId = s.Organization.RegistrationId,
                    OrganizationDoingBusinessAs = s.Organization.DoingBusinessAs,
                    SiteName = s.DoingBusinessAs,
                    SiteAddress = s.PhysicalAddress,
                    PEC = s.PEC,
                    BusinessHours = s.BusinessHours.Select(h => new BusinessHourViewModel
                    {
                        Day = h.Day,
                        StartTime = h.StartTime,
                        EndTime = h.EndTime
                    }),
                    Vendors = s.SiteVendors.Select(sv => new VendorViewModel
                    {
                        Name = sv.Vendor.Name,
                        Email = sv.Vendor.Email
                    }),
                    RemoteUsers = s.RemoteUsers.Select(ru => new RemoteUserViewModel
                    {
                        FullName = $"{ru.FirstName} {ru.LastName}",
                        Certifications = ru.RemoteUserCertifications.Select(c => new CertViewModel
                        {
                            CollegeName = c.College.Name,
                            LicenceNumber = c.LicenseNumber
                        })
                    }),
                    SigningAuthority = new ContactViewModel
                    {
                        JobTitle = s.Provisioner.JobRoleTitle,
                        FullName = $"{s.Provisioner.FirstName} {s.Provisioner.LastName}",
                        Phone = s.Provisioner.Phone,
                        Fax = s.Provisioner.Fax,
                        SmsPhone = s.Provisioner.SMSPhone,
                        Email = s.Provisioner.Email
                    },
                    AdministratorOfPharmaNet = new ContactViewModel
                    {
                        JobTitle = s.AdministratorPharmaNet.JobRoleTitle,
                        FullName = $"{s.AdministratorPharmaNet.FirstName} {s.AdministratorPharmaNet.LastName}",
                        Phone = s.AdministratorPharmaNet.Phone,
                        Fax = s.AdministratorPharmaNet.Fax,
                        SmsPhone = s.AdministratorPharmaNet.SMSPhone,
                        Email = s.AdministratorPharmaNet.Email
                    },
                    PrivacyOfficer = new ContactViewModel
                    {
                        JobTitle = s.PrivacyOfficer.JobRoleTitle,
                        FullName = $"{s.PrivacyOfficer.FirstName} {s.PrivacyOfficer.LastName}",
                        Phone = s.PrivacyOfficer.Phone,
                        Fax = s.PrivacyOfficer.Fax,
                        SmsPhone = s.PrivacyOfficer.SMSPhone,
                        Email = s.PrivacyOfficer.Email
                    },
                    TechnicalSupport = new ContactViewModel
                    {
                        JobTitle = s.TechnicalSupport.JobRoleTitle,
                        FullName = $"{s.TechnicalSupport.FirstName} {s.TechnicalSupport.LastName}",
                        Phone = s.TechnicalSupport.Phone,
                        Fax = s.TechnicalSupport.Fax,
                        SmsPhone = s.TechnicalSupport.SMSPhone,
                        Email = s.TechnicalSupport.Email
                    },
                })
                .SingleAsync();

            var html = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.SiteRegistrationReview, model);
            return new Pdf("SiteRegistrationReview.pdf", _pdfService.Generate(html));
        }

        private async Task<Pdf> GenerateOrganizationAgreementAttachmentAsync(int siteId)
        {
            var careSetting = await _context.Sites
                .Where(s => s.Id == siteId)
                .Select(s => s.CareSettingCode)
                .SingleOrDefaultAsync();

            if (careSetting == null)
            {
                return null;
            }

            var agreementType = _organizationService.OrgAgreementTypeForSiteSetting(careSetting.Value);
            var agreementDto = await _context.Sites
                .Where(s => s.Id == siteId)
                .SelectMany(s => s.Organization.Agreements)
                .Where(a => a.AgreementVersion.AgreementType == agreementType
                    && a.AcceptedDate.HasValue)
                .OrderByDescending(a => a.AcceptedDate)
                .Select(a => new
                {
                    OrganizationName = a.Organization.Name,
                    a.AcceptedDate,
                    a.SignedAgreement
                })
                .FirstOrDefaultAsync();

            if (agreementDto == null)
            {
                return null;
            }

            byte[] fileData = null;
            if (agreementDto.SignedAgreement == null)
            {
                var html = await _organizationAgreementService.RenderOrgAgreementHtmlAsync(agreementType, agreementDto.OrganizationName, agreementDto.AcceptedDate, true);
                fileData = _pdfService.Generate(html);
            }
            else
            {
                var content = await _documentClient.GetDocumentAsync(agreementDto.SignedAgreement.DocumentGuid);
                if (content == null)
                {
                    return await ApologyDocument(agreementDto.SignedAgreement.Filename);
                }

                fileData = await content.ReadAsByteArrayAsync();

                if (!agreementDto.SignedAgreement.HasFileExtension("pdf"))
                {
                    var html = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Document, new File(agreementDto.SignedAgreement.Filename, fileData));
                    fileData = _pdfService.Generate(html);
                }
            }

            return new Pdf("OrganizationAgreement.pdf", fileData);
        }

        private async Task<Pdf> ApologyDocument(string filename)
        {
            var html = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.ApologyDocument, new File(filename, null));
            return new Pdf("OrganizationAgreement.pdf", _pdfService.Generate(html));
        }
    }
}
