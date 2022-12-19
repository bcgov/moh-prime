using System.Collections.Generic;
using Prime.Models;

namespace Prime.Configuration.Database
{
    public class SelfDeclarationTypeConfiguration : SeededTable<SelfDeclarationType>
    {
        public override IEnumerable<SelfDeclarationType> SeedData
        {
            get
            {
                return new[] {
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.Conviction,            Name = "Has Conviction"            , SortingNumber = 1 },
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.RegistrationSuspended, Name = "Has Registration Suspended", SortingNumber = 2 },
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.DisciplinaryAction,    Name = "Has Disciplinary Action"   , SortingNumber = 4 },
                    new SelfDeclarationType { Code = (int)SelfDeclarationTypeCode.PharmaNetSuspended,    Name = "Has PharmaNet Suspended"   , SortingNumber = 3 },
                };
            }
        }
    }
}
