using System.Collections.Generic;
using System.IO;
using Prime.Models;

namespace Prime.Configuration
{
    public class UserClauseConfiguration : SeededTable<UserClause>
    {
        public string getClause(string file)
        {
            string clauseFile = Path.GetFullPath("./Configuration/assets/" + file);

            if (File.Exists(clauseFile))
            {
                return System.IO.File.ReadAllText(clauseFile);
            }
            else if (clauseFile.Contains("prime-dotnet-webapi-tests"))
            {
                return "";
            }
            throw new FileNotFoundException("Could not find file");
        }
        public override ICollection<UserClause> SeedData
        {
            get
            {
                return new[] {
                    new UserClause { Id = 1, Clause = this.getClause("obo-access-terms.html"), EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 2, Clause = this.getClause("ru-access-terms.html"), EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }
    }
}
