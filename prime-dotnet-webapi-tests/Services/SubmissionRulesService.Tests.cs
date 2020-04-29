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
using PrimeTests.ModelFactories;

namespace PrimeTests.Services
{
    public class SubmissionRulesServiceTests : BaseServiceTests<SubmissionRulesService>
    {
        public SubmissionRulesServiceTests() : base(new object[] { new PharmanetApiServiceMock(), new AccessTermServiceMock() })
        { }

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
        private void UpdateSelfDeclaration(Enrollee enrollee, SelfDeclaration declarations)
        {
            enrollee.HasConviction = declarations.HasFlag(SelfDeclaration.CONVICTION);
            enrollee.HasDisciplinaryAction = declarations.HasFlag(SelfDeclaration.DISCIPLINARY);
            enrollee.HasPharmaNetSuspended = declarations.HasFlag(SelfDeclaration.PHARMANET_SUSPENDED);
            enrollee.HasRegistrationSuspended = declarations.HasFlag(SelfDeclaration.REGISTRATION_SUSPENDED);
        }

        private void UpdateAddresses(Enrollee enrollee, bool physInBc, bool? mailInBc)
        {
            string withRulesFor(bool inBc) => inBc ? null : "default,notBC";

            enrollee.PhysicalAddress = new PhysicalAddressFactory().Generate(withRulesFor(physInBc));

            if (mailInBc.HasValue)
            {
                enrollee.MailingAddress = new MailingAddressFactory().Generate(withRulesFor(mailInBc.Value));
            }
            else
            {
                enrollee.MailingAddress = null;
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
            Enrollee enrollee = new EnrolleeFactory().Generate();
            enrollee.Certifications = new List<Certification>();

            Assert.True(await _service.QualifiesForAutomaticAdjudicationAsync(enrollee));
            AssertReasons(enrollee.CurrentStatus?.EnrolmentStatusReasons);
        }
    }
}
