using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime.Models;
using Prime.Services;
using Prime.Services.Clients;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class SubmissionRulesServiceTests : InMemoryDbTest
    {
        public SubmissionRulesService CreateService(
            IHttpContextAccessor httpContext = null,
            ICollegeLicenceClient collegeLicenceClient = null)
        {
            return new SubmissionRulesService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                collegeLicenceClient ?? A.Fake<ICollegeLicenceClient>()
            );
        }

        private void AssertReasons(ICollection<EnrolmentStatusReason> enrolmentStatusReasons, params StatusReasonType[] expectedReasons)
        {
            if (expectedReasons == null || expectedReasons.Length == 0)
            {
                Assert.Empty(enrolmentStatusReasons ?? new List<EnrolmentStatusReason>(0));
            }
            else
            {
                var actualCodes = enrolmentStatusReasons.Select(r => r.StatusReasonCode);
                Assert.Equal(expectedReasons.Cast<int>().OrderBy(c => c), actualCodes.OrderBy(c => c));
            }
        }

        [Fact(Skip = "Awaiting test refactor")]
        public async void testQualifiesForAutomaticAdjudication_NoCerts()
        {
            Enrollee enrollee = TestDb.HasAnEnrollee();
            enrollee.Certifications = new List<Certification>();
            var service = CreateService();

            Assert.True(await service.QualifiesForAutomaticAdjudicationAsync(enrollee));
            AssertReasons(enrollee.CurrentStatus?.EnrolmentStatusReasons);
        }
    }
}

//     public interface ISubmissionRulesService
//     {
//         Task<bool> QualifiesForAutomaticAdjudicationAsync(Enrollee enrollee);

//         Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeUpdateModel profileUpdate);
//     }
