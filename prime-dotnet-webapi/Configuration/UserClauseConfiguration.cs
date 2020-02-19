using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Prime.Models;

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
                if (string.IsNullOrWhiteSpace(ruClause))
                {
                    LoadResources();
                }

                return new[] {
                    new UserClause { Id = 1, Clause = oboClause, EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause { Id = 2, Clause = ruClause, EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

        private void LoadResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();

            string ruResource = resourceNames
              .Single(str => str.EndsWith("ru-access-terms.html"));

            using (Stream stream = assembly.GetManifestResourceStream(ruResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                ruClause = reader.ReadToEnd();
            }

            string oboResource = resourceNames
              .Single(str => str.EndsWith("obo-access-terms.html"));

            using (Stream stream = assembly.GetManifestResourceStream(oboResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                oboClause = reader.ReadToEnd();
            }
        }
    }
}
