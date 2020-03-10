using System;
using Xunit;

using Prime.Services;
using PrimeTests.Utils;
using Prime.Models;
using PrimeTests.Mocks;

namespace PrimeTests.Services
{
    public class EnrolmentCertificateServiceTests : BaseServiceTests<EnrolmentCertificateService>
    {
        public EnrolmentCertificateServiceTests() : base(new object[] {
            new AccessTermServiceMock(),
            new EnrolleeProfileVersionServiceMock()
        })
        { }

        [Fact]
        public async void testHappyPathCertificateAccess()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
            Assert.NotNull(token);

            EnrolmentCertificate cert = await _service.GetEnrolmentCertificateAsync(token.Id);
            Assert.NotNull(cert);
            Assert.Equal(enrollee.GPID, cert.GPID);
        }

        [Fact]
        public async void testMaxViews()
        {
            int tokenMaxViews = 3;
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
            Assert.NotNull(token);
            Assert.Equal(0, token.ViewCount);

            for (int view = 1; view <= tokenMaxViews; view++)
            {
                Assert.NotNull(await _service.GetEnrolmentCertificateAsync(token.Id));
                Assert.Equal(view, token.ViewCount);
            }

            Assert.Null(await _service.GetEnrolmentCertificateAsync(token.Id));
        }

        [Fact]
        public async void testExpiryDate()
        {
            TimeSpan tokenLifespan = TimeSpan.FromDays(7);
            TimeSpan tolerance = TimeSpan.FromSeconds(1);
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
            Assert.NotNull(token);
            // Assert that the difference between the computed and actual expiry date is less than some tolerance.
            Assert.True((DateTimeOffset.Now.Add(tokenLifespan) - token.Expires).Duration() < tolerance);

            token.Expires = DateTimeOffset.Now.AddHours(-1);
            Assert.Null(await _service.GetEnrolmentCertificateAsync(token.Id));
        }
    }
}
