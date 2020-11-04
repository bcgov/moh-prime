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
            if (enrollee.Certifications.Count() > 1)
            {
                // Multiple College licences result in too many edge cases for automatic determination to be possible.
                return null;
            }

            var context = CertificationContext.FromCertification(enrollee.Certifications.SingleOrDefault());


        }

        private class CertificationContext
        {
            public enum CollegeContext
            {
                None,
                Other,
                Pharmacist
            }

            public bool Regulated { get; set; }

            public bool CanProvideCare { get; set; }
            public CollegeContext College { get; set; }

            public static CertificationContext FromCertification(Certification cert)
            {
                return new CertificationContext
                {
                    Regulated = cert?.License.NamedInImReg ?? false,
                    CanProvideCare = cert?.License.LicensedToProvideCare ?? false,
                    College = DetermineCollegeContext(cert)
                };
            }

            private static CollegeContext DetermineCollegeContext(Certification cert)
            {
                if (cert == null)
                {
                    return CollegeContext.None;
                }

                if (cert.CollegeCode == 2) // Is College of Pharmacists
                {
                    return CollegeContext.Pharmacist;
                }
                else
                {
                    return CollegeContext.Other;
                }
            }
        }
    }
}
