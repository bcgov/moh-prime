using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

using Prime;
using Prime.Models;
using Prime.ViewModels;
using Prime.Services.Rules;
using PrimeTests.Utils;
using FakeItEasy;

namespace PrimeTests.UnitTests
{
    public class MinorUpdateRulesTests
    {
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
            var t = A.Fake<IUserBoundModel>();
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

        private async Task AssertAllowableChanges(bool expected, Enrollee enrollee, EnrolleeProfileViewModel profile)
        {
            var rule = new AllowableChangesRule(profile);
            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            AssertNoReasons(enrollee);
        }

        [Fact]
        public async void testAllowableChangesRule_AllowedUpdates()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();

            profile.ContactEmail += "change";
            profile.ContactPhone += "change";
            profile.VoicePhone += "change";
            profile.VoiceExtension += "change";

            await AssertAllowableChanges(true, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_SimpleDissallowedChange_SimpleProperty()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();
            profile.PreferredFirstName = "BIG CHANGES";

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_SimpleDissallowedChange_RemoveChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();
            profile.MailingAddress = null;

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_SimpleDissallowedChange_PropertyOnChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();
            profile.MailingAddress.City = "Flavortown, USA";

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_SimpleDissallowedChange_AddChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();
            enrollee.MailingAddress = null;

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_Certifications()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            // New cert
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();
            profile.Certifications.Add(new Certification { CollegeCode = 1 });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit cert
            profile = enrollee.ToViewModel();
            profile.Certifications.First().LicenseNumber += "6";
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove cert
            profile = enrollee.ToViewModel();
            profile.Certifications = profile.Certifications.Skip(1).ToList();
            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(null, false)]
        public async void testAllowableChangesRule_Jobs(bool? isObo, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            // Set the enrollee's user class via the access term
            if (isObo.HasValue)
            {
                enrollee.AccessTerms = new[]
                {
                    new AccessTerm
                    {
                        AcceptedDate = DateTimeOffset.Now,
                        UserClause = new UserClause { EnrolleeClassification = isObo == true ? PrimeConstants.PRIME_OBO : PrimeConstants.PRIME_RU }
                    }
                };
            }
            else
            {
                enrollee.AccessTerms = new AccessTerm[] { };
            }

            // New job
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();
            profile.Jobs.Add(new Job { Title = "Snake sweater knitter" });
            await AssertAllowableChanges(expected, enrollee, profile);

            // Edit job
            profile = enrollee.ToViewModel();
            profile.Jobs.First().Title = "Bespoke lifehack crafter";
            await AssertAllowableChanges(expected, enrollee, profile);

            // Remove job
            profile = enrollee.ToViewModel();
            profile.Jobs = profile.Jobs.Skip(1).ToList();
            await AssertAllowableChanges(expected, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_EnrolleeOrganizationTypes()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            // New org
            EnrolleeProfileViewModel profile = enrollee.ToViewModel();
            profile.EnrolleeOrganizationTypes.Add(new EnrolleeOrganizationType { OrganizationTypeCode = 1 });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit org
            profile = enrollee.ToViewModel();
            profile.EnrolleeOrganizationTypes.First().OrganizationTypeCode++;
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove org
            profile = enrollee.ToViewModel();
            profile.EnrolleeOrganizationTypes = profile.EnrolleeOrganizationTypes.Skip(1).ToList();
            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public void testAllowableChangesRule_SPEC()
        {
            // Make sure there are no new types we don't know how to compare
            var knownTypes = new[]
            {
                typeof(int),
                typeof(string),
                typeof(bool?),
                typeof(MailingAddress),
                typeof(ICollection<Certification>),
                typeof(ICollection<Job>),
                typeof(ICollection<EnrolleeOrganizationType>),
            };

            var unknownTypes = typeof(EnrolleeProfileViewModel)
                .GetProperties()
                .Select(p => p.PropertyType)
                .Distinct()
                .Except(knownTypes);

            Assert.False(unknownTypes.Any(), $"At least one new type has been added to {nameof(EnrolleeProfileViewModel)}. Please update {nameof(AllowableChangesRule)} and/or {nameof(testAllowableChangesRule_SPEC)}");
        }
    }
}
