using System;
using Bogus;
using Prime.Models;

namespace PrimeTest.ModelFactories
{
    public static class FactoryExtensions
    {
        public static void SetBaseRules<T>(this Faker<T> faker) where T : BaseAuditable
        {
            faker.StrictMode(true);
            faker.RuleFor(x => x.CreatedUserId, f => Guid.Empty);
            faker.RuleFor(x => x.CreatedTimeStamp, f => DateTime.Now);
            faker.RuleFor(x => x.UpdatedUserId, f => Guid.Empty);
            faker.RuleFor(x => x.UpdatedTimeStamp, f => DateTime.Now);
        }

        public static string OrOther(this string value, Faker f, string other, float otherWeight = 0.5f)
        {
            if (otherWeight > 1 || otherWeight < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(otherWeight), $".{nameof(OrOther)}() {nameof(otherWeight)} of '{otherWeight}' must be between 1.0f and 0.0f.");
            }

            return new string(f.Random.Float() > otherWeight ? value : other);
        }
    }
}
