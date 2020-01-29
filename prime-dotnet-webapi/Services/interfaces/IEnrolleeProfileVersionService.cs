using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolleeProfileVersionService
    {
        Task<IEnumerable<EnrolleeProfileVersion>> GetEnrolleeProfileVersionsAsync(int enrolleeId);

        Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeProfileVersionId);

        Task CreateEnrolleeProfileVersionAsync(Enrollee enrollee);
    }
}
