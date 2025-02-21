using System.Collections.Generic;
using Prime.Models;

namespace Prime.ViewModels
{
    public class LicenseViewModel
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public string Prefix { get; set; }

        public bool Manual { get; set; }

        public bool Validate { get; set; }

        public bool NamedInImReg { get; set; }

        public bool LicensedToProvideCare { get; set; }

        public bool AllowRequestRemoteAccess { get; set; }

        public PrescriberIdType? PrescriberIdType { get; set; }

        public ICollection<CollegeLicense> CollegeLicenses { get; set; }

        public ICollection<RemoteAccessTypeLicense> RemoteAccessTypeLicenses { get; set; }

        public bool Multijurisdictional { get; set; }
    }
}
