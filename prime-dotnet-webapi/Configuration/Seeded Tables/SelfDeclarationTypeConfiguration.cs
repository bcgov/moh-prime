using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration
{
    public class SelfDeclarationTypeConfiguration : SeededTable<SelfDeclarationType>
    {
        public override IEnumerable<SelfDeclarationType> SeedData
        {
            get
            {
                return new[] {
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.Conviction,            Name = "Has Conviction"             },
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.RegistrationSuspended, Name = "Has Registration Suspended" },
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.DisciplinaryAction,    Name = "Has Disciplinary Action"    },
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.PharmaNetSuspended,    Name = "Has PharmaNet Suspended"    },
                };
            }
        }
    }
}
