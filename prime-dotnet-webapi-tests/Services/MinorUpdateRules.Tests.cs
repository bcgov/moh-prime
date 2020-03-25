using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

using Prime;
using Prime.Models;
using Prime.ViewModels;
using Prime.Services;
using Prime.Services.Rules;
using PrimeTests.Utils;
using PrimeTests.Mocks;

namespace PrimeTests.Services
{
    public class MinorUpdateRulesTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        public MinorUpdateRulesTests() : base() { }

        /// <summary>
        /// Minor update rules should not add any staus reasons
        /// </summary>
        private void AssertNoReasons(Enrollee enrollee)
        {
            var reasons = enrollee.CurrentStatus.EnrolmentStatusReasons ?? Enumerable.Empty<EnrolmentStatusReason>();
            Assert.Empty(reasons);
        }

        [Fact(Skip = "Awaiting test refactor")]
        public void testCurrentToaRule()
        {
            // TODO: implement with better control over test DB and access term service.
        }

        [Theory]
        [MemberData(nameof(DateRuleData))]
        public async void testDateRule(DateTimeOffset? expiryDate, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.AccessTerms = new[]
            {
                new AccessTerm
                {
                    ExpiryDate = expiryDate
                }
            };

            var rule = new DateRule();
            bool result = await rule.ProcessRule(enrollee);

            Assert.Equal(expected, result);
            AssertNoReasons(enrollee);
        }

        public static IEnumerable<object[]> DateRuleData()
        {
            return new[]
            {
                new object[] { null, true },
                new object[] { DateTimeOffset.Now.AddDays(91), true },
                new object[] { DateTimeOffset.Now.AddDays(89), false },
                new object[] { DateTimeOffset.Now.AddDays(-1), false },
            };
        }

        [Fact]
        public async void testAllowableChangesRule_AllowedUpdates()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeProfileViewModel profile = new EnrolleeProfileViewModel();
            enrollee.CopyPropertiesTo(profile);

            profile.ContactEmail += "change";
            profile.ContactPhone += "change";
            profile.VoicePhone += "change";
            profile.VoiceExtension += "change";

            var rule = new AllowableChangesRule(profile);

            Assert.True(await rule.ProcessRule(enrollee));
            AssertNoReasons(enrollee);
        }

        [Fact]
        public async void testAllowableChangesRule_OBOCanUpdateJobs()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            // Set the enrollee as OBO via the access term
            enrollee.AccessTerms = new[]
            {
                new AccessTerm
                {
                    AcceptedDate = DateTimeOffset.Now,
                    UserClause = new UserClause { EnrolleeClassification = PrimeConstants.PRIME_OBO  }
                }
            };
            EnrolleeProfileViewModel profile = new EnrolleeProfileViewModel();
            enrollee.CopyPropertiesTo(profile);

            // New job
            profile.Jobs.Add(new Job { Title = "Snake sweater knitter" });

            var rule = new AllowableChangesRule(profile);
            Assert.True(await rule.ProcessRule(enrollee));
            AssertNoReasons(enrollee);

            // Edit job
            profile.Jobs = enrollee.Jobs;
            profile.Jobs.First().Title = "Bespoke lifehack crafter";

            rule = new AllowableChangesRule(profile);
            Assert.True(await rule.ProcessRule(enrollee));
            AssertNoReasons(enrollee);
        }

        [Theory]
        [MemberData(nameof(DissallowedChangesData))]
        public async void testAllowableChangesRule_DissallowedChanges()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeProfileViewModel profile = new EnrolleeProfileViewModel();
            enrollee.CopyPropertiesTo(profile);

            // New job
            profile.Jobs.Add(new Job { Title = "Snake sweater knitter" });

            var rule = new AllowableChangesRule(profile);
            Assert.True(await rule.ProcessRule(enrollee));
            AssertNoReasons(enrollee);

            // Edit job
            profile.Jobs = enrollee.Jobs;
            profile.Jobs.First().Title = "Bespoke lifehack crafter";

            rule = new AllowableChangesRule(profile);
            Assert.True(await rule.ProcessRule(enrollee));
            AssertNoReasons(enrollee);
        }

        public static IEnumerable<object[]> DissallowedChangesData()
        {
            throw new NotImplementedException();
        }
    }
}
