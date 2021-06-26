using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IAgreementService
    {
        Task<IEnumerable<AgreementVersionListViewModel>> GetLatestEnrolleeAgreementVersionsAsync();
        Task<AgreementVersionViewModel> GetAgreementVersionById(int agreementId);
        Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId);
        Task<SignedAgreementDocument> AddSignedAgreementDocumentAsync(int agreementId, Guid documentGuid);
    }
}
