using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IPrivilegeService
    {
        Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee);
    }
}
