using System;
using System.Linq;
using System.Collections.Generic;

using Prime.Models;
using Prime.Configuration.Resources;

namespace Prime.Configuration.Agreements
{
    public static class AgreementVersionConfiguration
    {
        private static DateTime SEEDING_DATE = SeededTable<AgreementVersion>.SEEDING_DATE;

        public static IEnumerable<AgreementVersion> SeedData
        {
            get
            {
                return new AgreementVersion[]{
                    new OboAgreement                 { Id = 1, Text = "obo-access-terms-v1.html", AgreementVersionType = AgreementVersionType.OboTOA, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 2, Text =  "ru-access-terms-v1.html", AgreementVersionType = AgreementVersionType.RegulatedUserTOA, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OboAgreement                 { Id = 3, Text = "obo-access-terms-v2.html", AgreementVersionType = AgreementVersionType.OboTOA, EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 4, Text =  "ru-access-terms-v2.html", AgreementVersionType = AgreementVersionType.RegulatedUserTOA, EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OboAgreement                 { Id = 5, Text = "obo-access-terms-v3.html", AgreementVersionType = AgreementVersionType.OboTOA, EffectiveDate = DateTimeOffset.Parse("2020-03-10 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 6, Text =  "ru-access-terms-v3.html", AgreementVersionType = AgreementVersionType.RegulatedUserTOA, EffectiveDate = DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OboAgreement                 { Id = 7, Text = "obo-access-terms-v4.html", AgreementVersionType = AgreementVersionType.OboTOA, EffectiveDate = DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 8, Text =  "ru-access-terms-v4.html", AgreementVersionType = AgreementVersionType.RegulatedUserTOA, EffectiveDate = DateTimeOffset.Parse("2020-06-03 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CommunityPharmacistAgreement { Id = 9, Text =  "com-pharm-terms-v1.html", AgreementVersionType = AgreementVersionType.CommunityPharmacistTOA, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CommunityPharmacistAgreement { Id = 10, Text = "com-pharm-terms-v2.html", AgreementVersionType = AgreementVersionType.CommunityPharmacistTOA, EffectiveDate = DateTimeOffset.Parse("2020-08-28 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

        public static IEnumerable<AgreementVersion> LoadText(this IEnumerable<AgreementVersion> clauses)
        {
            var processed = clauses.ToList();
            processed.ForEach(clause => clause.Text = ResourceLoader.Load(clause.Text));
            return processed;
        }
    }
}
