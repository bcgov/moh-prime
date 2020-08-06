using System;
using System.Linq;
using System.Collections.Generic;

using Prime.Models;
using Prime.Configuration.Resources;

namespace Prime.Configuration.Agreements
{
    public static class AgreementConfiguration
    {
        private static DateTime SEEDING_DATE = SeededTable<UserClause>.SEEDING_DATE;

        public static IEnumerable<UserClause> SeedData
        {
            get
            {
                return new UserClause[]{
                    new OboAgreement                 { Id = 1, Text = "obo-access-terms-v1.html", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 2, Text =  "ru-access-terms-v1.html", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OboAgreement                 { Id = 3, Text = "obo-access-terms-v2.html", EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 4, Text =  "ru-access-terms-v2.html", EffectiveDate = DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OboAgreement                 { Id = 5, Text = "obo-access-terms-v3.html", EffectiveDate = DateTimeOffset.Parse("2020-03-10 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 6, Text =  "ru-access-terms-v3.html", EffectiveDate = DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new OboAgreement                 { Id = 7, Text = "obo-access-terms-v4.html", EffectiveDate = DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new RegulatedUserAgreement       { Id = 8, Text =  "ru-access-terms-v4.html", EffectiveDate = DateTimeOffset.Parse("2020-06-03 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new CommunityPharmacistAgreement { Id = 9, Text =  "com-pharm-terms-v1.html", EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }

        public static IEnumerable<UserClause> LoadText(this IEnumerable<UserClause> clauses)
        {
            var processed = clauses.ToList();
            processed.ForEach(clause => clause.Text = ResourceLoader.Load(clause.Text));
            return processed;
        }
    }
}
