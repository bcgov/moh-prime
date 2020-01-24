using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;

namespace PrimeTest.ModelFactories
{
    public static class UserClauseLookup
    {
        private static ICollection<UserClause> _seedData = new UserClauseConfiguration().SeedData;

        public static ICollection<UserClause> All { get { return _seedData; } }
    }
}
