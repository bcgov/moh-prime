using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IEnrolleeService
    {
        Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId, bool excludeDecline = false);

        Task<bool> EnrolleeExistsAsync(int enrolleeId);

        Task<bool> UserIdExistsAsync(Guid userId);

        Task<bool> GpidExistsAsync(string gpid);

        Task<PermissionsRecord> GetPermissionsRecordAsync(int enrolleeId);

        Task<Enrollee> GetEnrolleeAsync(int enrolleeId, bool isAdmin = false);

        Task<Enrollee> GetEnrolleeNoTrackingAsync(int enrolleeId);

        Task<IEnumerable<EnrolleeListViewModel>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null);

        Task<int> CreateEnrolleeAsync(Enrollee enrollee);

        Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeUpdateModel enrolleeProfile, bool profileCompleted = false);

        Task DeleteEnrolleeAsync(int enrolleeId);

        Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId);

        Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params StatusType[] statusCodesToCheck);

        Task<IEnumerable<AdjudicatorNote>> GetEnrolleeAdjudicatorNotesAsync(Enrollee enrollee);

        Task<AdjudicatorNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId);

        Task<IEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IEnrolleeNote newNote);

        Task<int> GetEnrolleeCountAsync();

        Task<Enrollee> UpdateEnrolleeAdjudicator(int enrolleeId, Admin admin = null);

        Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEvents(int enrolleeId);

        Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids);

        Task<GpidValidationResponse> ValidateProvisionerDataAsync(string gpid, GpidValidationParameters parameters);

        Task<EnrolmentStatusReference> CreateEnrolmentStatusReferenceAsync(int statusId, int adminId);

        Task<EnrolmentStatusReference> AddAdjudicatorNoteToReferenceIdAsync(int statusId, int noteId);

        Task<SelfDeclarationDocument> AddSelfDeclarationDocumentAsync(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument);

        Task ImportEnrollees(IEnumerable<Enrollee> enrollees);
    }
}
