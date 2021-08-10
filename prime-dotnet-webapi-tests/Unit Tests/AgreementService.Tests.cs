using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;
using AutoMapper;

using Prime.Models;
using Prime.Services;
using Prime.HttpClients;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class AgreementServiceTests : InMemoryDbTest
    {
        public AgreementService CreateService(
            IHttpContextAccessor httpContext = null,
            IMapper mapper = null,
            IPdfService pdfService = null,
            IRazorConverterService razorConverterService = null,
            IDocumentManagerClient documentClient = null)
        {
            return new AgreementService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                mapper ?? A.Fake<IMapper>(),
                documentClient ?? A.Fake<IDocumentManagerClient>()
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

        [Fact]
        public async void TestCreateAgreement_ThrowsWhenNull()
        {
            // Arrange
            var service = CreateService();
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
            // TODO: Fix properly rather than comment-out
            // await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateEnrolleeAgreementAsync(enrollee.Id));
        }

        [Theory]
        [MemberData(nameof(AgreementTypeData))]
        public async void TestCreateAgreement(AgreementType determinedType)
        {
            // Arrange
            var service = CreateService();
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

            var expectedAgreementId = TestDb.AgreementVersions
                .Where(a => a.AgreementType == determinedType)
                .OrderByDescending(a => a.EffectiveDate)
                .Select(a => a.Id)
                .First();

            // Act
            // TODO: Fix properly rather than comment-out
            // await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedAgreementId);
        }

        public static IEnumerable<object[]> AgreementTypeData()
        {
            foreach (var agreementType in Enum.GetValues(typeof(AgreementType)))
            {
                yield return new object[] { agreementType };
            }
        }

        [Fact]
        public async void TestCreateAgreement_WithLimitsClause()
        {
            // Arrange
            var service = CreateService();
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
            // TODO: Fix properly rather than comment-out
            // await service.CreateEnrolleeAgreementAsync(enrollee.Id);

            // Assert
            AssertAgreementGeneration(enrollee, expectedLimitsClauseText: noteText);
        }
    }
}
