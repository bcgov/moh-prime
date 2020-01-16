using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolleeProfileVersionService
    {
        Task<IEnumerable<EnrolleeProfileVersion>> GetEnrolleeProfileVersionsAsync(int enrolleeId);

        Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeId, int enrolleeProfileHistoryId);

        Task<int> CreateEnrolleeProfileVersionAsync(Enrollee enrollee);
    }
}
