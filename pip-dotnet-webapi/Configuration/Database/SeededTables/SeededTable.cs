using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace Pidp.Configuration.Database
{
    public abstract class SeededTable<T> : IEntityTypeConfiguration<T> where T : class
    {
        public static readonly LocalDate SeedingDate = new(2021, 12, 1);

        public abstract IEnumerable<T> SeedData { get; }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasData(SeedData);
        }
    }
}
