using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;
using Prime.Configuration.Database.Resources;

namespace Prime.Configuration.Database
{
    public class AgreementVersionConfiguration : SeededTable<AgreementVersion>
    {
        public override IEnumerable<AgreementVersion> SeedData
        {
            get
            {
                return new[] {
                    new AgreementVersion { Id = 1,  AgreementType = AgreementType.OboTOA,                        Text =  "obo-access-terms-v1.html",    EffectiveDate = SEEDING_DATE,                                CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 2,  AgreementType = AgreementType.RegulatedUserTOA,              Text =  "ru-access-terms-v1.html",     EffectiveDate = SEEDING_DATE,                                CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 3,  AgreementType = AgreementType.OboTOA,                        Text =  "obo-access-terms-v2.html",    EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 4,  AgreementType = AgreementType.RegulatedUserTOA,              Text =  "ru-access-terms-v2.html",     EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 5,  AgreementType = AgreementType.OboTOA,                        Text =  "obo-access-terms-v3.html",    EffectiveDate = DateTimeOffset.Parse("2020-03-10 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 6,  AgreementType = AgreementType.RegulatedUserTOA,              Text =  "ru-access-terms-v3.html",     EffectiveDate = DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 7,  AgreementType = AgreementType.OboTOA,                        Text =  "obo-access-terms-v4.html",    EffectiveDate = DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 8,  AgreementType = AgreementType.RegulatedUserTOA,              Text =  "ru-access-terms-v4.html",     EffectiveDate = DateTimeOffset.Parse("2020-06-03 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 9,  AgreementType = AgreementType.CommunityPharmacistTOA,        Text =  "com-pharm-terms-v1.html",     EffectiveDate = SEEDING_DATE,                                CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 10, AgreementType = AgreementType.CommunityPharmacistTOA,        Text =  "com-pharm-terms-v2.html",     EffectiveDate = DateTimeOffset.Parse("2020-08-28 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 11, AgreementType = AgreementType.CommunityPracticeOrgAgreement, Text =  "com-practice-org-v1.html",    EffectiveDate = SEEDING_DATE,                                CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 12, AgreementType = AgreementType.CommunityPharmacyOrgAgreement, Text =  "com-pharmacy-org-v1.html",    EffectiveDate = SEEDING_DATE,                                CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 13, AgreementType = AgreementType.CommunityPharmacistTOA,        Text =  "com-pharm-terms-v3.html",     EffectiveDate = DateTimeOffset.Parse("2020-10-22 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 14, AgreementType = AgreementType.PharmacyOboTOA,                Text =  "pharmacy-obo-toa-v1.html",    EffectiveDate = SEEDING_DATE,                                CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 15, AgreementType = AgreementType.OboTOA,                        Text =  "obo-access-terms-v5.html",    EffectiveDate = DateTimeOffset.Parse("2020-11-12 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 16, AgreementType = AgreementType.RegulatedUserTOA,              Text =  "ru-access-terms-v5.html",     EffectiveDate = DateTimeOffset.Parse("2020-11-27 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new AgreementVersion { Id = 17, AgreementType = AgreementType.DeviceProviderOrgAgreement,    Text =  "device-provider-org-v1.html", EffectiveDate = SEEDING_DATE,                                CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }

        public override void Configure(EntityTypeBuilder<AgreementVersion> builder)
        {
            builder.HasData(LoadText(SeedData));
        }

        private static IEnumerable<AgreementVersion> LoadText(IEnumerable<AgreementVersion> clauses)
        {
            var processed = clauses.ToList();
            processed.ForEach(clause => clause.Text = ResourceLoader.Load(clause.Text));
            return processed;
        }
    }
}
