using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IOrganizationService
    {
        Task<bool> OrganizationExistsAsync(int organizationId);
        Task<IEnumerable<OrganizationSearchViewModel>> GetOrganizationsAsync(OrganizationSearchOptions search);
        Task<IEnumerable<OrganizationListViewModel>> GetOrganizationsByPartyIdAsync(int partyId);
        Task<Organization> GetOrganizationAsync(int organizationId);
        Task<Organization> GetOrganizationByPecAsync(string pec);
        Task<int> CreateOrganizationAsync(int signingAuthorityId);
        Task<int> UpdateOrganizationAsync(int organizationId, OrganizationUpdateModel updatedOrganization);
        Task<int> UpdateCompletedAsync(int organizationId);
        Task DeleteOrganizationAsync(int organizationId);
        Task<Organization> GetOrganizationNoTrackingAsync(int organizationId);
        Task<Agreement> EnsureUpdatedOrgAgreementAsync(int organizationId, int careSettingCode, int signingAuthorityId);
        Task AcceptOrgAgreementAsync(int organizationId, int agreementId);
        Task<SignedAgreementDocument> AddSignedAgreementAsync(int organizationId, int agreementId, Guid documentGuid);
        Task<SignedAgreementDocument> GetLatestSignedAgreementAsync(int organizationId);
        Task<IEnumerable<CareSettingType>> GetCareSettingCodesForPendingTransferAsync(int organizationId, int signingAuthorityId);
        Task FinalizeTransferAsync(int organizationId);
        AgreementType OrgAgreementTypeForSiteSetting(int careSettingCode);

        /// <summary>
        /// Returns <c>true</c> if can successfully switch signing authority, <c>false</c> otherwise.
        /// </summary>
        Task<bool> SwitchSigningAuthorityAsync(int organizationId, int newSigningAuthorityId);
    }
}
