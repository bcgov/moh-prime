using System.Linq;

using Prime.Models;

namespace Prime.Engines
{
    public class AgreementEngine
    {
        /// <summary>
        /// Determines the type of Agreement to asign to an Enrollee.
        /// May return null if no automatic Agreement Type could be determined.
        /// </summary>
        public AgreementType? DetermineAgreementType(Enrollee enrollee)
        {
            if (enrollee.certifications.Count() > 1)
            {
                return null;
            }

            if (!enrollee.IsRegulatedUser())
            {
                return AgreementType.OboTOA;
            }

            if (enrollee.HasCareSetting(CareSettingType.CommunityPharmacy))
            {
                return AgreementType.CommunityPharmacistTOA;
            }
            else
            {
                return AgreementType.RegulatedUserTOA;
            }
        }
    }
}
