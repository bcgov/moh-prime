using System.Linq;
using System.Collections.Generic;

using Prime.Models.Agreements.Internal;
using System.Reflection;

namespace Prime.Models
{
    public enum AgreementGroup
    {
        Enrollee = 1,
        Organization = 2,
    }

    public static class AgreementGroupExtensions
    {
        public static IEnumerable<AgreementType> AgreementTypes(this AgreementGroup group)
        {
            return typeof(AgreementType)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.GetCustomAttributes(false)
                    .OfType<AgreementGroupsAttribute>()
                    .Single()
                    .HasGroup(group))
                .Select(x => (AgreementType)x.GetValue(null))
                .ToList();
        }
    }
}
