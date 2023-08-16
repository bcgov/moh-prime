using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit;

using Prime.Models;
using Prime.ViewModels;
using Prime.Services.Rules;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;
using Prime.Configuration.Database;

namespace PrimeTests.UnitTests
{
    public class MinorUpdateRulesTests : InMemoryDbTest
    {
        /// <summary>
        /// Minor update rules should not add any staus reasons
        /// </summary>
        private void AssertNoReasons(Enrollee enrollee)
        {
            var reasons = enrollee.CurrentStatus.EnrolmentStatusReasons ?? Enumerable.Empty<EnrolmentStatusReason>();
            Assert.Empty(reasons);
        }

        [Theory]
        [InlineData(11, false)]
        [InlineData(12, false)]
        [InlineData(13, false)]
        [InlineData(14, false)]
        [InlineData(15, false)]
        [InlineData(16, false)]
        [InlineData(17, false)]
        [InlineData(19, false)]
        [InlineData(20, true)]
        [InlineData(21, true)]
        [InlineData(22, true)]
        [InlineData(23, true)]
        [InlineData(24, true)]
        [InlineData(25, false)]
        [InlineData(26, false)]
        [InlineData(27, true)]
        [InlineData(28, true)]
        [InlineData(29, true)]
        [InlineData(30, false)]
        [InlineData(31, true)]
        public async void TestCurrentToaRule(int agreementVersionId, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.Agreements = new[]
            {
                new Agreement
                {
                    AcceptedDate = DateTimeOffset.Now,
                    AgreementVersionId = agreementVersionId
                }
            };

            var agreementDtos = TestDb.AgreementVersions
                .Select(av => new
                {
                    av.Id,
                    av.AgreementType,
                    av.EffectiveDate
                })
                .ToList();

            var newestAgreementVersionIds = agreementDtos
                .GroupBy(av => av.AgreementType)
                .Select(group => group.OrderByDescending(av => av.EffectiveDate).First().Id)
                .ToList();

            var rule = new CurrentToaRule(newestAgreementVersionIds);
            bool result = await rule.ProcessRule(enrollee);

            Assert.Equal(expected, result);
            AssertNoReasons(enrollee);
        }

        [Fact(Skip = "Awaiting test refactor")]
        public void TestCorrectToaRule()
        {
            // Simple logic agreementEngine test cover most, would require pulling agreement engine out
        }

