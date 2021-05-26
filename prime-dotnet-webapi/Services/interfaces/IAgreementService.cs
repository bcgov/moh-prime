using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IAgreementService
    {
        Task<Agreement> GetEnrolleeAgreementAsync(int enrolleeId, int agreementId, bool includeText = false);

        Task<IEnumerable<Agreement>> GetEnrolleeAgreementsAsync(int enrolleeId, AgreementFilters filters);

        Task CreateEnrolleeAgreementAsync(int enrolleeId);

        Task<Agreement> GetCurrentAgreementAsync(int enrolleeId);

        Task AcceptCurrentEnrolleeAgreementAsync(int enrolleeId);

        Task ExpireCurrentEnrolleeAgreementAsync(int enrolleeId);

        Task<IEnumerable<AgreementViewModel>> GetOrgAgreementsAsync(int organizationId);

        Task<AgreementViewModel> GetOrgAgreementAsync(int organizationId, int agreementId, bool asEncodedPdf = false);

        Task<string> RenderOrgAgreementHtmlAsync(AgreementType type, string orgName, DateTimeOffset? acceptedDate, bool forPdf, bool withSignature = false);

        Task<string> GetSignableOrgAgreementAsync(int organizationId, AgreementType type);

        Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId);

        Task<SignedAgreementDocument> AddSignedAgreementDocumentAsync(int agreementId, Guid documentGuid);

        Task<IEnumerable<AgreementVersionListViewModel>> GetLatestEnrolleeAgreementVersionsAsync();
        Task<AgreementVersionViewModel> GetAgreementVersionById(int agreementId);
    }
}
