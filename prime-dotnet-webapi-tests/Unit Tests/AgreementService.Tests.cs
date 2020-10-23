using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class AgreementServiceTests : InMemoryDbTest
    {
        public AgreementService CreateService(
            IHttpContextAccessor httpContext = null,
            IRazorConverterService razorConverterService = null)
        {
            return new AgreementService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                razorConverterService ?? A.Fake<IRazorConverterService>()
            );
        }

        private void AssertAgreementGeneration(Enrollee enrollee, int? expectedAgreementVersionId = null, string expectedLimitsClauseText = null)
        {
            Assert.Single(enrollee.Agreements);

            var agreement = enrollee.Agreements.Single();

            Assert.True(agreement.CreatedDate > DateTimeOffset.MinValue);
            Assert.Null(agreement.AcceptedDate);

            if (expectedAgreementVersionId.HasValue)
            {
                Assert.Equal(agreement.AgreementVersionId, expectedAgreementVersionId.Value);
            }

            if (expectedLimitsClauseText == null)
            {
                Assert.Null(agreement.LimitsConditionsClauseId);
            }
            else
            {
                Assert.Equal(agreement.LimitsConditionsClause.Text, expectedLimitsClauseText);
            }
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public async void TestCreateAgreement_Obo(CareSettingType careSetting)
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeCareSettings.Single().CareSettingCode = (int)careSetting;
            enrollee.Certifications.Clear();
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);

            var expectedAgreementId = TestDb.AgreementVersions.GetNewestIdOfType<OboAgreement>();

            // Act
            await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public async void TestCreateAgreement_LicencedObo(CareSettingType careSetting)
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeCareSettings.Single().CareSettingCode = (int)careSetting;
            enrollee.Certifications = new CertificationFactory(enrollee).Generate(1, "default,licence.nonRegulated");
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);
            TestDb.Entry(enrollee.Certifications.Single()).Reference(c => c.License).Load();

            var expectedAgreementId = TestDb.AgreementVersions.GetNewestIdOfType<OboAgreement>();

            // Act
            await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public async void TestCreateAgreement_RegulatedUser(CareSettingType careSetting)
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeCareSettings.Single().CareSettingCode = (int)careSetting;
            enrollee.Certifications = new CertificationFactory(enrollee).Generate(1, "default,licence.regulated");
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);
            TestDb.Entry(enrollee.Certifications.Single()).Reference(c => c.License).Load();

            int expectedAgreementId = 0;
            switch (careSetting)
            {
                case CareSettingType.CommunityPractice:
                    expectedAgreementId = TestDb.AgreementVersions.GetNewestIdOfType<RegulatedUserAgreement>();
                    break;
                case CareSettingType.CommunityPharmacy:
                    expectedAgreementId = TestDb.AgreementVersions.GetNewestIdOfType<CommunityPharmacistAgreement>();
                    break;
            }

            // Act
            await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }

        [Fact]
        public async void TestCreateAgreement_WithLimitsClause()
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            var noteText = "oh dear";
            enrollee.AccessAgreementNote = new AccessAgreementNote { Note = noteText };
            TestDb.Has(enrollee);

            // Act
            await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedLimitsClauseText: noteText);
        }
    }

    internal static class AgreementVersionExtensions
    {
        public static int GetNewestIdOfType<T>(this IEnumerable<AgreementVersion> agreements) where T : AgreementVersion
        {
            return agreements
                .OfType<T>()
                .OrderByDescending(a => a.EffectiveDate)
                .Select(a => a.Id)
                .First();
        }
    }
}
