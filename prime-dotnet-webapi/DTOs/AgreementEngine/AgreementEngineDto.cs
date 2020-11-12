using System;
using System.Collections.Generic;

using Prime.Models;

namespace Prime.DTOs.AgreementEngine
{
    public class AgreementEngineDto
    {
        public ICollection<CertificationDto> Certifications { get; set; }

        public ICollection<CareSetting> CareSettings { get; set; }
    }

    public class CertificationDto
    {
        public int CollegeCode { get; set; }

        public License License { get; set; }
    }
}
