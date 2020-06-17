using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetOrganizationsAsync();
        Task<IEnumerable<Organization>> GetOrganizationsAsync(int partyId);
        Task<Organization> GetOrganizationAsync(int organizationId);
        Task<int> CreateOrganizationAsync(Party party);
        Task<int> UpdateOrganizationAsync(int organizationId, Organization organization, bool isCompleted = false);
        Task DeleteOrganizationAsync(int organizationId);
        Task<Organization> SubmitRegistrationAsync(int organizationId);
        Task<Organization> GetOrganizationNoTrackingAsync(int organizationId);
        Task<int> AcceptCurrentOrganizationAgreementAsync(int organizationId);
        Task<Organization> GetOrganizationByPartyIdAsync(int partyId);
        Task<SignedAgreement> AddSignedAgreementAsync(int organizationId, Guid documentGuid, string filename);
        Task<IEnumerable<SignedAgreement>> GetSignedAgreementsAsync(int organizationId);
        Task<SignedAgreement> GetLatestSignedAgreementAsync(int organizationId);
    }
}
