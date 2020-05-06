using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.UnitTests
{
    public class EnrolmentCertificateServiceTests
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

        [Fact]
        public async void TestCertificateCreate()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            var fakeDb = AFake.Db()
                .WithEnrollees(new[] { enrollee });

            // EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
            // Assert.NotNull(token);

            // EnrolmentCertificate cert = await _service.GetEnrolmentCertificateAsync(token.Id);
            // Assert.NotNull(cert);
            // Assert.Equal(enrollee.GPID, cert.GPID);
        }

        // [Fact(Skip = "Max views are temporarily disabled in the app")]
        // public async void testMaxViews()
        // {
        //     int tokenMaxViews = 3;
        //     Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

        //     EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
        //     Assert.NotNull(token);
        //     Assert.Equal(0, token.ViewCount);

        //     for (int view = 1; view <= tokenMaxViews; view++)
        //     {
        //         Assert.NotNull(await _service.GetEnrolmentCertificateAsync(token.Id));
        //         Assert.Equal(view, token.ViewCount);
        //     }

        //     Assert.Null(await _service.GetEnrolmentCertificateAsync(token.Id));
        // }

        // [Fact]
        // public async void testExpiryDate()
        // {
        //     TimeSpan tokenLifespan = TimeSpan.FromDays(7);
        //     TimeSpan tolerance = TimeSpan.FromSeconds(1);
        //     Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

        //     EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
        //     Assert.NotNull(token);
        //     // Assert that the difference between the computed and actual expiry date is less than some tolerance.
        //     Assert.True((DateTimeOffset.Now.Add(tokenLifespan) - token.Expires).Duration() < tolerance);

        //     token.Expires = DateTimeOffset.Now.AddHours(-1);
        //     Assert.Null(await _service.GetEnrolmentCertificateAsync(token.Id));
        // }
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
