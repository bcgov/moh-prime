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
            IHttpContextAccessor httpContext = null,
            IAccessTermService accessTermService = null,
            IEnrolleeProfileVersionService enroleeProfileVersionService = null)
        {
            return new EnrolmentCertificateService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                accessTermService ?? A.Fake<IAccessTermService>(),
                enroleeProfileVersionService ?? A.Fake<IEnrolleeProfileVersionService>()
            );
        }

        public EnrolmentCertificateService CreateWithMocks(Enrollee enrollee)
        {
            var accessTermServiceFake = A.Fake<IAccessTermService>();
            A.CallTo(() => accessTermServiceFake.GetMostRecentAcceptedEnrolleesAccessTermAsync(enrollee.Id))
                .Returns(new AccessTerm
                {
                    AcceptedDate = DateTimeOffset.Now
                });

            var versionServiceFake = A.Fake<IEnrolleeProfileVersionService>();
            A.CallTo(() => versionServiceFake.GetEnrolleeProfileVersionBeforeDateAsync(enrollee.Id, A<DateTimeOffset>.Ignored))
                .Returns(new EnrolleeProfileVersion
                {
                    ProfileSnapshot = JObject.FromObject(enrollee),
                });


            return CreateService(null, accessTermServiceFake, versionServiceFake);
        }

        [Fact]
        public async void testHappyPathCertificateAccess()
        {
            Enrollee enrollee = TestDb.Has(TestUtils.EnrolleeFaker.Generate());
            var service = CreateWithMocks(enrollee);

            EnrolmentCertificateAccessToken token = await service.CreateCertificateAccessTokenAsync(enrollee);
            Assert.NotNull(token);

            EnrolmentCertificate cert = await service.GetEnrolmentCertificateAsync(token.Id);
            Assert.NotNull(cert);
            Assert.Equal(enrollee.GPID, cert.GPID);
        }

        [Fact(Skip = "Max views are temporarily disabled in the app")]
        public async void testMaxViews()
        {
            int tokenMaxViews = 3;
            Enrollee enrollee = TestDb.Has(TestUtils.EnrolleeFaker.Generate());
            var service = CreateService();

            EnrolmentCertificateAccessToken token = await service.CreateCertificateAccessTokenAsync(enrollee);
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
        public async void testExpiryDate()
        {
            TimeSpan tokenLifespan = TimeSpan.FromDays(7);
            TimeSpan tolerance = TimeSpan.FromSeconds(1);
            Enrollee enrollee = TestDb.Has(TestUtils.EnrolleeFaker.Generate());
            var service = CreateWithMocks(enrollee);

            EnrolmentCertificateAccessToken token = await service.CreateCertificateAccessTokenAsync(enrollee);
            Assert.NotNull(token);
            // Assert that the difference between the computed and actual expiry date is less than some tolerance.
            Assert.True((DateTimeOffset.Now.Add(tokenLifespan) - token.Expires).Duration() < tolerance);

            token.Expires = DateTimeOffset.Now.AddHours(-1);
            Assert.Null(await service.GetEnrolmentCertificateAsync(token.Id));
        }
    }
}
