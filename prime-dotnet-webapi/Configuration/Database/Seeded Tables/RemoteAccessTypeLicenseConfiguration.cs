using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class RemoteAccessTypeLicenseConfiguration : SeededTable<RemoteAccessTypeLicense>
    {
        public static readonly DateTime InitialSeedingDate = new(2025, 2, 19, 8, 0, 0, DateTimeKind.Utc);
        public override IEnumerable<RemoteAccessTypeLicense> SeedData
        {
            get
            {
                return new[] {
                    // Normal Remote Access for select physician and nurse practitioner license classes
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 1, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 2, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 3, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 4, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 5, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 6, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 8, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 9, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 10, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 12, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 14, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 15, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 17, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 18, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 19, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 47, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 51, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 87, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 88, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 89, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },

                    // FNHA for select physician and nurse practitioner license classes
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 1, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 2, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 3, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 4, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 5, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 6, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 8, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 9, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 10, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 12, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 14, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 15, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 17, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 18, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 19, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 47, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 51, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 87, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 88, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 89, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    //FNHA for additional nurse license classes
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 35, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 32, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 41, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 175, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 176, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 39, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 45, CreatedTimeStamp = InitialSeedingDate, UpdatedTimeStamp = InitialSeedingDate },
                };
            }
        }

        public override void Configure(EntityTypeBuilder<RemoteAccessTypeLicense> builder)
        {
            builder.HasKey(rl => new { rl.RemoteAccessTypeCode, rl.LicenseCode });

            builder.HasData(SeedData);
        }
    }
}
