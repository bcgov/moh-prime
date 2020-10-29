using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolleeSubmissionService
    {
        Task<IEnumerable<Submission>> GetEnrolleeSubmissionsAsync(int enrolleeId);

        Task<Submission> GetEnrolleeSubmissionAsync(int enrolleeSubmissionId);

        Task<Submission> GetEnrolleeSubmissionBeforeDateAsync(int enrolleeId, DateTimeOffset dateTime);

        Task CreateEnrolleeSubmissionAsync(Enrollee enrollee);
    }
}
