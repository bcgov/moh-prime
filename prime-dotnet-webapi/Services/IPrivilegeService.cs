using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPrivilegeService
    {
        Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee);

        Task<ICollection<AssignedPrivilege>> GetAssignedPrivilegesForEnrolleeAsync(int? enrolleeId);

        Task<ICollection<Privilege>> GetPrivilegesForEnrolleeAsync(Enrollee enrollee);

    }
}
