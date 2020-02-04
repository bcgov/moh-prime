using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : SeededTable<UserClause>
    {
        private string ruClause = System.IO.File.ReadAllText("./Configuration/assets/ru-access-terms.html");
        private string oboClause = System.IO.File.ReadAllText("./Configuration/assets/obo-access-terms.html");
        public override ICollection<UserClause> SeedData
        {
            get
            {
                return new[] {
                    new UserClause { Id = 1, Clause = this.oboClause, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 2, Clause = this.ruClause, EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
