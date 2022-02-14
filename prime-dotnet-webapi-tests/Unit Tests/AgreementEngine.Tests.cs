using System.Collections.Generic;
using System.Linq;
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
        [MemberData(nameof(SingleCareSettingTestData))]
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

        public static IEnumerable<object[]> SingleCareSettingTestData()
        {
            // Pharmacists
            yield return new object[] { CareSettingType.CommunityPharmacy, 2, true, false, null };
            yield return new object[] { CareSettingType.CommunityPharmacy, 2, true, true, AgreementType.CommunityPharmacistTOA };
            yield return new object[] { CareSettingType.CommunityPharmacy, 2, false, true, AgreementType.PharmacyOboTOA };

            yield return new object[] { CareSettingType.CommunityPractice, 2, true, false, null };
            yield return new object[] { CareSettingType.CommunityPractice, 2, true, true, AgreementType.CommunityPharmacistTOA };
            yield return new object[] { CareSettingType.CommunityPractice, 2, false, true, AgreementType.PharmacyOboTOA };

            // Everyone else
            foreach (var collegeCode in Enumerable.Range(1, 18).Where(x => x != 2))
            {
                yield return new object[] { CareSettingType.CommunityPharmacy, collegeCode, true, false, null };
                yield return new object[] { CareSettingType.CommunityPharmacy, collegeCode, true, true, null };
                yield return new object[] { CareSettingType.CommunityPharmacy, collegeCode, false, true, null };

                yield return new object[] { CareSettingType.CommunityPractice, collegeCode, true, false, null };
                yield return new object[] { CareSettingType.CommunityPractice, collegeCode, true, true, AgreementType.RegulatedUserTOA };
                yield return new object[] { CareSettingType.CommunityPractice, collegeCode, false, true, AgreementType.OboTOA };
            }
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
