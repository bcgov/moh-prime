using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using Prime.Services.EmailInternal;
using PrimeTests.Utils;
using Prime.HttpClients;
using Prime.HttpClients.Mail;

namespace PrimeTests.UnitTests
{
    public class EmailServiceTests : InMemoryDbTest
    {
        public EmailService CreateService(
            IHttpContextAccessor httpContext = null,
            IEmailDocumentsService emailDocumentService = null,
            IEmailRenderingService emailRenderingService = null,
            IChesClient chesClient = null,
            ISmtpEmailClient smtpEmailClient = null
            )
        {
            return new EmailService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                emailDocumentService ?? A.Fake<IEmailDocumentsService>(),
                emailRenderingService ?? A.Fake<IEmailRenderingService>(),
                chesClient ?? A.Fake<IChesClient>(),
                smtpEmailClient ?? A.Fake<ISmtpEmailClient>()
            );
        }
    }
}
