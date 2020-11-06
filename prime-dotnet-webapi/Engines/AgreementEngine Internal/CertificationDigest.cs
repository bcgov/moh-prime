using Prime.Models;

namespace Prime.Engines.AgreementEngineInternal
{
    public interface ICertificationDigest
    {
        AgreementType? Resolve(SettingsDigest settingsDigest);
    }

    public static class CertificationDigest
    {
        public static ICertificationDigest FromCertification(Certification cert)
        {
            if (cert == null)
            {
                return new NoCollege();
            }

            if (!cert.License.LicensedToProvideCare)
            {
                return new CannotProvideCare();
            }

            bool regulated = cert.License.NamedInImReg;

            //TODO: Move this logic somewhere better, remove magic integer
            if (cert.CollegeCode == 2)
            {
                return new Pharmacist(regulated);
            }
            else
            {
                return new OtherCollege(regulated);
            }
        }
    }

    public class NoCollege : ICertificationDigest
    {
        public AgreementType? Resolve(SettingsDigest settingsDigest)
        {
            if (settingsDigest.Multiple)
            {
                return HandleMultipleSettings(settingsDigest);
            }
            else
            {
                return HandleSingleSetting(settingsDigest);
            }
        }

        private AgreementType? HandleSingleSetting(SettingsDigest settingsDigest)
        {
            if (settingsDigest.HasCommunityPharmacy)
            {
                return AgreementType.PharmacyOboTOA;
            }
            else
            {
                return AgreementType.OboTOA;
            }
        }

        private AgreementType? HandleMultipleSettings(SettingsDigest settingsDigest)
        {
            if (settingsDigest.HasCommunityPharmacy)
            {
                // Normally, An OBO should not be working in both a Pharmacy setting and somewhere else
                return null;
            }
            else
            {
                return AgreementType.OboTOA;
            }
        }
    }

    public class CannotProvideCare : ICertificationDigest
    {
        public AgreementType? Resolve(SettingsDigest settingsDigest)
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

        public AgreementType? Resolve(SettingsDigest settingsDigest)
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

    public class OtherCollege : ICertificationDigest
    {
        private bool Regulated { get; set; }

        public OtherCollege(bool regulated)
        {
            Regulated = regulated;
        }

        public AgreementType? Resolve(SettingsDigest settingsDigest)
        {
            if (settingsDigest.HasCommunityPharmacy)
            {
                // Normally, only Pharmacists should have Community Pharmacy
                return null;
            }

            if (Regulated)
            {
                return AgreementType.RegulatedUserTOA;
            }
            else
            {
                return AgreementType.OboTOA;
            }
        }
    }
}
