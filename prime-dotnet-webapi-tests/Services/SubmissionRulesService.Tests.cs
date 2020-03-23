using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

using Prime.Models;
using Prime.Services;
using Prime.Services.Rules;
using PrimeTests.Utils;
using PrimeTests.Mocks;
using static PrimeTests.Mocks.PharmanetApiServiceMock;

namespace PrimeTests.Services
{
    public class SubmissionRulesServiceTests : BaseServiceTests<SubmissionRulesService>
    {
        public SubmissionRulesServiceTests() : base(new object[] { new PharmanetApiServiceMock() })
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
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            QualifyEnrolleeForAuto(enrollee);

            Assert.True(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasons(enrollee.CurrentStatus?.EnrolmentStatusReasons);
        }

        [Theory]
        [InlineData(SelfDeclaration.NONE, true)]
        [InlineData(SelfDeclaration.CONVICTION, false)]
        [InlineData(SelfDeclaration.DISCIPLINARY, false)]
        [InlineData(SelfDeclaration.PHARMANET_SUSPENDED, false)]
        [InlineData(SelfDeclaration.REGISTRATION_SUSPENDED, false)]
        [InlineData((SelfDeclaration.CONVICTION | SelfDeclaration.DISCIPLINARY | SelfDeclaration.PHARMANET_SUSPENDED | SelfDeclaration.REGISTRATION_SUSPENDED), false)]
        public async void testSelfDeclarationRule(SelfDeclaration declaration, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            UpdateSelfDeclaration(enrollee, declaration);
            var rule = new SelfDeclarationRule();

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            if (expected)
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.SelfDeclaration);
            }
        }

        [Fact]
        public async void testAddressRule()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            var rule = new AddressRule();

            UpdateAddresses(enrollee);
            Assert.True(await rule.ProcessRule(enrollee));
            AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);

            UpdateAddresses(enrollee, false);
            Assert.False(await rule.ProcessRule(enrollee));
            AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.Address);
        }

        [Theory]
        [MemberData(nameof(CertificationRuleData))]
        public async void testCertificationRule(OperationMode[] apiModes, bool expected, StatusReasonType[] expectedReasons)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            UpdateCertifications(enrollee, apiModes.Length);
            var rule = new PharmanetValidationRule(new PharmanetApiServiceMock(enrollee, apiModes));

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, expectedReasons);
        }

        public static IEnumerable<object[]> CertificationRuleData()
        {
            var passingCases = new[]
            {
                new CertTestData(null, null),
                new CertTestData(new[] { OperationMode.MatchingRecord }, null),
                new CertTestData(new[] { OperationMode.MatchingRecord, OperationMode.MatchingRecord }, null),
            };

            var failingCases = new[]
            {
                // One bad cert
                new CertTestData(new[] { OperationMode.Error }, new[] { StatusReasonType.PharmanetError }),
                new CertTestData(new[] { OperationMode.NoRecord }, new[] { StatusReasonType.NotInPharmanet }),
                new CertTestData(new[] { OperationMode.NameDiscrepancy }, new[] { StatusReasonType.NameDiscrepancy }),
                new CertTestData(new[] { OperationMode.DateDiscrepancy }, new[] { StatusReasonType.BirthdateDiscrepancy }),
                new CertTestData(new[] { OperationMode.NotPracticing }, new[] { StatusReasonType.Practicing }),
                new CertTestData(
                    new[] { OperationMode.NameDiscrepancy | OperationMode.DateDiscrepancy },
                    new[] { StatusReasonType.NameDiscrepancy, StatusReasonType.BirthdateDiscrepancy }
                ),
                new CertTestData(
                    new[] { OperationMode.NameDiscrepancy | OperationMode.DateDiscrepancy | OperationMode.NotPracticing },
                    new[] { StatusReasonType.NameDiscrepancy, StatusReasonType.BirthdateDiscrepancy, StatusReasonType.Practicing }
                ),
                // One good cert, one bad
                new CertTestData(
                    new[] { OperationMode.MatchingRecord, OperationMode.NoRecord },
                    new[] { StatusReasonType.NotInPharmanet }
                ),
                new CertTestData(
                    new[] { OperationMode.MatchingRecord, OperationMode.NameDiscrepancy | OperationMode.DateDiscrepancy },
                    new[] { StatusReasonType.NameDiscrepancy, StatusReasonType.BirthdateDiscrepancy }
                ),
                // Two bad certs
                new CertTestData(
                    new[] { OperationMode.NoRecord, OperationMode.NotPracticing },
                    new[] { StatusReasonType.NotInPharmanet, StatusReasonType.Practicing }
                ),
                new CertTestData(
                    new[] { OperationMode.Error, OperationMode.NameDiscrepancy | OperationMode.DateDiscrepancy },
                    new[] { StatusReasonType.PharmanetError, StatusReasonType.NameDiscrepancy, StatusReasonType.BirthdateDiscrepancy }
                ),
                new CertTestData(
                    new[] { OperationMode.NameDiscrepancy, OperationMode.NameDiscrepancy },
                    new[] { StatusReasonType.NameDiscrepancy, StatusReasonType.NameDiscrepancy }
                ),
            };

            foreach (var data in passingCases)
            {
                yield return data.AsTestCase(true);
            }

            foreach (var data in failingCases)
            {
                yield return data.AsTestCase(false);
            }
        }

        private struct CertTestData
        {
            public OperationMode[] Modes;
            public StatusReasonType[] ExpectedReasons;

            public CertTestData(OperationMode[] modes, StatusReasonType[] expectedReasons)
            {
                Modes = modes ?? new OperationMode[0];
                ExpectedReasons = expectedReasons;
            }

            public object[] AsTestCase(bool passingCase)
            {
                return new Object[] { Modes, passingCase, ExpectedReasons };
            }
        }

        [Theory]
        [InlineData(false, false, true)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        [InlineData(true, true, false)]
        public async void testPumpProviderRule(bool isProvider, bool isPumpProvider, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            UpdateDeviceProvider(enrollee, isProvider, isPumpProvider);

            var rule = new DeviceProviderRule();

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            if (expected)
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.PumpProvider);
            }
        }

        [Theory]
        [InlineData(0, false, true)]
        // TODO Get Licence loaded onto Certificate
        // [InlineData(1, false, true)]
        // [InlineData(1, true, false)]
        public async void testLicenceClassRule(int licenseCount, bool isManual, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            UpdateCertifications(enrollee, licenseCount, isManual);

            var rule = new LicenceClassRule();

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            if (expected)
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.LicenceClass);
            }
        }
    }
}
