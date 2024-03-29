using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class AdjudicatorNoteConfiguration : IEntityTypeConfiguration<EnrolleeNote>
    {
        public void Configure(EntityTypeBuilder<EnrolleeNote> builder)
        {
            builder
                .HasOne<Admin>(an => an.Adjudicator)
                .WithMany(a => a.AdjudicatorNotes)
                .HasForeignKey(an => an.AdjudicatorId);
        }
    }
}
