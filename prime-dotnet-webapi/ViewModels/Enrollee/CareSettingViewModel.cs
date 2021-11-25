using System.Collections.Generic;

using Prime.Models;

namespace Prime.ViewModels
{
    public class CareSettingViewModel
    {
        // TODO: these should be codes rather than the XRef objects
        public ICollection<EnrolleeCareSetting> EnrolleeCareSettings { get; set; }
        public ICollection<EnrolleeHealthAuthority> EnrolleeHealthAuthorities { get; set; }
    }
}
