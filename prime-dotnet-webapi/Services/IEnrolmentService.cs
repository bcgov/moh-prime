using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolmentService
    {
        bool EnrolmentExists(int enrolmentId);

        Task<Enrolment> GetEnrolmentAsync(int enrolmentId);

        Task<Enrolment> GetEnrolmentForUserIdAsync(
            Guid userId);

        Task<IEnumerable<Enrolment>> GetEnrolmentsAsync(EnrolmentSearchOptions searchOptions);

        Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(
            Guid userId);

        Task<int?> CreateEnrolmentAsync(Enrolment enrolment);

        Task<int> UpdateEnrolmentAsync(Enrolment enrolment);

        Task DeleteEnrolmentAsync(int enrolmentId);

        Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolmentId);

        Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolmentId);

        Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolmentId, Status status);

        bool IsStatusChangeAllowed(Status startingStatus, Status endingStatus);

        Task<bool> IsEnrolmentInStatusAsync(int enrolmentId, short statusCodeToCheck);

    }
}
