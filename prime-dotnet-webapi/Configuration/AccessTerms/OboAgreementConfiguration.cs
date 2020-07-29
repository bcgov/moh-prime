using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;

namespace Prime.Configuration.Agreements
{
    public class OboAgreementConfiguration : SeededTable<OboAgreement>
    {
        public override ICollection<OboAgreement> SeedData
        {
            get
            {
                return Agreements.SeedData.OfType<OboAgreement>().ToList();
            }
        }

        public override void Configure(EntityTypeBuilder<OboAgreement> builder)
        {
            builder.HasData(SeedData.LoadText());
        }
    }
}
