using System.Linq;
using System.Collections.Generic;

namespace Prime.Models.Api
{
    public class AgreementVersionFilters
    {
        public bool Latest { get; set; }
        public AgreementType? Type { get; set; }
        public AgreementGroup? Group { get; set; }
        public bool HasTypeFilter { get => Type.HasValue || Group.HasValue; }

        public IEnumerable<AgreementType> FilteredTypes()
        {
            if (Type.HasValue)
            {
                return new[] { Type.Value };
            }
            else if (Group.HasValue)
            {
                return Group.Value.AgreementTypes();
            }
            else
            {
                return Enumerable.Empty<AgreementType>();
            }
        }
    }
}
