using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prime.Configuration
{
    public abstract class SeededTable<T> : IEntityTypeConfiguration<T> where T : class
    {
        public static readonly DateTimeOffset SEEDING_DATE = DateTimeOffset.Parse("2019-09-16 -7:00");

        public abstract IEnumerable<T> SeedData { get; }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasData(SeedData);
        }
    }
}
