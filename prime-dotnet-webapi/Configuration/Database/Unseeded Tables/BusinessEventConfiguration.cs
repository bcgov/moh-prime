using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class BusinessEventConfiguration : IEntityTypeConfiguration<BusinessEvent>
    {
        public void Configure(EntityTypeBuilder<BusinessEvent> builder)
        {
            builder.HasOne(be => be.Admin)
                .WithMany()
                .HasForeignKey(be => be.AdminId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(be => be.Enrollee)
                .WithMany()
                .HasForeignKey(be => be.EnrolleeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(be => be.Organization)
                .WithMany()
                .HasForeignKey(be => be.OrganizationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(be => be.Party)
                .WithMany()
                .HasForeignKey(be => be.PartyId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(be => be.Site)
                .WithMany()
                .HasForeignKey(be => be.SiteId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
