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
        Task<bool> EnrolleeExistsAsync(int enrolleeId);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> GpidExistsAsync(string gpid);
        Task<EnrolleeStub> GetEnrolleeStubAsync(string username);
        Task<PermissionsRecord> GetPermissionsRecordAsync(int enrolleeId);
        Task<string> GetActiveGpidAsync(string username);
        Task<HpdidLookup> GetActiveGpidDetailAsync(string username);
        Task<EnrolleeViewModel> GetEnrolleeAsync(int enrolleeId);
        Task<PaginatedList<EnrolleeListViewModel>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null, ClaimsPrincipal user = null);
        Task<EnrolleeNavigation> GetAdjacentEnrolleeIdAsync(int enrolleeId);
        Task<int> CreateEnrolleeAsync(EnrolleeCreateModel enrollee);
        Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeUpdateModel enrolleeProfile, bool profileCompleted = false);
        Task DeleteEnrolleeAsync(int enrolleeId);
        Task<AccessAgreementNoteViewModel> GetAccessAgreementNoteAsync(int enrolleeId);
        Task<CareSettingViewModel> GetCareSettingsAsync(int enrolleeId);
        Task<IEnumerable<CertificationViewModel>> GetCertificationsAsync(int enrolleeId);
        Task<IEnumerable<EnrolleeDeviceProviderViewModel>> GetEnrolleeDeviceProvidersAsync(int enrolleeId);
        Task<IEnumerable<UnlistedCertificationViewModel>> GetUnlistedCertificationsAsync(int enrolleeId);
        Task<IEnumerable<EnrolleeRemoteUserViewModel>> GetEnrolleeRemoteUsersAsync(int enrolleeId);
        Task<IEnumerable<OboSiteViewModel>> GetOboSitesAsync(int enrolleeId);
        Task<IEnumerable<RemoteAccessLocationViewModel>> GetRemoteAccessLocationsAsync(int enrolleeId);
        Task<IEnumerable<RemoteAccessSiteViewModel>> GetRemoteAccessSitesAsync(int enrolleeId);
        Task<IEnumerable<SelfDeclarationViewModel>> GetSelfDeclarationsAsync(int enrolleeId);
        Task<IEnumerable<SelfDeclarationDocumentViewModel>> GetSelfDeclarationDocumentsAsync(int enrolleeId, bool includeHidden);
        Task AssignToaAgreementType(int enrolleeId, AgreementType? agreementType);
        Task<IEnumerable<EnrolmentStatusAdminViewModel>> GetEnrolmentStatusesAsync(int enrolleeId);
        Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params StatusType[] statusCodesToCheck);
        Task<bool> IsEnrolleeProfileCompleteAsync(int enrolleeId);
        Task<IEnumerable<EnrolleeNoteViewModel>> GetEnrolleeAdjudicatorNotesAsync(int enrolleeId);
        Task<EnrolleeNoteViewModel> GetEnrolleeAdjudicatorNoteAsync(int enrolleeId, int noteId);
        Task<EnrolleeNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId);
        Task<IBaseEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, int adminId, IBaseEnrolleeNote newNote);
        Task UpdateEnrolleeAdjudicator(int enrolleeId, int? adminId = null);
        Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEventsAsync(int enrolleeId, IEnumerable<int> businessEventTypeCodes);
        Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids);
        Task<EnrolleeLookup> GpidLookupAsync(GpidLookupOptions option);
        Task<GpidValidationResponse> ValidateProvisionerDataAsync(string gpid, GpidValidationParameters parameters);
        Task<EnrolmentStatusReference> CreateEnrolmentStatusReferenceAsync(int statusId, int adminId);
        Task<EnrolmentStatusReference> AddAdjudicatorNoteToReferenceIdAsync(int statusId, int noteId);
        Task<SelfDeclarationDocument> AddSelfDeclarationDocumentAsync(int enrolleeId, SelfDeclarationDocument selfDeclarationDocument);
        Task<IdentificationDocument> CreateIdentificationDocument(int enrolleeId, Guid documentGuid, string filename);
        Task<EnrolleeAdjudicationDocument> AddEnrolleeAdjudicationDocumentAsync(int enrolleeId, Guid documentGuid, int adminId);
        Task<IEnumerable<EnrolleeAdjudicationDocument>> GetEnrolleeAdjudicationDocumentsAsync(int enrolleeId);
        Task<EnrolleeAdjudicationDocument> GetEnrolleeAdjudicationDocumentAsync(int documentId);
        Task DeleteEnrolleeAdjudicationDocumentAsync(int documentId);
        Task<EnrolmentStatusAdminViewModel> GetEnrolleeCurrentStatusAsync(int enrolleeId);
        Task<EnrolleeNotification> CreateEnrolleeNotificationAsync(int EnrolleeNoteId, int adminId, int assigneeId);
        Task RemoveEnrolleeNotificationAsync(int enrolleeNotificationId);
        Task<EnrolleeNotification> GetEnrolleeNotificationAsync(int enrolleeNotificationId);
        Task<IEnumerable<EnrolleeNoteViewModel>> GetNotificationsAsync(int enrolleeId, int adminId);
        Task RemoveNotificationsAsync(int enrolleeId);
        Task<IEnumerable<int>> GetNotifiedEnrolleeIdsForAdminAsync(ClaimsPrincipal user);
        Task<IEnumerable<string>> GetEnrolleeEmails(BulkEmailType bulkEmailType);
        Task<Credential> GetCredentialAsync(int enrolleeId);
        Task<EnrolleeAbsence> CreateEnrolleeAbsenceAsync(int enrolleeId, EnrolleeAbsenceViewModel createModel);
        Task<IEnumerable<EnrolleeAbsenceViewModel>> GetEnrolleeAbsencesAsync(int enrolleeId, bool includesPast);
        Task<EnrolleeAbsenceViewModel> GetCurrentEnrolleeAbsenceAsync(int enrolleeId);
        Task EndCurrentEnrolleeAbsenceAsync(int enrolleeId);
        Task DeleteFutureEnrolleeAbsenceAsync(int enrolleeId, int absenceId);
        Task<string> GetAdjudicatorIdirForEnrolleeAsync(int enrolleeId);
        Task UpdateDateOfBirthAsync(int enrolleeId, DateTime dateOfBirth);
        Task UpdateCertificationPrefix(int cretId, string prefix);
        Task<IEnumerable<string>> FilterToUpdatedAsync(IEnumerable<string> hpdids, DateTimeOffset updatedSince);
    }
}
