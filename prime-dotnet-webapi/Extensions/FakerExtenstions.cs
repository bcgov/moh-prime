using System;
using Bogus;
using Prime.Models;

namespace Prime
{
    public static class FakerExtensions
    {
        public static void SetBaseRules<T>(this Faker<T> faker) where T : BaseAuditable
        {
            faker.StrictMode(true);
            faker.RuleFor(x => x.CreatedUserId, f => Guid.Empty);
            faker.RuleFor(x => x.CreatedTimeStamp, f => DateTime.Now);
            faker.RuleFor(x => x.UpdatedUserId, f => Guid.Empty);
            faker.RuleFor(x => x.UpdatedTimeStamp, f => DateTime.Now);
        }
    }
}
