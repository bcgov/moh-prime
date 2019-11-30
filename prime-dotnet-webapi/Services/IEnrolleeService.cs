using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolleeService
    {
        Task<IEnumerable<Enrollee>> GetEnrolleesAsync();

        Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId);

        bool EnrolleeExists(int enrolleeId);

        Task<Enrollee> GetEnrolleeAsync(int enrolleeId);

        Task<IEnumerable<Enrollee>> GetEnrolleesAsync(EnrolmentSearchOptions searchOptions);

        Task<int?> CreateEnrolleeAsync(Enrollee enrollee);

        Task<int> UpdateEnrolleeAsync(Enrollee enrollee);

        Task DeleteEnrolleeAsync(int enrolleeId);

        Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolleeId);

        Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId);

        Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolleeId, Status status);

        bool IsStatusChangeAllowed(Status startingStatus, Status endingStatus);

        Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, short statusCodeToCheck);
    }
}
