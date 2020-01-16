using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolleeProfileHistoryService
    {
        Task<IEnumerable<EnrolleeProfileHistory>> GetEnrolleeProfileHistoriesAsync(int enrolleeId);

        Task<EnrolleeProfileHistory> GetEnrolleeProfileHistoryAsync(int enrolleeId, int enrolleeProfileHistoryId);

        Task<int> CreateEnrolleeProfileHistoryAsync(Enrollee enrollee);
    }
}
