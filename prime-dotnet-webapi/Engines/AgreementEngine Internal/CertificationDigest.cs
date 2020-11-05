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
            throw new System.NotImplementedException();
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
            // A pharmacist always recieves a Pharmacy Agreement, regardless of setting
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
            throw new System.NotImplementedException();
        }
    }
}
