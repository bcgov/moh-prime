using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration.Resources;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : SeededTable<UserClause>
    {
        private string ruClause;
        private string oboClause;

        public override ICollection<UserClause> SeedData
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ruClause) || string.IsNullOrWhiteSpace(oboClause))
                {
                    ruClause = ResourceLoader.Load("ru-access-terms.html");
                    oboClause = ResourceLoader.Load("obo-access-terms.html");
                }

                return new[] {
                    new UserClause { Id = 1, Clause = oboClause, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 2, Clause = ruClause, EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
