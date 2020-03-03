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
                .HasKey(a => new { a.Id });

            builder
                .HasIndex("UserId")
                .IsUnique();

            builder
                .HasOne<Admin>(e => e.Adjudicator)
                .WithMany(a => a.Enrollees)
                .HasForeignKey(a => a.AdjudicatorId);
        }
    }
}
