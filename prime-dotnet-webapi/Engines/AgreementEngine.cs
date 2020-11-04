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

            var context = CertificationContext.FromEnrolleeCertifiaction(enrollee.Certifications.SingleOrDefault());


        }

        private class CertificationContext
        {
            public enum User
            {
                CannotProvideCare,
                HasNoLicence,
                HasOtherLicence,
                IsPharmacist
            }

            public bool Regulated { get; set; }
            public User UserContext { get; set; }

            public static CertificationContext FromEnrolleeCertification(Certification cert)
            {
                return new CertificationContext
                {
                    Regulated = cert?.License.NamedInImReg ?? false,
                    UserContext = DetermineUserContext(cert)
                };
            }

            private static User DetermineUserContext(Certification cert)
            {
                if (cert == null)
                {
                    return User.HasNoLicence;
                }

                if (!cert.License.LicensedToProvideCare)
                {
                    return User.CannotProvideCare;
                }

                if (cert.CollegeCode == 2) // Is College of Pharmacists
                {
                    return User.IsPharmacist;
                }
                else
                {
                    return User.HasOtherLicence;
                }
            }
        }
    }
}