        [Theory]
        [MemberData(nameof(DateRuleData))]
        public async void TestDateRule(DateTimeOffset? expiryDate, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.Agreements = new[]
            {
                new Agreement
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
                new object[] { DateTimeOffset.Now.AddDays(31), true },
                new object[] { DateTimeOffset.Now.AddDays(29), false },
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
        public async void TestAllowableChangesRule_AllowedUpdates()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();

            profile.Email = profile.Email.Bump();
            profile.SmsPhone = profile.SmsPhone.Bump();
            profile.Phone = profile.Phone.Bump();
            profile.PhoneExtension = profile.PhoneExtension.Bump();

            await AssertAllowableChanges(true, enrollee, profile);
        }

        [Fact]
        public async void TestAllowableChangesRule_SimpleDissallowedChange_SimpleProperty()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.PreferredFirstName = profile.PreferredFirstName.Bump();

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void TestAllowableChangesRule_SimpleDissallowedChange_RemoveChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.Addresses = new EnrolleeAddressFactory(enrollee).Generate(1);
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.VerifiedAddress = null;

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void TestAllowableChangesRule_SimpleDissallowedChange_PropertyOnChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.Addresses = new EnrolleeAddressFactory(enrollee).Generate(1);
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.VerifiedAddress.City = profile.VerifiedAddress.City.Bump();

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void TestAllowableChangesRule_SimpleDissallowedChange_AddChildObject()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.MailingAddress = new MailingAddressFactory().Generate();

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void TestAllowableChangesRule_Certifications()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            // New cert
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.Certifications.Add(new Certification { CollegeCode = 1 });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit cert
            profile = enrollee.CopyToUpdateModel();
            profile.Certifications.First().LicenseNumber += "6";
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove cert
            profile = enrollee.CopyToUpdateModel();
            profile.Certifications = profile.Certifications.Skip(1).ToList();
            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public async void TestAllowableChangesRule_Jobs()
        {
            // Request OBO enrollee
            Enrollee enrollee = new EnrolleeFactory().Generate("default,obo");
            Assert.True(enrollee.Certifications.Count == 0);
            Assert.True(enrollee.OboSites.Count > 0);
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();

            // No changes should definitely pass Minor Update rule
            await AssertAllowableChanges(true, enrollee, profile);

            // Changing a job title should still pass Minor Update rule
            profile.OboSites.First().JobTitle = "BS Executive";
            await AssertAllowableChanges(true, enrollee, profile);
        }

        [Fact]
        public async void TestAllowableChangesRule_EnrolleeCareSettings()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();

            // New org
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.EnrolleeCareSettings.Add(new EnrolleeCareSetting { CareSettingCode = 1 });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit org
            profile = enrollee.CopyToUpdateModel();
            profile.EnrolleeCareSettings.First().CareSettingCode++;
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove org
            profile = enrollee.CopyToUpdateModel();
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
        public async void TestAllowableChangesRule_SelfDeclarations_AddToEmpty(SelfDeclarationType declarationType)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.SelfDeclarations = new List<SelfDeclaration>();

            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.SelfDeclarations.Add(new SelfDeclaration
            {
                SelfDeclarationTypeCode = declarationType.Code,
                SelfDeclarationDetails = "I did stuffs"
            });

            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Theory]
        [MemberData(nameof(SelfDeclarationData))]
        public async void TestAllowableChangesRule_SelfDeclarations_ModifySingle(SelfDeclarationType declarationType)
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
            EnrolleeUpdateModel profile = enrollee.CopyToUpdateModel();
            profile.SelfDeclarations.Add(new SelfDeclaration
            {
                SelfDeclarationTypeCode = (declaration.SelfDeclarationTypeCode % 4) + 1 // Pick a different code that exists
            });
            await AssertAllowableChanges(false, enrollee, profile);

            // Edit declaration
            profile = enrollee.CopyToUpdateModel();
            profile.SelfDeclarations.Single().SelfDeclarationDetails += "and another thing...";
            await AssertAllowableChanges(false, enrollee, profile);

            // Remove declaration
            profile = enrollee.CopyToUpdateModel();
            profile.SelfDeclarations.Clear();
            await AssertAllowableChanges(false, enrollee, profile);

            // Any document GUID in a self declaration update should be considered a change (even if it somehow matches an existing document)
            declaration.DocumentGuids = new[] { new Guid() };
            profile = enrollee.CopyToUpdateModel();
            await AssertAllowableChanges(false, enrollee, profile);
        }

        [Fact]
        public void TestAllowableChangesRule_SPEC()
        {
            // Make sure there are no new types we don't know how to compare
            var knownTypes = new[]
            {
                typeof(int),
                typeof(string),
                typeof(bool?),
                typeof(bool),
                typeof(DateTimeOffset?),
                typeof(VerifiedAddress),
                typeof(PhysicalAddress),
                typeof(MailingAddress),
                typeof(ICollection<Certification>),
                typeof(ICollection<OboSite>),
                typeof(ICollection<EnrolleeCareSetting>),
                typeof(ICollection<EnrolleeHealthAuthority>),
                typeof(ICollection<EnrolleeDeviceProvider>),
                typeof(ICollection<EnrolleeRemoteUser>),
                typeof(ICollection<RemoteAccessSite>),
                typeof(ICollection<RemoteAccessLocation>),
                typeof(ICollection<SelfDeclaration>),
                typeof(ICollection<RemoteAccessSiteUpdateModel>)
            };

            var unknownTypes = typeof(EnrolleeUpdateModel)
                .GetProperties()
                .Select(p => p.PropertyType)
                .Distinct()
                .Except(knownTypes);

            Assert.False(unknownTypes.Any(), $"At least one new type has been added to {nameof(EnrolleeUpdateModel)}. Please update {nameof(AllowableChangesRule)} and/or {nameof(TestAllowableChangesRule_SPEC)}");
        }
    }
}
