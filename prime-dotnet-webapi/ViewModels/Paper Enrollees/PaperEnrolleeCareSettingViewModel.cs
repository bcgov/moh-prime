using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollees
{
    public class PaperEnrolleeCareSettingViewModel
    {
        public IEnumerable<int> CareSettings { get; set; }
        public IEnumerable<HealthAuthorityCode> HealthAuthorities { get; set; }
    }
}
