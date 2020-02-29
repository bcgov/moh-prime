using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class AssignedPrivilegeConfiguration : IEntityTypeConfiguration<AssignedPrivilege>
    {
        public void Configure(EntityTypeBuilder<AssignedPrivilege> builder)
        {
            builder.HasKey(ap => new { ap.PrivilegeId, ap.EnrolleeId });

            builder
                .HasOne<Privilege>(ap => ap.Privilege)
                .WithMany(p => p.AssignedPrivileges)
                .HasForeignKey(ap => ap.PrivilegeId);

            builder
                .HasOne<Enrollee>(ap => ap.Enrollee)
                .WithMany(l => l.AssignedPrivileges)
                .HasForeignKey(ap => ap.EnrolleeId);
        }
    }
}
