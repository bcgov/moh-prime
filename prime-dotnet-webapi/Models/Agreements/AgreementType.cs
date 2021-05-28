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
        /// Checks for an enrollee agreement type.
        /// </summary>
        public static bool IsEnrolleeAgreement(this AgreementType agreementType)
        {
            return agreementType != AgreementType.CommunityPracticeOrgAgreement && agreementType != AgreementType.CommunityPharmacyOrgAgreement;
        }

        public static IEnumerable<AgreementType> EnrolleeAgreementTypes()
        {
            return Enum.GetValues(typeof(AgreementType))
                .Cast<AgreementType>()
                .Where(t => t.IsEnrolleeAgreement());
        }
    }
}
