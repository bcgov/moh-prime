using System.Collections.Generic;

using Prime.Models;

namespace Prime.DTOs.AgreementEngine
{
    public class AgreementEngineDto
    {
        public ICollection<CertificationDto> Certifications { get; set; }

        public ICollection<EnrolleeDeviceProvider> EnrolleeDeviceProviders { get; set; }

        public ICollection<int> CareSettingCodes { get; set; }
    }

    public class CertificationDto
    {
        public int CollegeCode { get; set; }
        public string PractitionerId { get; set; }

        public License License { get; set; }
    }
}
