using System.Collections.Generic;
using Prime;
using Prime.Models;
using PrimeTests.ModelFactories;

namespace PrimeTests
{
    public static class ContextExtensions
    {
        public static T Has<T>(this ApiDbContext context, T thing)
        {
            context.Add(thing);
            context.SaveChanges();
            return thing;
        }

        public static IEnumerable<T> Has<T>(this ApiDbContext context, IEnumerable<T> things)
        {
            context.AddRange(things);
            context.SaveChanges();
            return things;
        }

        public static Enrollee HasAnEnrollee(this ApiDbContext context, string ruleSets = null)
        {
            return context.Has(new EnrolleeFactory().Generate(ruleSets));
        }
    }
}
