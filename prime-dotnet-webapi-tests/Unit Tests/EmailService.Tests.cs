using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.Services
{
    public class EmailServiceTests
    {
        public static EmailService CreateService(
            ApiDbContext context = null,
            IHttpContextAccessor httpContext = null,
            IRazorConverterService razorConverterService = null
        )
        {
            return new EmailService(
                context ?? A.Fake<ApiDbContext>(),
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                razorConverterService ?? A.Fake<IRazorConverterService>()
            );
        }
    }
}

//     public interface IEmailService
//     {
//         Task SendReminderEmailAsync(Enrollee enrollee);

//         Task SendProvisionerLinkAsync(string[] recipients, EnrolmentCertificateAccessToken token, string provisionerName = null);
//     }
