using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPrivilegeService
    {
        Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee);

        ICollection<AssignedPrivilege> GetAssignedPrivilegesForEnrollee(int? enrolleeId);

        ICollection<Privilege> GetPrivilegesForEnrollee(Enrollee enrollee);

    }
}
