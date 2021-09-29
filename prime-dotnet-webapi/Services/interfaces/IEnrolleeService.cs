using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;
using Prime.Models.VerifiableCredentials;
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
        Task<EnrolleeNavigation> GetAdjacentEnrolleeIdAsync(int enrolleeId);
        Task<int> CreateEnrolleeAsync(EnrolleeCreateModel enrollee);
        Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeUpdateModel enrolleeProfile, bool profileCompleted = false);
        Task DeleteEnrolleeAsync(int enrolleeId);
        Task AssignToaAgreementType(int enrolleeId, AgreementType? agreementType);
        Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId);
        Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params StatusType[] statusCodesToCheck);
        Task<IEnumerable<EnrolleeNoteViewModel>> GetEnrolleeAdjudicatorNotesAsync(int enrolleeId);
        Task<EnrolleeNoteViewModel> GetEnrolleeAdjudicatorNoteAsync(int enrolleeId, int noteId);
        Task<EnrolleeNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId);
        Task<IBaseEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, int adminId, IBaseEnrolleeNote newNote);
        Task<int> GetEnrolleeCountAsync();
        Task UpdateEnrolleeAdjudicator(int enrolleeId, int? adminId = null);
        Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEventsAsync(int enrolleeId, IEnumerable<int> businessEventTypeCodes);
        Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids);
        Task<GpidValidationResponse> ValidateProvisionerDataAsync(string gpid, GpidValidationParameters parameters);
        Task<EnrolmentStatusReference> CreateEnrolmentStatusReferenceAsync(int statusId, int adminId);
        Task<EnrolmentStatusReference> AddAdjudicatorNoteToReferenceIdAsync(int statusId, int noteId);
        Task<SelfDeclarationDocument> AddSelfDeclarationDocumentAsync(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument);
        Task<IdentificationDocument> CreateIdentificationDocument(int enrolleeId, Guid documentGuid, string filename);
        Task<EnrolleeAdjudicationDocument> AddEnrolleeAdjudicationDocumentAsync(int enrolleeId, Guid documentGuid, int adminId);
        Task<IEnumerable<EnrolleeAdjudicationDocument>> GetEnrolleeAdjudicationDocumentsAsync(int enrolleeId);
        Task<EnrolleeAdjudicationDocument> GetEnrolleeAdjudicationDocumentAsync(int documentId);
        Task DeleteEnrolleeAdjudicationDocumentAsync(int documentId);
        Task<EnrolmentStatus> GetEnrolleeCurrentStatusAsync(int enrolleeId);
        Task<EnrolleeNotification> CreateEnrolleeNotificationAsync(int EnrolleeNoteId, int adminId, int assigneeId);
        Task RemoveEnrolleeNotificationAsync(int enrolleeNotificationId);
        Task<EnrolleeNotification> GetEnrolleeNotificationAsync(int enrolleeNotificationId);
        Task<IEnumerable<EnrolleeNoteViewModel>> GetNotificationsAsync(int enrolleeId, int adminId);
        Task RemoveNotificationsAsync(int enrolleeId);
        Task<IEnumerable<int>> GetNotifiedEnrolleeIdsForAdminAsync(ClaimsPrincipal user);
        Task<IEnumerable<string>> GetEnrolleeEmails(BulkEmailType bulkEmailType);
        Task<Credential> GetCredentialAsync(int enrolleeId);
        Task<EnrolleeAbsence> CreateEnrolleeAbsenceAsync(int enrolleeId, DateTime startTimestamp, DateTime endTimestamp);
        Task<EnrolleeAbsenceViewModel> GetEnrolleeAbsenceAsync(int enrolleeId);
        Task<EnrolleeAbsenceViewModel> GetCurrentEnrolleeAbsenceAsync(int enrolleeId);
        Task EndEnrolleeAbsenceAsync(int enrolleeId);
        Task DeleteFutureEnrolleeAbsenceAsync(int enrolleeId, int absenceId);
    }
}
