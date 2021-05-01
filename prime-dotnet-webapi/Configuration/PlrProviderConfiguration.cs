using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class PlrProviderConfiguration : IEntityTypeConfiguration<PlrProvider>
    {
        public void Configure(EntityTypeBuilder<PlrProvider> builder)
        {
            // Courtesy of https://stackoverflow.com/questions/20711986/entity-framework-code-first-cant-store-liststring
            // Store an array of string values in a non-array database column
            builder.Property(e => e.Credentials)
                .HasConversion(
                    v => string.Join('|', v),
                    v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));
            builder.Property(e => e.Expertise)
                .HasConversion(
                    v => string.Join('|', v),
                    v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));

            builder.HasIndex(p => p.Ipc)
                .IsUnique();
        }
    }
}
