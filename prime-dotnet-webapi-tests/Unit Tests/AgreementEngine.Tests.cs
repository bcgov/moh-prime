using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;
using AutoMapper;

using Prime.Models;
using Prime.Engines;
using Prime.Services;
using Prime.HttpClients;
using PrimeTests.Utils;
using PrimeTests.ModelFactories;

namespace PrimeTests.UnitTests
{
    public class AgreementEngineTests : InMemoryDbTest
    {
        private Certification[] GenerateOneCert(Enrollee enrollee, bool regulatedUser)
        {
            var cert = regulatedUser
                ? new CertificationFactory(enrollee).Generate("default,licence.regulated")
                : new CertificationFactory(enrollee).Generate("default,licence.nonRegulated");

            cert.License = LicenseLookup.All.Single(l => l.Code == cert.LicenseCode);

            return new[] { cert };
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public void TestDetermineAgreementType_Obo(CareSettingType careSetting)
        {
            // Arrange
            var engine = new AgreementEngine();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeCareSettings.Single().CareSettingCode = (int)careSetting;
            enrollee.Certifications.Clear();

            // Act
            var determinedType = engine.DetermineAgreementType(enrollee);

            // Assert
            Assert.Equal(AgreementType.OboTOA, determinedType);
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public void TestDetermineAgreementType_LicencedObo(CareSettingType careSetting)
        {
            // Arrange
            var engine = new AgreementEngine();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeCareSettings.Single().CareSettingCode = (int)careSetting;
            enrollee.Certifications = GenerateOneCert(enrollee, false);

            // Act
            var determinedType = engine.DetermineAgreementType(enrollee);

            // Assert
            Assert.Equal(AgreementType.OboTOA, determinedType);
        }

        [Theory]
        [InlineData(CareSettingType.CommunityPractice)]
        [InlineData(CareSettingType.CommunityPharmacy)]
        public void TestDetermineAgreementType_RegulatedUser(CareSettingType careSetting)
        {
            // Arrange
            var engine = new AgreementEngine();
            var enrollee = new EnrolleeFactory().Generate();
            enrollee.EnrolleeCareSettings.Single().CareSettingCode = (int)careSetting;
            enrollee.Certifications = GenerateOneCert(enrollee, true);

            AgreementType expectedType;
            switch (careSetting)
            {
                case CareSettingType.CommunityPractice:
                    expectedType = AgreementType.RegulatedUserTOA;
                    break;
                case CareSettingType.CommunityPharmacy:
                    expectedType = AgreementType.CommunityPharmacistTOA;
                    break;
                default:
                    throw new Exception($"unknown care setting in {nameof(TestDetermineAgreementType_RegulatedUser)}");
            }

            // Act
            var determinedType = engine.DetermineAgreementType(enrollee);

            // Assert
            Assert.Equal(expectedType, determinedType);
        }
    }
}
