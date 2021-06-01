using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class EnrolmentStatusConfiguration : IEntityTypeConfiguration<EnrolmentStatus>
    {
        public void Configure(EntityTypeBuilder<EnrolmentStatus> builder)
        {
            builder
                .HasOne(es => es.Enrollee)
                .WithMany(e => e.EnrolmentStatuses)
                .HasForeignKey(es => es.EnrolleeId);

            builder
                .HasOne(es => es.Status)
                .WithMany(s => s.EnrolmentStatuses)
                .HasForeignKey(es => es.StatusCode);
        }
    }
}
