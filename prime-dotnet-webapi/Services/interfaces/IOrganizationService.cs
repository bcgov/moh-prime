using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IOrganizationService
    {
        Task<bool> OrganizationExistsAsync(int organizationId);
        Task<IEnumerable<OrganizationListViewModel>> GetOrganizationsByPartyIdAsync(int partyId);
        Task<IEnumerable<OrganizationAdminListViewModel>> GetOrganizationAdminListViewAsync(string searchText);
        Task<OrganizationAdminListViewModel> GetOrganizationAdminListViewByIdAsync(int id);
        Task<Organization> GetOrganizationAsync(int organizationId);
        Task<List<Organization>> GetOrganizationByNameAsync(string organizationName);
        Task<int> GetOrganizationSigningAuthorityIdAsync(int organizationId);
        Task<Organization> GetOrganizationByPecAsync(string pec);
        Task<int> CreateOrganizationAsync(int signingAuthorityId);
        Task<int> UpdateOrganizationAsync(int organizationId, OrganizationUpdateModel updatedOrganization);
        Task<int> UpdateCompletedAsync(int organizationId);
        Task DeleteOrganizationAsync(int organizationId);
        Task ArchiveOrganizationAsync(int organizationId);
        Task RestoreArchivedOrganizationAsync(int organizationId);
        Task<Organization> GetOrganizationNoTrackingAsync(int organizationId);
        Task<Agreement> EnsureUpdatedOrgAgreementAsync(int organizationId, int careSettingCode, int signingAuthorityId);
        Task AcceptOrgAgreementAsync(int organizationId, int agreementId);
        Task<SignedAgreementDocument> AddSignedAgreementAsync(int organizationId, int agreementId, Guid documentGuid, string filename = "");
        Task<SignedAgreementDocument> GetLatestSignedAgreementAsync(int organizationId);
        Task<IEnumerable<CareSettingType>> GetCareSettingCodesForPendingTransferAsync(int organizationId, int signingAuthorityId);
        Task FinalizeTransferAsync(int organizationId);
        AgreementType OrgAgreementTypeForSiteSetting(int careSettingCode);
        Task SwitchSigningAuthorityAsync(int organizationId, int newSigningAuthorityId);
        Task RemoveUnsignedOrganizationAgreementsAsync(int organizationId);
        Task<bool> IsOrganizationTransferCompleteAsync(int organizationId);
        Task FlagPendingTransferIfOrganizationAgreementsRequireSignaturesAsync(int organizationId);
        Task<int> UpdateMissingRegistrationIds();
        Task SetOrganizationDetailEditable(int organizationId);
    }
}
