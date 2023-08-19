using System;
using System.Linq;

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
            if (dto.Certifications == null || dto.Certifications.Any(c => c.License == null))
            {
                throw new ArgumentException($"Certifications must have Licences loaded.", nameof(dto));
            }

            var certDigest = CertificationDigest.Create(dto.Certifications, dto.EnrolleeDeviceProviders);
            var settingsDigest = new SettingsDigest(dto.CareSettingCodes);

            return certDigest.ResolveWith(settingsDigest);
        }
    }
}
