using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class EnrolmentStatusReasonConfiguration : IEntityTypeConfiguration<EnrolmentStatusReason>
    {
        public void Configure(EntityTypeBuilder<EnrolmentStatusReason> builder)
        {
            builder
                .HasOne(esr => esr.EnrolmentStatus)
                .WithMany(es => es.EnrolmentStatusReasons)
                .HasForeignKey(esr => esr.EnrolmentStatusId);

            builder
                .HasOne(esr => esr.StatusReason)
                .WithMany(sr => sr.EnrolmentStatusReasons)
                .HasForeignKey(esr => esr.StatusReasonCode);
        }
    }
}
