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

        [Theory]
        [InlineData()]
        public async void testCurrentToaRule()
        {
            throw new NotImplementedException();
            // Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            // UpdateSelfDeclaration(enrollee, declaration);
            // var rule = new SelfDeclarationRule();

            // Assert.Equal(expected, await rule.ProcessRule(enrollee));
            // if (expected)
            // {
            //     AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);
            // }
            // else
            // {
            //     AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.SelfDeclaration);
            // }
        }

        [Theory]
        [InlineData()]
        public async void testDateRule()
        {
            throw new NotImplementedException();
            // Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            // var rule = new AddressRule();

            // UpdateAddresses(enrollee);
            // Assert.True(await rule.ProcessRule(enrollee));
            // AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons);

            // UpdateAddresses(enrollee, false);
            // Assert.False(await rule.ProcessRule(enrollee));
            // AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReasonType.Address);
        }

        [Theory]
        [InlineData()]
        public async void testAllowableChangesRule()
        {
            throw new NotImplementedException();
            // Enrollee enrollee = TestUtils.EnrolleeFaker.Generate();
            // UpdateCertifications(enrollee, apiModes.Length);
            // var rule = new PharmanetValidationRule(new PharmanetApiServiceMock(enrollee, apiModes));

            // Assert.Equal(expected, await rule.ProcessRule(enrollee));
            // AssertReasons(enrollee.CurrentStatus.EnrolmentStatusReasons, expectedReasons);
        }
    }
}
