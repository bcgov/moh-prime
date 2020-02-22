using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prime.Configuration
{
    public abstract class SeededTable<T> : IEntityTypeConfiguration<T> where T : class
    {
        protected static readonly DateTimeOffset SEEDING_DATE = new DateTimeOffset(new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0));

        public abstract ICollection<T> SeedData { get; }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasData(SeedData);
        }
    }
}
