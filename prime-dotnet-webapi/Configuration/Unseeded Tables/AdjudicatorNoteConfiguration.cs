using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prime.Models;

namespace Prime.Configuration
{
    public class AdjudicatorNoteConfiguration : IEntityTypeConfiguration<EnrolleeNote>
    {
        public void Configure(EntityTypeBuilder<EnrolleeNote> builder)
        {
            builder.HasKey(an => new { an.Id });

            builder
                .HasOne<Admin>(an => an.Adjudicator)
                .WithMany(a => a.AdjudicatorNotes)
                .HasForeignKey(an => an.AdjudicatorId);
        }
    }
}
