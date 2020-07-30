using System;
using System.Linq;
using System.Collections.Generic;

using Prime.Models;

namespace Prime
{
    public static class EnrolleeExtensions
    {
        public static bool HasCareSetting(this Enrollee enrollee, CareSettingType type)
        {
            enrollee.ThrowIfNull(nameof(enrollee));
            if (enrollee.EnrolleeOrganizationTypes == null) throw new ArgumentException($"{nameof(enrollee.EnrolleeOrganizationTypes)} cannnot be null", nameof(enrollee));

            return enrollee.EnrolleeOrganizationTypes.Any(o => o.IsType(CareSettingType.CommunityPharmacy));
        }

        static public bool IsRegulatedUser(this Enrollee enrollee)
        {
            enrollee.ThrowIfNull(nameof(enrollee));
            if (enrollee.Certifications == null) throw new ArgumentException($"{nameof(enrollee.Certifications)} cannnot be null", nameof(enrollee));
            if (enrollee.Certifications.Any(c => c.License == null)) throw new ArgumentException($"{nameof(enrollee.Certifications)} must have Licenses loaded", nameof(enrollee));

            return enrollee.Certifications.Any(cert => cert.License.RegulatedUser);
        }
    }
}
