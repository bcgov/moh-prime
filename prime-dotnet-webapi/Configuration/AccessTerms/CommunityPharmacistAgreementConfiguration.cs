using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;

namespace Prime.Configuration.Agreements
{
    public class CommunityPharmacistAgreementConfiguration : SeededTable<CommunityPharmacistAgreement>
    {
        public override IEnumerable<CommunityPharmacistAgreement> SeedData
        {
            get
            {
                return Agreements.SeedData.OfType<CommunityPharmacistAgreement>();
            }
        }

        public override void Configure(EntityTypeBuilder<CommunityPharmacistAgreement> builder)
        {
            builder.HasData(SeedData.LoadText());
        }
    }
}
