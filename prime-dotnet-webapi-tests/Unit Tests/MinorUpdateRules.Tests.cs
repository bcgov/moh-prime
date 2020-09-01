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
using PrimeTests.ModelFactories;
using Prime.Configuration;

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
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.AccessTerms = new[]
            {
                new AccessTerm
                {
                    AcceptedDate = DateTimeOffset.Now,
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

        private async Task AssertAllowableChanges(bool expected, Enrollee enrollee, EnrolleeUpdateModel profile)
        {
            var rule = new AllowableChangesRule(profile);
            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            AssertNoReasons(enrollee);
        }

        [Fact]
        public async void testAllowableChangesRule_AllowedUpdates()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();

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
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.PreferredFirstName = "BIG CHANGES";

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_SimpleDissallowedChange_RemoveChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.MailingAddress = null;

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_SimpleDissallowedChange_PropertyOnChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.MailingAddress.City = "Flavortown, USA";

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_SimpleDissallowedChange_AddChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            enrollee.MailingAddress = null;

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_Certifications()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            // New cert
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.Certifications.Add(new Certification { CollegeCode = 1 });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit cert
            profile = enrollee.ToUpdateModel();
            profile.Certifications.First().LicenseNumber += "6";
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove cert
            profile = enrollee.ToUpdateModel();
            profile.Certifications = profile.Certifications.Skip(1).ToList();
            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async void testAllowableChangesRule_Jobs(bool isObo, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            if (isObo)
            {
                enrollee.Certifications.Clear();
            }
            else
            {
                enrollee.Certifications = new Certification[] { new Certification() };
            }

            // New job
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.Jobs.Add(new Job { Title = "Snake sweater knitter" });
            await AssertAllowableChanges(expected, enrollee, profile);

            // Edit job
            profile = enrollee.ToUpdateModel();
            profile.Jobs.First().Title = "Bespoke lifehack crafter";
            await AssertAllowableChanges(expected, enrollee, profile);

            // Remove job
            profile = enrollee.ToUpdateModel();
            profile.Jobs = profile.Jobs.Skip(1).ToList();
            await AssertAllowableChanges(expected, enrollee, profile);
        }

        [Fact]
        public async void testAllowableChangesRule_EnrolleeCareSettings()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            // New org
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.EnrolleeCareSettings.Add(new EnrolleeCareSetting { CareSettingCode = 1 });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit org
            profile = enrollee.ToUpdateModel();
            profile.EnrolleeCareSettings.First().CareSettingCode++;
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove org
            profile = enrollee.ToUpdateModel();
            profile.EnrolleeCareSettings = profile.EnrolleeCareSettings.Skip(1).ToList();
            await AssertAllowableChanges(false, enrollee, profile);
        }

        public static IEnumerable<object[]> SelfDeclarationData()
        {
            foreach (var declarationType in new SelfDeclarationTypeConfiguration().SeedData)
            {
                yield return new[] { declarationType };
            }
        }

        [Theory]
        [MemberData(nameof(SelfDeclarationData))]
        public async void testAllowableChangesRule_SelfDeclarations_AddToEmpty(SelfDeclarationType declarationType)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.SelfDeclarations = new List<SelfDeclaration>();

            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.SelfDeclarations.Add(new SelfDeclaration
            {
                SelfDeclarationTypeCode = declarationType.Code,
                SelfDeclarationDetails = "I did stuffs"
            });

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Theory]
        [MemberData(nameof(SelfDeclarationData))]
        public async void testAllowableChangesRule_SelfDeclarations_ModifySingle(SelfDeclarationType declarationType)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            var declaration = new SelfDeclaration
            {
                SelfDeclarationType = declarationType,
                SelfDeclarationTypeCode = declarationType.Code,
                SelfDeclarationDetails = "I did a thing"
            };
            enrollee.SelfDeclarations = new[] { declaration };

            // New declaration
            EnrolleeUpdateModel profile = enrollee.ToUpdateModel();
            profile.SelfDeclarations.Add(new SelfDeclaration
            {
                SelfDeclarationTypeCode = (declaration.SelfDeclarationTypeCode % 4) + 1 // Pick a different code that exists
            });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit declaration
            profile = enrollee.ToUpdateModel();
            profile.SelfDeclarations.Single().SelfDeclarationDetails += "and another thing...";
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove declaration
            profile = enrollee.ToUpdateModel();
            profile.SelfDeclarations.Clear();
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
                typeof(bool),
                typeof(MailingAddress),
                typeof(ICollection<Certification>),
                typeof(ICollection<Job>),
                typeof(ICollection<EnrolleeCareSetting>),
                typeof(ICollection<SelfDeclaration>),
            };

            var unknownTypes = typeof(EnrolleeUpdateModel)
                .GetProperties()
                .Select(p => p.PropertyType)
                .Distinct()
                .Except(knownTypes);

            Assert.False(unknownTypes.Any(), $"At least one new type has been added to {nameof(EnrolleeUpdateModel)}. Please update {nameof(AllowableChangesRule)} and/or {nameof(testAllowableChangesRule_SPEC)}");
        }
    }
}
