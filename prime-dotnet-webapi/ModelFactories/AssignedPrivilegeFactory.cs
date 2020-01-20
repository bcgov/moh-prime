using System;
using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class AssignedPrivilegeFactory : Faker<AssignedPrivilege>
    {
        public AssignedPrivilegeFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Privilege, f => f.PickRandom(PrivilegeLookup.All));
            RuleFor(x => x.PrivilegeId, (f, x) => x.Privilege.Id);
        }
    }
}
