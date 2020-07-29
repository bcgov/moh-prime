using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;

namespace Prime.Configuration.Agreements
{
    public class RegulatedUserAgreementConfiguration : SeededTable<RegulatedUserAgreement>
    {
        public override ICollection<RegulatedUserAgreement> SeedData
        {
            get
            {
                return Agreements.SeedData.OfType<RegulatedUserAgreement>().ToList();
            }
        }

        public override void Configure(EntityTypeBuilder<RegulatedUserAgreement> builder)
        {
            builder.HasData(SeedData.LoadText());
        }
    }
}
