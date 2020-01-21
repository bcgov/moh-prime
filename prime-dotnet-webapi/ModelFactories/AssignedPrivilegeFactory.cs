using System;
using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class AssignedPrivilegeFactory : Faker<AssignedPrivilege>
    {
        public AssignedPrivilegeFactory(Enrollee owner, int privilegeId = -1)
        {
            this.SetBaseRules();

            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Privilege, f => privilegeId == -1 ? f.PickRandom(PrivilegeLookup.All) : PrivilegeLookup.ById(privilegeId));
            RuleFor(x => x.PrivilegeId, (f, x) => x.Privilege.Id);
        }
    }
}
