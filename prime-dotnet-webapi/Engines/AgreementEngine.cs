using System.Linq;

using Prime.Models;
using Prime.Engines.AgreementEngineInternal;

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
            if (enrollee.Certifications.Count() > 1)
            {
                // Multiple College licences result in too many edge cases for automatic determination to be possible.
                return null;
            }

            var certDigest = CertificationDigest.FromCertification(enrollee.Certifications.SingleOrDefault());
            var settings = new SettingsDigest(enrollee.EnrolleeCareSettings);

            return certDigest.ResolveWith(settings);
        }
    }
}
