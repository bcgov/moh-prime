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
        }
    }
}

// TODO
// Enrollee
// EnrolleeId
// Privilege
// PrivilegeId
