using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace Prime.ModelFactories
{
    public static class LimitsAndConditionsClauseLookup
    {
        private static ICollection<LimitsAndConditionsClause> _seedData = new LimitsAndConditionsClauseConfiguration().SeedData;

        public static ICollection<LimitsAndConditionsClause> All { get { return _seedData; } }
    }
}
