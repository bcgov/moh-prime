using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;

namespace Prime.Configuration.Agreements
{
    public class CommunityPracticeOrgAgreementConfiguration : SeededTable<CommunityPracticeOrgAgreement>
    {
        public override IEnumerable<CommunityPracticeOrgAgreement> SeedData
        {
            get
            {
                return AgreementVersionConfiguration.SeedData.OfType<CommunityPracticeOrgAgreement>();
            }
        }

        public override void Configure(EntityTypeBuilder<CommunityPracticeOrgAgreement> builder)
        {
            builder.HasData(SeedData.LoadText());
        }
    }
}
