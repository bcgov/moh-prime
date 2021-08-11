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
        /// <summary>
        /// Returns an HTML diff of the text of the two specified Agreement Versions.
        /// </summary>
        /// <param name="compareViewModel"></param>
        Task<string> CompareAgreementsAsync(AgreementCompareViewModel compareViewModel);
        Task<SignedAgreementDocument> AddSignedAgreementDocumentAsync(int agreementId, Guid documentGuid);
        Task<SignedAgreementDocument> GetSignedAgreementDocumentAsync(int agreementId);
    }
}
