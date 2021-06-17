using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class AgreementConfiguration : IEntityTypeConfiguration<Agreement>
    {
        public void Configure(EntityTypeBuilder<Agreement> builder)
        {
            builder
                .HasOne(toa => toa.Enrollee)
                .WithMany(e => e.Agreements)
                .HasForeignKey(toa => toa.EnrolleeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(toa => toa.Organization)
                .WithMany(e => e.Agreements)
                .HasForeignKey(toa => toa.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(toa => toa.Party)
                .WithMany(e => e.Agreements)
                .HasForeignKey(toa => toa.PartyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasCheckConstraint("CHK_Agreement_OnlyOneForeignKey",
                    @"( CASE WHEN ""EnrolleeId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""OrganizationId"" IS NULL THEN 0 ELSE 1 END
                     + CASE WHEN ""PartyId"" IS NULL THEN 0 ELSE 1 END) = 1");
        }
    }
}
