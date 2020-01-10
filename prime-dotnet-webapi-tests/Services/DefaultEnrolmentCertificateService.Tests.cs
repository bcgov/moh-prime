using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

using Prime.Services;
using PrimeTests.Utils;
using Prime.Models;

namespace PrimeTests.Services
{
    public class DefaultEnrolmentCertificateServiceTests : BaseServiceTests<DefaultEnrolmentCertificateService>
    {
        public DefaultEnrolmentCertificateServiceTests() : base()
        { }

        [Fact]
        public async void testHappyPathCertificateAccess()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
            Assert.NotNull(token);

            EnrolmentCertificate cert = await _service.GetEnrolmentCertificateAsync(token.Id);
            Assert.NotNull(cert);
            Assert.Equal(enrollee.LicensePlate, cert.LicensePlate);
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
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            EnrolmentCertificateAccessToken token = await _service.CreateCertificateAccessTokenAsync(enrollee);
            Assert.NotNull(token);
            Assert.Equal(DateTime.Today.Add(tokenLifespan), token.Expires);

            token.Expires = DateTime.Today.AddDays(-1);
            Assert.Null(await _service.GetEnrolmentCertificateAsync(token.Id));
        }
    }
}
