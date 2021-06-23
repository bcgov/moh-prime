using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeCareSettingViewModel
    {
        public ICollection<int> CareSettingCodes { get; set; }

        public ICollection<HealthAuthorityCode> HealthAuthorityCodes { get; set; }
    }
}
