using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class EnrolleeCredentialConfiguration : IEntityTypeConfiguration<EnrolleeCredential>
    {
        public void Configure(EntityTypeBuilder<EnrolleeCredential> builder)
        {
            builder
                .HasOne(ec => ec.Enrollee)
                .WithMany(e => e.EnrolleeCredentials)
                .HasForeignKey(ec => ec.EnrolleeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
