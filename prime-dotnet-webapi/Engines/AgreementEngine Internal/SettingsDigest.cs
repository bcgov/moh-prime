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

        public SettingsDigest(IEnumerable<int> careSettingCodes)
        {
            if (!careSettingCodes.Any())
            {
                throw new ArgumentException("Must have one or more care settings", nameof(careSettingCodes));
            }

            Multiple = careSettingCodes.Count() > 1;
            HasCommunityPharmacy = careSettingCodes.Any(cs => cs == (int)CareSettingType.CommunityPharmacy);
        }
    }
}
