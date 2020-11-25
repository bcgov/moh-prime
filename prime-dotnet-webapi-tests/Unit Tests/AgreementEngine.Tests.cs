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
using Prime.DTOs.AgreementEngine;
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
        [InlineData(CareSettingType.CommunityPractice, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPharmacy, AgreementType.PharmacyOboTOA)]
        [InlineData(CareSettingType.HealthAuthority, AgreementType.OboTOA)]
        public void TestDetermineAgreementType_NoCerts_OneCareSetting(CareSettingType careSetting, AgreementType expectedType)
        {
            // Arrange
            var dto = new AgreementEngineDto
            {
                Certifications = new CertificationDto[] { },
                CareSettingCodes = new[] { (int)careSetting }
            };

            // Act
            var determinedType = AgreementEngine.DetermineAgreementType(dto);

            // Assert
            Assert.Equal(expectedType, determinedType);
        }

        [Fact(Skip = "Need more agreement engine tests now that it is more complicated")]
        public void WriteMoreTests() { }
    }
}
