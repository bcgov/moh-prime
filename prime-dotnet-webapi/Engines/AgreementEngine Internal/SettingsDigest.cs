using System;
using System.Linq;
using System.Collections.Generic;

using Prime.Models;

namespace Prime.Engines.AgreementEngineInternal
{
    public class SettingsDigest
    {
        public bool Multiple { get; set; }
        public bool HasCommunityPharmacy { get; set; }

        public SettingsDigest(IEnumerable<CareSetting> careSettings)
        {
            if (!careSettings.Any())
            {
                throw new ArgumentException("Must have one or more care settings", nameof(careSettings));
            }

            Multiple = careSettings.Count() > 1;
            HasCommunityPharmacy = careSettings.Any(cs => cs.IsType(CareSettingType.CommunityPharmacy));
        }
    }
}
