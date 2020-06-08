using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Resources;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : SeededTable<UserClause>
    {
        private string ruClause1;
        private string ruClause2;
        private string ruClause3;
        private string ruClause4;
        private string oboClause1;
        private string oboClause2;
        private string oboClause3;
        private string oboClause4;

        public override ICollection<UserClause> SeedData
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ruClause1)
                    || string.IsNullOrWhiteSpace(ruClause2)
                    || string.IsNullOrWhiteSpace(oboClause1)
                    || string.IsNullOrWhiteSpace(oboClause2)
                    || string.IsNullOrWhiteSpace(oboClause3)
                )
                {
                    ruClause1 = ResourceLoader.Load("ru-access-terms-v1.html");
                    ruClause2 = ResourceLoader.Load("ru-access-terms-v2.html");
                    ruClause3 = ResourceLoader.Load("ru-access-terms-v3.html");
                    ruClause4 = ResourceLoader.Load("ru-access-terms-v4.html");
                    oboClause1 = ResourceLoader.Load("obo-access-terms-v1.html");
                    oboClause2 = ResourceLoader.Load("obo-access-terms-v2.html");
                    oboClause3 = ResourceLoader.Load("obo-access-terms-v3.html");
                    oboClause4 = ResourceLoader.Load("obo-access-terms-v4.html");
                }

                return new[] {
                    new UserClause { Id = 1, Clause = oboClause1, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 2, Clause = ruClause1, EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 3, Clause = oboClause2, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = System.DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 4, Clause = ruClause2, EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = System.DateTimeOffset.Parse("2020-03-05 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 5, Clause = oboClause3, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = System.DateTimeOffset.Parse("2020-03-10 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 6, Clause = ruClause3, EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = System.DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 7, Clause = oboClause4, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = System.DateTimeOffset.Parse("2020-05-07 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 8, Clause = ruClause4, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = System.DateTimeOffset.Parse("2020-06-03 00:00:00"), CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
