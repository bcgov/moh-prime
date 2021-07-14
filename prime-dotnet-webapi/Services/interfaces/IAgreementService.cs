using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IAgreementService
    {
        Task<IEnumerable<AgreementVersionListViewModel>> GetLatestAgreementVersionsAsync(AgreementGroup? group);
        Task<AgreementVersionViewModel> GetAgreementVersionAsync(int agreementVersionId);
        Task<int> GetLatestAgreementVersionIdOfTypeAsync(AgreementType type);
        Task<SignedAgreementDocument> AddSignedAgreementDocumentAsync(int agreementId, Guid documentGuid);
        Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId);
    }
}
