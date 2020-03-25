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
    public class MinorUpdateRulesTests : BaseServiceTests<SubmissionRulesService>
    {
        public MinorUpdateRulesTests() : base(new object[] { new PharmanetApiServiceMock(), new AccessTermServiceMock() })
        { }

        private void QualifyEnrolleeForAuto(Enrollee enrollee)
        {
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);
            this.UpdateSelfDeclaration(enrollee);
        }

        private void UpdateCertifications(Enrollee enrollee, int certCount = 0, bool manual = false)
        {
            if (certCount == 0)
            {
                enrollee.Certifications.Clear();
            }
            if (manual)
            {
                enrollee.Certifications = TestUtils.ManualCertificationFaker.Generate(certCount);
            }
            else
            {
                enrollee.Certifications = TestUtils.CertificationFaker.Generate(certCount);
            }

            foreach (var cert in enrollee.Certifications)
            {
                cert.License = new License
                {
                    Code = cert.LicenseCode,
                    Weight = 1,
                    Manual = false,
                    Validate = true
                };
            }
        }

        private void UpdateDeviceProvider(Enrollee enrollee, bool provider = false, bool pumpProvider = false)
        {
            enrollee.DeviceProviderNumber = provider ? TestUtils.RandomDeviceProviderNumber() : null;
            enrollee.IsInsulinPumpProvider = pumpProvider;
        }

        [Flags]
        public enum SelfDeclaration
        {
            NONE = 0,
            CONVICTION = 1,
            DISCIPLINARY = 2,
            PHARMANET_SUSPENDED = 4,
            REGISTRATION_SUSPENDED = 8
        }
        private void UpdateSelfDeclaration(Enrollee enrollee, SelfDeclaration declarations = SelfDeclaration.NONE)
        {
            enrollee.HasConviction = declarations.HasFlag(SelfDeclaration.CONVICTION);
            enrollee.HasDisciplinaryAction = declarations.HasFlag(SelfDeclaration.DISCIPLINARY);
            enrollee.HasPharmaNetSuspended = declarations.HasFlag(SelfDeclaration.PHARMANET_SUSPENDED);
            enrollee.HasRegistrationSuspended = declarations.HasFlag(SelfDeclaration.REGISTRATION_SUSPENDED);
        }

        private void UpdateAddresses(Enrollee enrollee, bool inBC = true)
        {
            // update all addresses to 'BC', or a random province outside BC
            if (enrollee.PhysicalAddress != null)
            {
                enrollee.PhysicalAddress.ProvinceCode = inBC ? Province.BRITISH_COLUMBIA_CODE : TestUtils.RandomProvinceCode(Province.BRITISH_COLUMBIA_CODE);
            }
            if (enrollee.MailingAddress != null)
            {
                enrollee.MailingAddress.ProvinceCode = inBC ? Province.BRITISH_COLUMBIA_CODE : TestUtils.RandomProvinceCode(Province.BRITISH_COLUMBIA_CODE);
            }
        }

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
            // TODO: implement with better control over test DB.
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
    }
}
