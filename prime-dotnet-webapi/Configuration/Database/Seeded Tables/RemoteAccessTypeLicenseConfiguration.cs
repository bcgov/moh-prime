using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class RemoteAccessTypeLicenseConfiguration : SeededTable<RemoteAccessTypeLicense>
    {
        public override IEnumerable<RemoteAccessTypeLicense> SeedData
        {
            get
            {
                return new[] {
                    // Normal Remote Access for select physician and nurse practitioner license classes
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 1 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 2 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 3 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 4 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 5 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 6 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 8 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 9 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 10 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 12 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 13 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 14 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 15 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 17 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 18 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 19 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 47 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 51 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 59 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 66 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 67 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 87 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 88 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.PrivateCommunityHealthPractice, LicenseCode = 89 },

                    // FNHA for select physician and nurse practitioner license classes
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 1 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 2 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 3 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 4 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 5 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 6 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 8 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 9 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 10 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 12 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 13 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 14 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 15 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 17 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 18 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 19 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 47 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 51 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 59 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 66 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 67 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 87 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 88 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 89 },
                    //FNHA for additional nurse license classes
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 35 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 32 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 41 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 175 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 176 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 39 },
                    new RemoteAccessTypeLicense { RemoteAccessTypeCode = RemoteAccessTypeEnum.FNHA, LicenseCode = 45 },
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
