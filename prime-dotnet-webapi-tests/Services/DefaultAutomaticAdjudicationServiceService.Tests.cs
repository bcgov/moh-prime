using System;
using System.Linq;
using Xunit;

using Prime.Services;
using PrimeTests.Utils;
using Prime.Models;
using System.Collections.Generic;

namespace PrimeTests.Services
{
    public class DefaultAutomaticAdjudicationServiceServiceTests : BaseServiceTests<DefaultAutomaticAdjudicationServiceService>
    {
        private void UpdateCertifications(Enrolment enrolment, bool hasCertification = false)
        {
            // update certification information
            enrolment.HasCertification = hasCertification;
            if (hasCertification)
            {
                enrolment.Certifications = TestUtils.CertificationFaker.Generate(1);
            }
            else
            {
                enrolment.Certifications.Clear();
            }
        }

        private void UpdateDeviceProvider(Enrolment enrolment, bool provider = false, bool pumpProvider = false)
        {
            // update device provider information
            enrolment.IsDeviceProvider = provider;
            if (provider)
            {
                enrolment.DeviceProviderNumber = TestUtils.RandomDeviceProviderNumber();
                enrolment.IsInsulinPumpProvider = pumpProvider;
            }
            else
            {
                enrolment.DeviceProviderNumber = null;
                enrolment.IsInsulinPumpProvider = false;
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
        private void UpdateSelfDeclaration(Enrolment enrolment, SelfDeclaration bitmask = SelfDeclaration.NONE)
        {
            // update all self-declaration questions
            enrolment.HasConviction = (bitmask & SelfDeclaration.CONVICTION) == SelfDeclaration.CONVICTION;
            enrolment.HasDisciplinaryAction = (bitmask & SelfDeclaration.DISCIPLINARY) == SelfDeclaration.DISCIPLINARY;
            enrolment.HasPharmaNetSuspended = (bitmask & SelfDeclaration.PHARMANET_SUSPENDED) == SelfDeclaration.PHARMANET_SUSPENDED;
            enrolment.HasRegistrationSuspended = (bitmask & SelfDeclaration.REGISTRATION_SUSPENDED) == SelfDeclaration.REGISTRATION_SUSPENDED;
        }

        private void UpdateAddresses(Enrolment enrolment, bool outsideBC = false)
        {
            // update all addresses to 'BC', or a random province outside BC
            if (enrolment.Enrollee?.PhysicalAddress?.ProvinceCode != null)
            {
                enrolment.Enrollee.PhysicalAddress.ProvinceCode = outsideBC ? TestUtils.RandomProvince(new[] { Province.BRITISH_COLUMBIA_CODE }) : Province.BRITISH_COLUMBIA_CODE;
            }
            if (enrolment.Enrollee?.MailingAddress?.ProvinceCode != null)
            {
                enrolment.Enrollee.MailingAddress.ProvinceCode = outsideBC ? TestUtils.RandomProvince(new[] { Province.BRITISH_COLUMBIA_CODE }) : Province.BRITISH_COLUMBIA_CODE;
            }
        }
        
        private void AssertReasonCodes(ICollection<EnrolmentStatusReason> enrolmentStatusReasons, short[] expectedReasonCodes = null)
        {
            if (expectedReasonCodes == null)
            {
                Assert.Empty(enrolmentStatusReasons ?? new List<EnrolmentStatusReason>(0));
            }
            else
            {
                Assert.NotEmpty(enrolmentStatusReasons);
                Assert.Equal(expectedReasonCodes.Length, enrolmentStatusReasons.Count);
                foreach (var enrolmentStatusReason in enrolmentStatusReasons)
                {
                    Assert.Contains(enrolmentStatusReason.StatusReasonCode, expectedReasonCodes);
                }
            }
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment);
            this.UpdateSelfDeclaration(enrolment);

            // check that this qualifies for automatic adjudication
            Assert.True(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus?.EnrolmentStatusReasons);
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_All()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrolment, (SelfDeclaration.CONVICTION | SelfDeclaration.DISCIPLINARY | SelfDeclaration.PHARMANET_SUSPENDED | SelfDeclaration.REGISTRATION_SUSPENDED));
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.SELF_DECLARATION_CODE });
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Conviction()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrolment, (SelfDeclaration.CONVICTION));
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.SELF_DECLARATION_CODE });
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Disciplinary()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrolment, (SelfDeclaration.DISCIPLINARY));
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.SELF_DECLARATION_CODE });
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Pharmanet_Suspended()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrolment, (SelfDeclaration.PHARMANET_SUSPENDED));
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.SELF_DECLARATION_CODE });
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_SelfDeclaration_Registration_Suspended()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment);

            // check that this does not qualify for automatic adjudication
            this.UpdateSelfDeclaration(enrolment, (SelfDeclaration.REGISTRATION_SUSPENDED));
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.SELF_DECLARATION_CODE });
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_Address()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will not qualify for automatic adjudication
            this.UpdateAddresses(enrolment, true);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment);
            this.UpdateSelfDeclaration(enrolment);

            // check that this does not qualify for automatic adjudication
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.ADDRESS_CODE });
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_Certification()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will not qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment, true);
            this.UpdateDeviceProvider(enrolment);
            this.UpdateSelfDeclaration(enrolment);

            // check that this does not qualify for automatic adjudication
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.NAME_DISCREPANCY_CODE, StatusReason.NOT_IN_PHARMANET_CODE, StatusReason.LICENCE_CLASS_CODE });
        }

        [Fact]
        public async void testQualifiesForAutomaticAdjudication_PumpProvider()
        {
            // make sure there are no enrolments
            Assert.False(_dbContext.Enrolments.Any());
            await _dbContext.SaveChangesAsync();

            // create an enrolment directly to the context
            _dbContext.Enrolments.Add(TestUtils.EnrolmentFaker.Generate());
            await _dbContext.SaveChangesAsync();

            // pick of the enrolment to use
            var enrolment = _dbContext.Enrolments.FirstOrDefault();
            Assert.NotNull(enrolment);

            // change the values in the enrolment so that it will not qualify for automatic adjudication
            this.UpdateAddresses(enrolment);
            this.UpdateCertifications(enrolment);
            this.UpdateDeviceProvider(enrolment, true, true);
            this.UpdateSelfDeclaration(enrolment);

            // check that this does not qualify for automatic adjudication
            Assert.False(_service.QualifiesForAutomaticAdjudication(enrolment));
            AssertReasonCodes(enrolment.CurrentStatus.EnrolmentStatusReasons, new short[] { StatusReason.PUMP_PROVIDER_CODE });
        }
    }
}