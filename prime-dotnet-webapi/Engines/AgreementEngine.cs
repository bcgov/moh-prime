using Prime.Models;
using Prime.DTOs.AgreementEngine;
using Prime.Engines.AgreementEngineInternal;

namespace Prime.Engines
{
    public static class AgreementEngine
    {
        /// <summary>
        /// Determines the type of Agreement to asign to an Enrollee.
        /// May return null if no automatic Agreement Type could be determined.
        /// </summary>
        public static AgreementType? DetermineAgreementType(AgreementEngineDto dto)
        {
            var certDigest = CertificationDigest.Create(dto.Certifications);
            var settingsDigest = new SettingsDigest(dto.CareSettings);

            return certDigest.ResolveWith(settingsDigest);
        }
    }
}
