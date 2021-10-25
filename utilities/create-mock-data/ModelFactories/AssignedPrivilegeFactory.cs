using Bogus;
using Prime.Models;

namespace PrimeTests.ModelFactories
{
    public class AssignedPrivilegeFactory : Faker<AssignedPrivilege>
    {
        public AssignedPrivilegeFactory(Enrollee owner) : this(owner, -1) { }

        public AssignedPrivilegeFactory(Enrollee owner, int privilegeId)
        {
//            this.SetBaseRules();

            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.PrivilegeId, f => privilegeId == -1 ? f.PickRandom(PrivilegeLookup.All).Id : privilegeId);

            Ignore(x => x.Privilege);
        }
    }
}
