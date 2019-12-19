using System.Linq;
using System.Collections.Generic;
using Xunit;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using PrimeTests.Mocks;

namespace PrimeTests.Services
{
    public class DefaultAutomaticAdjudicationServiceServiceTests : BaseServiceTests<DefaultAutomaticAdjudicationService>
    {
        public DefaultAutomaticAdjudicationServiceServiceTests() : base(new object[] { new PharmanetApiServiceMock() })
        { }

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
            if (provider)
            {
                enrollee.DeviceProviderNumber = TestUtils.RandomDeviceProviderNumber();
                enrollee.IsInsulinPumpProvider = pumpProvider;
            }
            else
            {
                enrollee.DeviceProviderNumber = null;
                enrollee.IsInsulinPumpProvider = false;
            }
        }

        // bitmask for setting the values
        // 0 - all false
        // 1 - conviction
        // 2 - disciplinary
        // 4 - pharmanet suspended
        // 8 - registration suspended
        private enum SelfDeclaration
        {
            NONE = 0,
            CONVICTION = 1,
            DISCIPLINARY = 2,
            PHARMANET_SUSPENDED = 4,
            REGISTRATION_SUSPENDED = 8
        }
        private void UpdateSelfDeclaration(Enrollee enrollee, SelfDeclaration bitmask = SelfDeclaration.NONE)
        {
            // update all self-declaration questions
            enrollee.HasConviction = (bitmask & SelfDeclaration.CONVICTION) == SelfDeclaration.CONVICTION;
            enrollee.HasDisciplinaryAction = (bitmask & SelfDeclaration.DISCIPLINARY) == SelfDeclaration.DISCIPLINARY;
            enrollee.HasPharmaNetSuspended = (bitmask & SelfDeclaration.PHARMANET_SUSPENDED) == SelfDeclaration.PHARMANET_SUSPENDED;
            enrollee.HasRegistrationSuspended = (bitmask & SelfDeclaration.REGISTRATION_SUSPENDED) == SelfDeclaration.REGISTRATION_SUSPENDED;
        }

        private void UpdateAddresses(Enrollee enrollee, bool outsideBC = false)
        {
            // update all addresses to 'BC', or a random province outside BC
            if (enrollee.PhysicalAddress?.ProvinceCode != null)
            {
                enrollee.PhysicalAddress.ProvinceCode = outsideBC ? TestUtils.RandomProvince(new[] { Province.BRITISH_COLUMBIA_CODE }) : Province.BRITISH_COLUMBIA_CODE;
            }
            if (enrollee.MailingAddress?.ProvinceCode != null)
            {
                enrollee.MailingAddress.ProvinceCode = outsideBC ? TestUtils.RandomProvince(new[] { Province.BRITISH_COLUMBIA_CODE }) : Province.BRITISH_COLUMBIA_CODE;
            }
        }

        private void AssertReasonCodes(ICollection<EnrolmentStatusReason> enrolmentStatusReasons, params short[] expectedReasonCodes)
        {
            if (expectedReasonCodes.Length == 0)
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
        public async void testQualifiesForAutomaticAdjudication()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);
            this.UpdateSelfDeclaration(enrollee);

            // check that this qualifies for automatic adjudication
            Assert.True(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus?.EnrolmentStatusReasons);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_All()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrollee, (SelfDeclaration.CONVICTION | SelfDeclaration.DISCIPLINARY | SelfDeclaration.PHARMANET_SUSPENDED | SelfDeclaration.REGISTRATION_SUSPENDED));
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.SELF_DECLARATION_CODE);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Conviction()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrollee, (SelfDeclaration.CONVICTION));
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.SELF_DECLARATION_CODE);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Disciplinary()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrollee, (SelfDeclaration.DISCIPLINARY));
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.SELF_DECLARATION_CODE);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Pharmanet_Suspended()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrollee, (SelfDeclaration.PHARMANET_SUSPENDED));
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.SELF_DECLARATION_CODE);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Registration_Suspended()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrollee, (SelfDeclaration.REGISTRATION_SUSPENDED));
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.SELF_DECLARATION_CODE);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_Address()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will not qualify for automatic adjudication
            this.UpdateAddresses(enrollee, true);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee);
            this.UpdateSelfDeclaration(enrollee);

            // check that this does not qualify for automatic adjudication
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.ADDRESS_CODE);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_Certification()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will not qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee, 1);
            this.UpdateDeviceProvider(enrollee);
            this.UpdateSelfDeclaration(enrollee);

            // check that this does not qualify for automatic adjudication
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.NOT_IN_PHARMANET_CODE);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_PumpProvider()
        {
            // make sure there are no enrollees
            Assert.False(_dbContext.Enrollees.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrollee directly to the context
            _dbContext.Enrollees.Add(TestUtils.EnrolleeFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrollee to use
            var enrollee = _dbContext.Enrollees.FirstOrDefault();
            Assert.NotNull(enrollee);

            // change the values in the enrollee so that it will not qualify for automatic adjudication
            this.UpdateAddresses(enrollee);
            this.UpdateCertifications(enrollee);
            this.UpdateDeviceProvider(enrollee, true, true);
            this.UpdateSelfDeclaration(enrollee);

            // check that this does not qualify for automatic adjudication
            Assert.False(await _service.QualifiesForAutomaticAdjudication(enrollee));
            AssertReasonCodes(enrollee.CurrentStatus.EnrolmentStatusReasons, StatusReason.PUMP_PROVIDER_CODE);
        }
    }
}
