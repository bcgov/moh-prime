using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using PrimeTests.Mocks;
using static PrimeTests.Mocks.PharmanetApiServiceMock;

namespace PrimeTests.Services
{
    public class DefaultAutomaticAdjudicationServiceServiceTests : BaseServiceTests<DefaultAutomaticAdjudicationService>
    {
        public DefaultAutomaticAdjudicationServiceServiceTests() : base(new object[] { new PharmanetApiServiceMock() })
        { }

        private void QualifyEnrolleeForAuto(Enrollee enrollee)
        {
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);
            this.UpdateSelfDeclaration(enrollee);
        }

        private void UpdateCertifications(Enrollee enrollee, int certCount = 0)
        {
            if (certCount == 0)
            {
                enrollee.Certifications.Clear();
            }
            else
            {
                enrollee.Certifications = TestUtils.CertificationFaker.Generate(certCount);
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

        private void AssertReasonCodes(ICollection<EnrolmentStatusReason> enrolmentStatusReasons, params short[] expectedReasonCodes)
        {
            if (expectedReasonCodes == null || expectedReasonCodes.Length == 0)
            {
                Assert.Empty(enrolmentStatusReasons ?? new List<EnrolmentStatusReason>(0));
            }
            else
            {
                var actualCodes = enrolmentStatusReasons.Select(r => r.StatusReasonCode);
                Assert.Equal(expectedReasonCodes.OrderBy(c => c), actualCodes.OrderBy(c => c));
            }
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_NoCerts()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            QualifyEnrolleeForAuto(enrollee);

            Assert.True(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus?.EnrolmentStatusReasons);
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
            var rule = new DefaultAutomaticAdjudicationService.SelfDeclarationRule();

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            if (expected)
            {
                AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.SELF_DECLARATION_CODE);
            }
        }

        [Fact]
        public async void testAddressRule()
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            var rule = new DefaultAutomaticAdjudicationService.AddressRule();

            UpdateAddresses(enrollee);
            Assert.True(await rule.ProcessRule(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons);

            UpdateAddresses(enrollee, false);
            Assert.False(await rule.ProcessRule(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.ADDRESS_CODE);
        }

        [Theory]
        [MemberData(nameof(CertificationRuleData))]
        public async void testCertificationRule(OperationMode[] apiModes, bool expected, short[] expectedReasonCodes)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            UpdateCertifications(enrollee, apiModes.Length);
            var rule = new DefaultAutomaticAdjudicationService.PharmanetValidationRule(new PharmanetApiServiceMock(enrollee, apiModes));

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, expectedReasonCodes);
        }

        public static IEnumerable<object[]> CertificationRuleData()
        {
            yield return new object[] { new[] { OperationMode.MATCHING_RECORD }, true, null };
            yield return new object[] { new[] { OperationMode.MATCHING_RECORD, OperationMode.MATCHING_RECORD }, true, null };

            var failingCases = new[]
            {
                new[] { OperationMode.ERROR },
                new[] { OperationMode.NO_RECORD },
                new[] { OperationMode.NAME_DISCREPANCY },
                new[] { OperationMode.DATE_DISCREPANCY },
                new[] { OperationMode.NOT_PRACTICING },
                new[] { OperationMode.NAME_DISCREPANCY | OperationMode.DATE_DISCREPANCY },
                new[] { OperationMode.NAME_DISCREPANCY | OperationMode.DATE_DISCREPANCY | OperationMode.NOT_PRACTICING },
                new[] { OperationMode.MATCHING_RECORD, OperationMode.NO_RECORD },
                new[] { OperationMode.MATCHING_RECORD, OperationMode.NAME_DISCREPANCY | OperationMode.DATE_DISCREPANCY },
                new[] { OperationMode.NO_RECORD, OperationMode.NOT_PRACTICING },
                new[] { OperationMode.ERROR, OperationMode.NAME_DISCREPANCY | OperationMode.DATE_DISCREPANCY }
            };

            foreach (var modes in failingCases)
            {
                yield return new object[] { modes, false, GetExpectedReasonCodes(modes) };
            }
        }

        private static short[] GetExpectedReasonCodes(OperationMode[] modes)
        {
            var codes = new List<short>();
            foreach (var mode in modes)
            {
                if (mode == OperationMode.ERROR)
                {
                    codes.Add(StatusReason.PHARMANET_ERROR_CODE);
                    continue;
                }
                if (mode == OperationMode.MATCHING_RECORD)
                {
                    continue;
                }
                if (mode.HasFlag(OperationMode.NO_RECORD))
                {
                    codes.Add(StatusReason.NOT_IN_PHARMANET_CODE);
                    continue;
                }

                if (mode.HasFlag(OperationMode.NAME_DISCREPANCY))
                {
                    codes.Add(StatusReason.NAME_DISCREPANCY_CODE);
                }
                if (mode.HasFlag(OperationMode.DATE_DISCREPANCY))
                {
                    codes.Add(StatusReason.BIRTHDATE_DISCREPANCY_CODE);
                }
                if (mode.HasFlag(OperationMode.NOT_PRACTICING))
                {
                    codes.Add(StatusReason.PRACTICING_CODE);
                }
            }

            return codes.ToArray();
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

            var rule = new DefaultAutomaticAdjudicationService.DeviceProviderRule();

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            if (expected)
            {
                AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.PUMP_PROVIDER_CODE);
            }
        }
    }
}
