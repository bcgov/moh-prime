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

        private void AssertAgreementGeneration(Enrollee enrollee, int? expectedAgreementId = null, string expectedLimitsClauseText = null)
        {
            Assert.Single(enrollee.AccessTerms);

            var accessTerm = enrollee.AccessTerms.Single();

            Assert.True(accessTerm.CreatedDate > DateTimeOffset.MinValue);
            Assert.Null(accessTerm.AcceptedDate);

            if (expectedAgreementId.HasValue)
            {
                Assert.Equal(accessTerm.AgreementId, expectedAgreementId.Value);
            }

            if (expectedLimitsClauseText == null)
            {
                Assert.Null(accessTerm.LimitsConditionsClauseId);
            }
            else
            {
                Assert.Equal(accessTerm.LimitsConditionsClause.Text, expectedLimitsClauseText);
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
            enrollee.Certifications.Clear();
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);

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
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);
            TestDb.Entry(enrollee.Certifications.Single()).Reference(c => c.License).Load();

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
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);
            TestDb.Entry(enrollee.Certifications.Single()).Reference(c => c.License).Load();

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

        [Fact]
        public async void TestCreateAccessTerm_WithLimitsClause()
        {
            // Arrange
            var service = CreateService();
            var enrollee = new EnrolleeFactory().Generate();
            var noteText = "oh dear";
            enrollee.AccessAgreementNote = new AccessAgreementNote { Note = noteText };
            TestDb.Has(enrollee);

            // Act
            await service.CreateEnrolleeAccessTermAsync(enrollee);

            // Assert
            AssertAgreementGeneration(enrollee, expectedLimitsClauseText: noteText);
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
