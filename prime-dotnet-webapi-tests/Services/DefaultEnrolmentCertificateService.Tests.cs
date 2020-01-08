using System.Linq;
using Xunit;

using Prime.Services;
using PrimeTests.Utils;
using Prime.Models;
using System.Collections.Generic;

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
    }
}
