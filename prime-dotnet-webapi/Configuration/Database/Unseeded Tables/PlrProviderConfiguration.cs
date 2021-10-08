using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class PlrProviderConfiguration : IEntityTypeConfiguration<PlrProvider>
    {
        public void Configure(EntityTypeBuilder<PlrProvider> builder)
        {
            var strCollectionComparer = new ValueComparer<ICollection<string>>(
                (coll1, coll2) => coll1.SequenceEqual(coll2),
                coll => coll.Aggregate(0, (accumulated, elem) => HashCode.Combine(accumulated, elem.GetHashCode())),
                coll => (ICollection<string>)coll.ToList());

            // Store a collection of string values in a non-array database column,
            // see https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#collections-of-primitives
            // and https://docs.microsoft.com/en-us/ef/core/modeling/value-comparers?tabs=ef5#overriding-the-default-comparer
            builder.Property(e => e.Credentials)
                .HasConversion(
                    val => JsonSerializer.Serialize(val, null),
                    val => JsonSerializer.Deserialize<List<string>>(val, null))
                .Metadata.SetValueComparer(
                    strCollectionComparer);
            builder.Property(e => e.Expertise)
                .HasConversion(
                    val => JsonSerializer.Serialize(val, null),
                    val => JsonSerializer.Deserialize<List<string>>(val, null))
                .Metadata.SetValueComparer(
                    strCollectionComparer);

            builder.HasIndex(p => p.Ipc)
                .IsUnique();
        }
    }
}
