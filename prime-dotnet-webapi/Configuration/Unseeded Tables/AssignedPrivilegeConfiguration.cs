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
        }
    }
}
