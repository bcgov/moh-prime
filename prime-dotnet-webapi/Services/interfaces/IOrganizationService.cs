using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetOrganizationsAsync(int? partyId = null);
        Task<Organization> GetOrganizationAsync(int organizationId);
        Task<int> CreateOrganizationAsync(Party signingAuthority);
        Task<int> UpdateOrganizationAsync(int organizationId, OrganizationUpdateModel updatedOrganization);
        Task<int> UpdateCompletedAsync(int organizationId);
        Task DeleteOrganizationAsync(int organizationId);
        Task<Organization> SubmitRegistrationAsync(int organizationId);
        Task<Organization> GetOrganizationNoTrackingAsync(int organizationId);
        Task<int> AcceptCurrentOrganizationAgreementAsync(int organizationId);
        Task<Organization> GetOrganizationByPartyIdAsync(int partyId);
        Task<SignedAgreementDocument> AddSignedAgreementAsync(int organizationId, Guid documentGuid, string filename);
        Task<IEnumerable<SignedAgreementDocument>> GetSignedAgreementsAsync(int organizationId);
        Task<SignedAgreementDocument> GetLatestSignedAgreementAsync(int organizationId);
    }
}
