using System.Collections.Generic;
using Prime.Models;
using Prime.Configuration;
using Prime;
using System;

namespace PrimeTests.ModelFactories
{
    public static class UserClauseLookup
    {
        static readonly DateTime SEEDING_DATE = new DateTime(2019, 9, 16);
        private static ICollection<UserClause> _seedData
        {
            get
            {
                return new[] {
                    new UserClause{ Id = 1, Clause = "", EnrolleeClassification = PrimeConstants.PRIME_OBO, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new UserClause{ Id = 2, Clause = "", EnrolleeClassification = PrimeConstants.PRIME_RU, EffectiveDate = SEEDING_DATE, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE }
                };
            }
        }

        public static ICollection<UserClause> All { get { return _seedData; } }
    }
}
