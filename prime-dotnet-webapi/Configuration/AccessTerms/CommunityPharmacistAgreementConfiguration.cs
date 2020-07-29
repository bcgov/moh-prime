using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;

namespace Prime.Configuration.Agreements
{
    public class CommunityPharmacistAgreementConfiguration : SeededTable<CommunityPharmacistAgreement>
    {
        public override ICollection<CommunityPharmacistAgreement> SeedData
        {
            get
            {
                return Agreements.SeedData.OfType<CommunityPharmacistAgreement>().ToList();
            }
        }

        public override void Configure(EntityTypeBuilder<CommunityPharmacistAgreement> builder)
        {
            builder.HasData(SeedData.LoadText());
        }
    }
}
