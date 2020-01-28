using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IEnrolleeService
    {
        Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId);

        Task<bool> EnrolleeExistsAsync(int enrolleeId);

        Task<bool> EnrolleeUserIdExistsAsync(Guid userId);

        Task<Enrollee> GetEnrolleeAsync(int enrolleeId);

        Task<IEnumerable<Enrollee>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null);

        Task<int?> CreateEnrolleeAsync(Enrollee enrollee);

        Task<int> UpdateEnrolleeAsync(Enrollee enrollee, bool profileCompleted = false);

        Task DeleteEnrolleeAsync(int enrolleeId);

        Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolleeId);

        Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId);

        Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolleeId, Status status);

        bool IsStatusChangeAllowed(Status startingStatus, Status endingStatus);

        Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params short[] statusCodesToCheck);

        Task<IEnumerable<AdjudicatorNote>> GetEnrolleeAdjudicatorNotesAsync(Enrollee enrollee);

        Task<AdjudicatorNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, AdjudicatorNote adjudicatorNote);

        Task<IEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IEnrolleeNote newNote);
    }
}
