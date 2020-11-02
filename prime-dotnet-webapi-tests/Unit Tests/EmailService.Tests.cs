using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using Prime.HttpClients;

namespace PrimeTests.UnitTests
{
    public class EmailServiceTests : InMemoryDbTest
    {
        public EmailService CreateService(
            IHttpContextAccessor httpContext = null,
            IRazorConverterService razorConverterService = null,
            IDocumentService documentService = null,
            IPdfService pdfService = null,
            IOrganizationService organizationService = null,
            IChesClient chesClient = null,
            ISmtpEmailClient smtpEmailClient = null,
            IDocumentManagerClient documentManagerClient = null,
            IDocumentAccessTokenService documentAccessTokenService = null,
            ISiteService siteService = null,
            IAgreementService agreementService = null
            )
        {
            return new EmailService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                razorConverterService,
                documentService,
                pdfService,
                organizationService,
                chesClient,
                smtpEmailClient,
                documentManagerClient,
                documentAccessTokenService,
                siteService,
                agreementService
            );
        }
    }
}

//     public interface IEmailService
//     {
//         Task SendReminderEmailAsync(Enrollee enrollee);

//         Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null);
//     }
