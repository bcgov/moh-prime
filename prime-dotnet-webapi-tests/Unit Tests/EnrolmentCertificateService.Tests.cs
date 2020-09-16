using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using System.Threading.Tasks;

namespace PrimeTests.UnitTests
{
    public class EnrolmentCertificateServiceTests : InMemoryDbTest
    {
        public EnrolmentCertificateService CreateService(
            IHttpContextAccessor httpContext = null)
        {
            return new EnrolmentCertificateService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }

        [Fact]
        public async void TestHappyPathCertificateAccess()
        {
            Enrollee enrollee = TestDb.Has(TestUtils.EnrolleeFaker.Generate());
            var service = CreateService();

            EnrolmentCertificateAccessToken token = await service.CreateCertificateAccessTokenAsync(enrollee.Id);
            Assert.NotNull(token);

            EnrolmentCertificate cert = await service.GetEnrolmentCertificateAsync(token.Id);
            Assert.NotNull(cert);
            Assert.Equal(enrollee.GPID, cert.GPID);
        }

        [Fact(Skip = "Max views are temporarily disabled in the app")]
        public async void TestMaxViews()
        {
            int tokenMaxViews = 3;
            Enrollee enrollee = TestDb.Has(TestUtils.EnrolleeFaker.Generate());
            var service = CreateService();

            EnrolmentCertificateAccessToken token = await service.CreateCertificateAccessTokenAsync(enrollee.Id);
            Assert.NotNull(token);
            Assert.Equal(0, token.ViewCount);

            for (int view = 1; view <= tokenMaxViews; view++)
            {
                Assert.NotNull(await service.GetEnrolmentCertificateAsync(token.Id));
                Assert.Equal(view, token.ViewCount);
            }

            Assert.Null(await service.GetEnrolmentCertificateAsync(token.Id));
        }

        [Fact]
        public async void TestExpiryDate()
        {
            TimeSpan tokenLifespan = TimeSpan.FromDays(7);
            TimeSpan tolerance = TimeSpan.FromSeconds(1);
            Enrollee enrollee = TestDb.Has(TestUtils.EnrolleeFaker.Generate());
            var service = CreateService();

            EnrolmentCertificateAccessToken token = await service.CreateCertificateAccessTokenAsync(enrollee.Id);
            Assert.NotNull(token);
            // Assert that the difference between the computed and actual expiry date is less than some tolerance.
            Assert.True((DateTimeOffset.Now.Add(tokenLifespan) - token.Expires).Duration() < tolerance);

            token.Expires = DateTimeOffset.Now.AddHours(-1);
            Assert.Null(await service.GetEnrolmentCertificateAsync(token.Id));
        }
    }
}
