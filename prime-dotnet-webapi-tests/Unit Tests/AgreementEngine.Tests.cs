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

        [Fact(Skip = "Need more agreement engine tests now that we have the fuller story")]
        public void WriteMoreTests() { }
    }
}
