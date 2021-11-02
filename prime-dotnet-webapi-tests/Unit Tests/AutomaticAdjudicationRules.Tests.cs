using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FakeItEasy;

using Prime.Models;
using Prime.Services;
using Prime.Services.Rules;
using PrimeTests.Utils;
using PrimeTests.HttpClientMocks;
using static PrimeTests.HttpClientMocks.CollegeLicenceClientMock;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class AutomaticAdjudicationRulesTests
    {
        private void UpdateCertifications(Enrollee enrollee, int certCount = 0, bool manual = false)
        {
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

        private void UpdateDeviceProvider(Enrollee enrollee, bool provider = false)
        {
            enrollee.DeviceProviderIdentifier = provider ? TestUtils.RandomDeviceProviderIdentifier() : null;
        }

        [Flags]
        public enum SelfDeclarationFlag
        {
            NONE = 0,
            CONVICTION = 1,
            DISCIPLINARY = 2,
            PHARMANET_SUSPENDED = 4,
            REGISTRATION_SUSPENDED = 8
        }

        private void UpdateSelfDeclaration(Enrollee enrollee, SelfDeclarationFlag declarations)
        {
            var updated = new List<SelfDeclaration>();
            if (declarations.HasFlag(SelfDeclarationFlag.CONVICTION))
            {
                updated.Add(GenerateSelfDeclaration(enrollee, 1));
            }
            if (declarations.HasFlag(SelfDeclarationFlag.DISCIPLINARY))
            {
                updated.Add(GenerateSelfDeclaration(enrollee, 3));
            }
            if (declarations.HasFlag(SelfDeclarationFlag.PHARMANET_SUSPENDED))
            {
                updated.Add(GenerateSelfDeclaration(enrollee, 2));
            }
            if (declarations.HasFlag(SelfDeclarationFlag.REGISTRATION_SUSPENDED))
            {
                updated.Add(GenerateSelfDeclaration(enrollee, 4));
            }
            enrollee.SelfDeclarations = updated;
        }

        private SelfDeclaration GenerateSelfDeclaration(Enrollee enrollee, int selfDeclarationTypeCode)
        {
            var decl = new SelfDeclaration
            {
                Enrollee = enrollee,
                EnrolleeId = enrollee.Id,
                SelfDeclarationTypeCode = selfDeclarationTypeCode,
                SelfDeclarationDetails = "",
            };
            return decl;
        }

        private void UpdateAddresses(Enrollee enrollee, AddressCase verified, AddressCase physical, AddressCase mailing)
        {
            string withRulesFor(AddressCase address) => address == AddressCase.OutsideBc ? "default,notBC" : null;

            enrollee.Addresses = new List<EnrolleeAddress>();

            enrollee.Addresses.Add(new EnrolleeAddress
            {
                Address = new VerifiedAddressFactory().Generate(withRulesFor(verified))
            });

            if (physical != AddressCase.Null)
            {
                enrollee.Addresses.Add(new EnrolleeAddress
                {
                    Address = new PhysicalAddressFactory().Generate(withRulesFor(physical))
                });
            }

            if (mailing != AddressCase.Null)
            {
                enrollee.Addresses.Add(new EnrolleeAddress
                {
                    Address = new MailingAddressFactory().Generate(withRulesFor(mailing))
                });
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

        [Theory]
        [InlineData(SelfDeclarationFlag.NONE, true)]
        [InlineData(SelfDeclarationFlag.CONVICTION, false)]
        [InlineData(SelfDeclarationFlag.DISCIPLINARY, false)]
        [InlineData(SelfDeclarationFlag.PHARMANET_SUSPENDED, false)]
        [InlineData(SelfDeclarationFlag.REGISTRATION_SUSPENDED, false)]
        [InlineData((SelfDeclarationFlag.CONVICTION | SelfDeclarationFlag.DISCIPLINARY | SelfDeclarationFlag.PHARMANET_SUSPENDED | SelfDeclarationFlag.REGISTRATION_SUSPENDED), false)]
        public async void TestSelfDeclarationRule(SelfDeclarationFlag declaration, bool expected)
        {
            Enrollee enrollee = new EnrolleeFactory().Generate();
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

        [Theory]
        [MemberData(nameof(AddressTestCases))]
        public async void TestAddressRule(AddressCase verified, AddressCase physical, AddressCase mailing)
        {
            var rule = new AddressRule();
            Enrollee enrollee = new EnrolleeFactory().Generate();
            UpdateAddresses(enrollee, verified, physical, mailing);
            var expected = new[] { verified, physical, mailing }.All(a => a != AddressCase.OutsideBc);

            var result = await rule.ProcessRule(enrollee);

            Assert.Equal(expected, result);
            if (expected)
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.Address);
            }
        }

        public enum AddressCase
        {
            Null,
            InBc,
            OutsideBc
        }
        public static IEnumerable<object[]> AddressTestCases()
        {
            foreach (var v in new[] { AddressCase.InBc, AddressCase.OutsideBc })
            {
                foreach (var p in new[] { AddressCase.Null, AddressCase.InBc, AddressCase.OutsideBc })
                {
                    foreach (var m in new[] { AddressCase.Null, AddressCase.InBc, AddressCase.OutsideBc })
                    {
                        yield return new object[] { v, p, m };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(CertificationRuleData))]
        public async void TestCertificationRule(OperationMode[] apiModes, bool expected, StatusReasonType[] expectedReasons)
        {
            Enrollee enrollee = new EnrolleeFactory().Generate();
            UpdateCertifications(enrollee, apiModes.Length);
            var rule = new PharmanetValidationRule(new CollegeLicenceClientMock(enrollee, apiModes), A.Fake<IBusinessEventService>());

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
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(true, true)]
        public async void TestDeviceProviderRule(bool isProvider, bool expected)
        {
            Enrollee enrollee = new EnrolleeFactory().Generate();
            UpdateDeviceProvider(enrollee, isProvider);

            var rule = new DeviceProviderRule();

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            if (expected)
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.DeviceProvider);
            }
        }

        [Theory]
        [InlineData(0, false, true)]
        // TODO Get Licence loaded onto Certificate
        // [InlineData(1, false, true)]
        // [InlineData(1, true, false)]
        public async void TestLicenceClassRule(int licenseCount, bool isManual, bool expected)
        {
            Enrollee enrollee = new EnrolleeFactory().Generate();
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

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, true)]
        public async void TestIdentityAssuranceLevelRule(int assuranceLevel, bool expected)
        {
            Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            enrollee.IdentityAssuranceLevel = assuranceLevel;

            var rule = new IdentityAssuranceLevelRule();

            Assert.Equal(expected, await rule.ProcessRule(enrollee));
            if (expected)
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);
            }
            else
            {
                AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.AssuranceLevel);
            }
        }
    }
}
