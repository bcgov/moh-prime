using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;
using AutoMapper;

using Prime.Models;
using Prime.Extensions;
using Prime.Services;
using Prime.HttpClients;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class EnrolleeAgreementServiceTests : InMemoryDbTest
    {
        private void AssertAgreementGeneration(Enrollee enrollee, int? expectedAgreementVersionId = null, string expectedLimitsClauseText = null)
        {
            Assert.Single(enrollee.Agreements);

            var agreement = enrollee.Agreements.Single();

            Assert.True(agreement.CreatedDate > DateTimeOffset.MinValue);
            Assert.Null(agreement.AcceptedDate);

            if (expectedAgreementVersionId.HasValue)
            {
                Assert.Equal(expectedAgreementVersionId.Value, agreement.AgreementVersionId);
            }

            if (expectedLimitsClauseText == null)
            {
                Assert.Null(agreement.LimitsConditionsClauseId);
            }
            else
            {
                Assert.Equal(expectedLimitsClauseText, agreement.LimitsConditionsClause.Text);
            }
        }

        [Fact]
        public async void TestCreateAgreement_ThrowsWhenNull()
        {
            // Arrange
            var service = CreateWithMockedDI<EnrolleeAgreementService>();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.Submissions = new[]
            {
                new Submission
                {
                    AgreementType = null,
                    CreatedDate = DateTimeOffset.Now,
                }
            };
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);

            // Act
            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateEnrolleeAgreementAsync(enrollee.Id));
        }

        [Theory]
        [MemberData(nameof(AgreementTypeData))]
        public async void TestCreateAgreement(AgreementType determinedType)
        {
            // Arrange
            var expectedAgreementId = 66; // arbitrary number
            var agreementService = A.Fake<IAgreementService>();
            A.CallTo(() => agreementService.GetLatestAgreementVersionIdOfTypeAsync(A<AgreementType>.That.IsEqualTo(determinedType))).Returns(expectedAgreementId);
            var service = CreateWithMockedDI<EnrolleeAgreementService>(agreementService);

            var enrollee = new EnrolleeFactory().Generate();
            enrollee.Submissions = new[]
            {
                new Submission
                {
                    AgreementType = determinedType,
                    CreatedDate = DateTimeOffset.Now,
                }
            };
            enrollee.AccessAgreementNote = null;
            TestDb.Has(enrollee);

            // Act
            await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }

        public static IEnumerable<object[]> AgreementTypeData()
        {
            foreach (var agreementType in AgreementGroup.Enrollee.AgreementTypes())
            {
                yield return new object[] { agreementType };
            }
        }

        [Fact]
        public async void TestCreateAgreement_WithLimitsClause()
        {
            // Arrange
            var service = CreateWithMockedDI<EnrolleeAgreementService>();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.Submissions = new[]
            {
                new Submission
                {
                    AgreementType = AgreementType.OboTOA,
                    CreatedDate = DateTimeOffset.Now,
                }
            };
            var noteText = "oh dear";
            enrollee.AccessAgreementNote = new AccessAgreementNote { Note = noteText };
            TestDb.Has(enrollee);

            // Act
            await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedLimitsClauseText: noteText);
        }
    }
}
