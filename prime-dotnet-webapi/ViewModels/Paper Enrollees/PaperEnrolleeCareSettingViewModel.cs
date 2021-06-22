using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels.PaperEnrollee
{
    public class PaperEnrolleeCareSettingViewModel
    {
        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }

        public ICollection<EnrolleeHealthAuthority> EnrolleeHealthAuthorities { get; set; }
    }
}
