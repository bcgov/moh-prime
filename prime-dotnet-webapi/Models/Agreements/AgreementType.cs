using Prime.Extensions;
using Prime.Models.Agreements.Internal;

namespace Prime.Models
{
    public enum AgreementType
    {
        [AgreementGroups(AgreementGroup.Enrollee)]
        CommunityPharmacistTOA = 1,

        [AgreementGroups(AgreementGroup.Enrollee)]
        RegulatedUserTOA = 2,

        [AgreementGroups(AgreementGroup.Enrollee)]
        OboTOA = 3,

        [AgreementGroups(AgreementGroup.Organization)]
        CommunityPracticeOrgAgreement = 4,

        [AgreementGroups(AgreementGroup.Organization)]
        CommunityPharmacyOrgAgreement = 5,

        [AgreementGroups(AgreementGroup.Enrollee)]
        PharmacyOboTOA = 6,

        [AgreementGroups(AgreementGroup.Organization)]
        DeviceProviderOrgAgreement = 7,
    }

    public static class AgreementTypeExtensions
    {
        public static bool IsEnrolleeAgreement(this AgreementType agreementType)
        {
            return agreementType.HasGroup(AgreementGroup.Enrollee);
        }

        public static bool IsOrganizationAgreement(this AgreementType agreementType)
        {
            return agreementType.HasGroup(AgreementGroup.Organization);
        }

        private static bool HasGroup(this AgreementType type, AgreementGroup group)
        {
            var attribute = type.GetAttribute<AgreementGroupsAttribute>();

            if (attribute == null)
            {
                return false;
            }

            return attribute.HasGroup(group);
        }
    }
}
