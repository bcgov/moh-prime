using System.Linq;
using System.Collections.Generic;

using Prime.Models;
using Prime.DTOs.AgreementEngine;

namespace Prime.Engines.AgreementEngineInternal
{
    public interface ICertificationDigest
    {
        AgreementType? ResolveWith(SettingsDigest settings);
    }

    public static class CertificationDigest
    {
        public static ICertificationDigest Create(ICollection<CertificationDto> certs)
        {
            if (certs.Count > 1)
            {
                return new MultipleColleges();
            }

            if (certs.Count == 0)
            {
                return new NoCollege();
            }

            var cert = certs.Single();

            if (!cert.License.CurrentLicenseDetail.LicensedToProvideCare)
            {
                return new CannotProvideCare();
            }

            bool regulated = cert.License.CurrentLicenseDetail.NamedInImReg;

            if (College.IsCollegeOfPharmacists(cert.CollegeCode))
            {
                if (License.IsPharmacyTechnician(cert.License.CurrentLicenseDetail))
                {
                    return new PharmacyTechnician(regulated);
                }
                else
                {
                    return new Pharmacist(regulated);
                }
            }
            else
            {
                if (License.isLicensedPracticalNurse(cert.License.CurrentLicenseDetail))
                {
                    return new LicensedPracticalNurse();
                }
                else
                {
                    return new OtherCollege(regulated, cert);
                }
            }
        }
    }

    public class MultipleColleges : ICertificationDigest
    {
        public AgreementType? ResolveWith(SettingsDigest settings)
        {
            // Multiple College licences result in too many edge cases for automatic determination to be possible.
            return null;
        }
    }

    public class NoCollege : ICertificationDigest
    {
        public AgreementType? ResolveWith(SettingsDigest settings)
        {
            if (settings.HasCommunityPharmacy)
            {
                if (settings.Multiple)
                {
                    // Normally, An OBO should not be working in both a Pharmacy setting and somewhere else
                    return null;
                }

                return AgreementType.PharmacyOboTOA;
            }
            else
            {
                return AgreementType.OboTOA;
            }
        }
    }

    public class CannotProvideCare : ICertificationDigest
    {
        public AgreementType? ResolveWith(SettingsDigest settings)
        {
            return null;
        }
    }

    public class Pharmacist : ICertificationDigest
    {
        private bool Regulated { get; set; }

        public Pharmacist(bool regulated)
        {
            Regulated = regulated;
        }

        public AgreementType? ResolveWith(SettingsDigest settings)
        {
            // A Pharmacist always recieves a Pharmacy Agreement, regardless of setting
            if (Regulated)
            {
                return AgreementType.CommunityPharmacistTOA;
            }
            else
            {
                return AgreementType.PharmacyOboTOA;
            }
        }
    }

    public class PharmacyTechnician : ICertificationDigest
    {
        private bool Regulated { get; set; }

        public PharmacyTechnician(bool regulated)
        {
            Regulated = regulated;
        }

        public AgreementType? ResolveWith(SettingsDigest settings)
        {
            if (Regulated)
            {
                return AgreementType.PharmacyTechnicianTOA;
            }
            else
            {
                return AgreementType.PharmacyOboTOA;
            }
        }
    }

    public class LicensedPracticalNurse : ICertificationDigest
    {
        public LicensedPracticalNurse() { }

        public AgreementType? ResolveWith(SettingsDigest settings)
        {
            return AgreementType.LicencedPracticalNurseTOA;
        }
    }

    public class OtherCollege : ICertificationDigest
    {
        private bool Regulated { get; set; }
        private CertificationDto Certification { get; set; }

        public OtherCollege(bool regulated, CertificationDto certification)
        {
            Regulated = regulated;
            Certification = certification;
        }

        public AgreementType? ResolveWith(SettingsDigest settings)
        {
            if (settings.HasCommunityPharmacy)
            {
                // Normally, only Pharmacists or Pharmacy OBOs should have Community Pharmacy
                return null;
            }

            if (Regulated)
            {
                if (!Certification.License.CurrentLicenseDetail.PrescriberIdType.HasValue || Certification.PractitionerId != null)
                {
                    return AgreementType.RegulatedUserTOA;
                }
                else
                {
                    return AgreementType.OboTOA;
                }
            }
            else
            {
                return AgreementType.OboTOA;
            }
        }
    }
}
