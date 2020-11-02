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

        Task<EnrolleeViewModel> GetEnrolleeAsync(int enrolleeId, bool isAdmin = false);

        Task<Enrollee> GetEnrolleeNoTrackingAsync(int enrolleeId);

        Task<IEnumerable<EnrolleeListViewModel>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null);

        Task<int> CreateEnrolleeAsync(EnrolleeCreateModel enrollee);

        Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeUpdateModel enrolleeProfile, bool profileCompleted = false);

        Task DeleteEnrolleeAsync(int enrolleeId);

        Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId);

        Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params StatusType[] statusCodesToCheck);

        Task<IEnumerable<EnrolleeNoteViewModel>> GetEnrolleeAdjudicatorNotesAsync(int enrolleeId);

        Task<EnrolleeNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId);

        Task<IBaseEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IBaseEnrolleeNote newNote);

        Task<int> GetEnrolleeCountAsync();

        Task<Enrollee> UpdateEnrolleeAdjudicator(int enrolleeId, int? adminId = null);

        Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEvents(int enrolleeId);

        Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids);

        Task<GpidValidationResponse> ValidateProvisionerDataAsync(string gpid, GpidValidationParameters parameters);

        Task<EnrolmentStatusReference> CreateEnrolmentStatusReferenceAsync(int statusId, int adminId);

        Task<EnrolmentStatusReference> AddAdjudicatorNoteToReferenceIdAsync(int statusId, int noteId);

        Task<SelfDeclarationDocument> AddSelfDeclarationDocumentAsync(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument);

        Task<IdentificationDocument> CreateIdentificationDocument(int enrolleeId, Guid documentGuid, string filename);
    }
}
