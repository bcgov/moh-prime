using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
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
            builder
                .HasOne(e => e.PaperEnrolment)
                .WithOne(l => l.Enrollee);
            builder
                .HasOne(e => e.LinkedEnrolment)
                .WithOne(l => l.PaperEnrollee);
        }
    }
}
