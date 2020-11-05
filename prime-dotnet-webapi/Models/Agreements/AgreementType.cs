using System;
using System.Collections.Generic;
using System.Linq;

namespace Prime.Models
{
    public enum AgreementType
    {
        CommunityPharmacistTOA = 1,
        RegulatedUserTOA = 2,
        OboTOA = 3,
        CommunityPracticeOrgAgreement = 4,
        CommunityPharmacyOrgAgreement = 5,
        PharmacyOboTOA = 6
    }

    public static class AgreementTypeExtensions
    {
        /// <summary>
        /// Checks an agreement type is a TOA.
        /// </summary>
        public static bool IsToa(this AgreementType agreementType)
        {
            return Enum.GetValues(typeof(AgreementType))
                .Cast<AgreementType>()
                .Where(v =>
                    v != AgreementType.CommunityPracticeOrgAgreement &&
                    v != AgreementType.CommunityPharmacyOrgAgreement)
                .ToList()
                .Contains(agreementType);
        }
    }
}
