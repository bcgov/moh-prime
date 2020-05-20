using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class SubmissionRulesServiceTests : InMemoryDbTest
    {
        public SubmissionRulesService CreateService(
            IHttpContextAccessor httpContext = null,
            IPharmanetApiService pharmanetApiService = null,
            IAccessTermService accessTermService = null)
        {
            return new SubmissionRulesService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                pharmanetApiService ?? A.Fake<IPharmanetApiService>(),
                accessTermService ?? A.Fake<IAccessTermService>()
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

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_NoCerts()
        {
            Enrollee enrollee = TestDb.Has(new EnrolleeFactory().Generate());
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

//         Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeProfileViewModel profileUpdate);
//     }
