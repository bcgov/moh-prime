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
    public class EnrolmentCertificateServiceTests2
    {
        public static EnrolmentCertificateService CreateService(
            ApiDbContext context = null,
            IHttpContextAccessor httpContext = null,
            IAccessTermService accessTermService = null,
            IEnrolleeProfileVersionService enroleeProfileVersionService = null)
        {
            return new EnrolmentCertificateService(
                context ?? A.Fake<ApiDbContext>(),
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                accessTermService ?? A.Fake<IAccessTermService>(),
                enroleeProfileVersionService ?? A.Fake<IEnrolleeProfileVersionService>()
            );
        }
    }
}

//     public interface IEnrolmentCertificateService
//     {
//         Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId);

//         Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenAsync(Enrollee enrollee);

//         Task<IEnumerable<EnrolmentCertificateAccessToken>> GetCertificateAccessTokensForUserIdAsync(Guid userId);

//         string[] GetPharmaNetProvisionerNames();

//         string GetPharmaNetProvisionerEmail(string pharmaNetVendor);

//         int GetMaxViews();

//         int GetExpiryDays();
//     }
