
using Xunit;

using Prime.Models;
using Prime.Engines;
using Prime.DTOs.AgreementEngine;
using PrimeTests.Utils;

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

        [Fact]
        public void TestDetermineAgreementType_NoCerts_MultipleCareSetting()
        {
            // Arrange
            var noPharm = new AgreementEngineDto
            {
                Certifications = new CertificationDto[] { },
                CareSettingCodes = new[] { (int)CareSettingType.CommunityPractice, (int)CareSettingType.DeviceProvider }
            };
            // Act
            var determinedType = AgreementEngine.DetermineAgreementType(noPharm);
            // Assert
            Assert.Equal(AgreementType.OboTOA, determinedType);

            // Arrange
            var withPharm = new AgreementEngineDto
            {
                Certifications = new CertificationDto[] { },
                CareSettingCodes = new[] { (int)CareSettingType.CommunityPractice, (int)CareSettingType.CommunityPharmacy }
            };
            // Act
            determinedType = AgreementEngine.DetermineAgreementType(withPharm);
            // Assert
            Assert.Null(determinedType);
        }

        [Theory]
        // Community Pharmacy
        [InlineData(CareSettingType.CommunityPharmacy, 1, true, true, null)]
        [InlineData(CareSettingType.CommunityPharmacy, 1, true, false, null)]
        [InlineData(CareSettingType.CommunityPharmacy, 1, false, true, null)]
        [InlineData(CareSettingType.CommunityPharmacy, 2, true, true, AgreementType.CommunityPharmacistTOA)]
        [InlineData(CareSettingType.CommunityPharmacy, 2, false, true, AgreementType.PharmacyOboTOA)]
        // Community Practice
        [InlineData(CareSettingType.CommunityPractice, 1, true, false, null)]
        [InlineData(CareSettingType.CommunityPractice, 1, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 1, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 2, true, true, AgreementType.CommunityPharmacistTOA)]
        [InlineData(CareSettingType.CommunityPractice, 2, false, true, AgreementType.PharmacyOboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 3, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 3, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 4, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 4, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 5, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 5, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 6, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 6, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 7, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 7, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 8, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 8, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 9, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 9, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 10, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 10, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 11, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 11, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 12, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 12, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 13, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 13, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 14, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 14, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 15, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 15, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 16, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 16, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 17, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 17, false, true, AgreementType.OboTOA)]
        [InlineData(CareSettingType.CommunityPractice, 18, true, true, AgreementType.RegulatedUserTOA)]
        [InlineData(CareSettingType.CommunityPractice, 18, false, true, AgreementType.OboTOA)]
        public void TestDetermineAgreementType_SingleCert_OneCareSetting(CareSettingType careSettingCode, int collegeCode, bool namedInImReg, bool licensedToProvideCare, AgreementType? expectedAgreementType)
        {
            var dto = new AgreementEngineDto
            {
                CareSettingCodes = new[] { (int)careSettingCode },
                Certifications = new[]
                {
                    new CertificationDto {
                        CollegeCode = collegeCode,
                        License = new License {
                            LicenseDetails = new [] {
                                new LicenseDetail {
                                    NamedInImReg = namedInImReg,
                                    LicensedToProvideCare = licensedToProvideCare
                                }
                            }
                        }
                    }
                }
            };
            var actual = AgreementEngine.DetermineAgreementType(dto);

            Assert.Equal(expectedAgreementType, actual);
        }

        [Fact]
        public void TestDetermineAgreementType_MultipleCerts_OneCareSetting()
        {
            // Arrange
            var dto = new AgreementEngineDto
            {
                CareSettingCodes = new[] { (int)CareSettingType.CommunityPractice },
                Certifications = new[]
                {
                    new CertificationDto {
                        CollegeCode = 1,
                        License = new License {
                            LicenseDetails = new [] {
                                new LicenseDetail {
                                    NamedInImReg = true,
                                    LicensedToProvideCare = true
                                }
                            }
                        }
                    },
                    new CertificationDto {
                        CollegeCode = 1,
                        License = new License {
                            LicenseDetails = new [] {
                                new LicenseDetail {
                                    NamedInImReg = true,
                                    LicensedToProvideCare = true
                                }
                            }
                        }
                    }
                }
            };
            // Act
            var determinedType = AgreementEngine.DetermineAgreementType(dto);
            // Assert
            Assert.Null(determinedType);
        }
    }
}
