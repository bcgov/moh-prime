using System.Collections.Generic;
using System.IO;
using Prime.Models;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : SeededTable<UserClause>
    {

        public string getRuClause()
        {
            string ruClauseFile = "./Configuration/assets/ru-access-terms.html";
            if (File.Exists(ruClauseFile))
            {
                return System.IO.File.ReadAllText(ruClauseFile);
            }
            throw new FileNotFoundException("Could not find file");
        }

        public string getOboClause()
        {
            string oboClauseFile = "./Configuration/assets/obo-access-terms.html";
            if (File.Exists(oboClauseFile))
            {
                return System.IO.File.ReadAllText(oboClauseFile);
            }
            throw new FileNotFoundException("Could not find file");
        }
        public override ICollection<UserClause> SeedData
        {
            get
            {
                return new[] {
                    new UserClause { Id = 1, Clause = this.getOboClause(), EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 2, Clause = this.getRuClause(), EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
