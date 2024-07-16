using Prime.Extensions;
using Prime.Models.Agreements.Internal;

namespace Prime.Models
{
    public enum AgreementType
    {
        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.RegulatedUser)]
        CommunityPharmacistTOA = 1,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.RegulatedUser)]
        RegulatedUserTOA = 2,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.OnBehalfOf)]
        OboTOA = 3,

        [AgreementGroups(AgreementGroup.Organization)]
        CommunityPracticeOrgAgreement = 4,

        [AgreementGroups(AgreementGroup.Organization)]
        CommunityPharmacyOrgAgreement = 5,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.OnBehalfOf)]
        PharmacyOboTOA = 6,

        [AgreementGroups(AgreementGroup.Organization)]
        DeviceProviderOrgAgreement = 7,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.RegulatedUser)]
        PharmacyTechnicianTOA = 8,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.RegulatedUser)]
        LicencedPracticalNurseTOA = 9,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.RegulatedUser)]
        DeviceProviderRUTOA = 10,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.OnBehalfOf)]
        DeviceProviderOBOTOA = 11,

        [AgreementGroups(AgreementGroup.Enrollee, AgreementGroup.OnBehalfOf)]
        PrescriberOBOTOA = 12,
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

        public static bool IsRegulatedUserAgreement(this AgreementType agreementType)
        {
            return agreementType.HasGroup(AgreementGroup.RegulatedUser);
        }

        public static bool IsOnBehalfOfAgreement(this AgreementType agreementType)
        {
            return agreementType.HasGroup(AgreementGroup.OnBehalfOf);
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
