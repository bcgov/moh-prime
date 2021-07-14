using System;
using System.Linq;
using System.Collections.Generic;

namespace Prime.Models.Agreements.Internal
{
    public class AgreementGroupsAttribute : Attribute
    {
        public IEnumerable<AgreementGroup> Groups { get; set; }

        public AgreementGroupsAttribute(params AgreementGroup[] groups)
        {
            if (groups == null || !groups.Any())
            {
                throw new ArgumentException($"Must specify at least 1 group in {nameof(AgreementGroupsAttribute)}");
            }

            Groups = groups;
        }

        public bool HasGroup(AgreementGroup group)
        {
            return Groups.Contains(group);
        }
    }
}
