using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using Prime.Configuration.Agreements;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class AccessTermServiceTests : InMemoryDbTest
    {
        public AccessTermService CreateService(
            IHttpContextAccessor httpContext = null,
            IRazorConverterService razorConverterService = null)
        {
            return new AccessTermService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                razorConverterService ?? A.Fake<IRazorConverterService>()
            );
        }

        private void AssertAgreementGeneration(Enrollee enrollee, int expectedAgreementId, bool expectedHasLimitsClause = false)
        {
            Assert.Single(TestDb.AccessTerms.Where(at => at.EnrolleeId == enrollee.Id));

            var accessTerm = TestDb.AccessTerms.Single(at => at.EnrolleeId == enrollee.Id);

            Assert.True(accessTerm.CreatedDate > DateTimeOffset.MinValue);
            Assert.Null(accessTerm.AcceptedDate);
            Assert.Equal(accessTerm.AgreementId, expectedAgreementId);

            if (expectedHasLimitsClause)
            {
                Assert.NotNull(accessTerm.LimitsConditionsClauseId);

            }
            else
            {
                Assert.Null(accessTerm.LimitsConditionsClauseId);
            }
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public async void TestCreateAccessTerm_Obo(CareSettingType careSetting)
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeOrganizationTypes.Single().OrganizationTypeCode = (int)careSetting;
            enrollee.Certifications = new Certification[] { };

            var expectedAgreementId = TestDb.Agreements.GetNewestIdOfType<OboAgreement>();

            // Act
            await service.CreateEnrolleeAccessTermAsync(enrollee);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public async void TestCreateAccessTerm_LicencedObo(CareSettingType careSetting)
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeOrganizationTypes.Single().OrganizationTypeCode = (int)careSetting;
            enrollee.Certifications = new CertificationFactory(enrollee).Generate(1, "default,licence.nonRegulated");

            var expectedAgreementId = TestDb.Agreements.GetNewestIdOfType<OboAgreement>();

            // Act
            await service.CreateEnrolleeAccessTermAsync(enrollee);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public async void TestCreateAccessTerm_RegulatedUser(CareSettingType careSetting)
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeOrganizationTypes.Single().OrganizationTypeCode = (int)careSetting;
            enrollee.Certifications = new CertificationFactory(enrollee).Generate(1, "default,licence.regulated");

            int expectedAgreementId = 0;
            switch (careSetting)
            {
                case CareSettingType.CommunityPractice:
                    expectedAgreementId = TestDb.Agreements.GetNewestIdOfType<RegulatedUserAgreement>();
                    break;
                case CareSettingType.CommunityPharmacy:
                    expectedAgreementId = TestDb.Agreements.GetNewestIdOfType<CommunityPharmacistAgreement>();
                    break;
            }

            // Act
            await service.CreateEnrolleeAccessTermAsync(enrollee);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }
    }

    internal static class AgreementExtensions
    {
        public static int GetNewestIdOfType<T>(this IEnumerable<Agreement> agreements) where T : Agreement
        {
            return agreements
                .OfType<T>()
                .OrderByDescending(a => a.EffectiveDate)
                .Select(a => a.Id)
                .First();
        }
    }
}
