using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class EnrolleeConfiguration : IEntityTypeConfiguration<Enrollee>
    {
        public void Configure(EntityTypeBuilder<Enrollee> builder)
        {
            builder
                .HasIndex(e => e.UserId)
                .IsUnique();
            builder
                .HasIndex(e => e.GPID)
                .IsUnique();
            builder
                .HasIndex(e => e.HPDID)
                .IsUnique();
        }
    }
}
