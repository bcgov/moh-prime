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
        Task<IEnumerable<AgreementVersionListViewModel>> GetAgreementVersionsAsync(AgreementVersionFilters? filters);
        Task<AgreementVersionViewModel> GetAgreementVersionAsync(int agreementVersionId);
        Task<int> GetLatestAgreementVersionIdOfTypeAsync(AgreementType type);
        Task<SignedAgreementDocument> AddSignedAgreementDocumentAsync(int agreementId, Guid documentGuid);
        Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId);
    }
}
