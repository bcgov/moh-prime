using System;
using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IDocumentService
    {
        Task<string> GetDownloadTokenForBusinessLicenceDocument(int siteId);
        Task<string> GetDownloadTokenForSignedAgreementDocument(int agreementId);
        Task<string> GetDownloadTokenForSelfDeclarationDocument(int selfDeclarationDocumentId);
        Task<string> GetDownloadTokenForIdentificationDocument(int identificationDocumentId);
        Task<string> GetDownloadTokenForEnrolleeAdjudicationDocument(int enrolleeAdjudicationDocumentId);
        Task<string> GetDownloadTokenForSiteAdjudicationDocument(int siteAdjudicationDocumentId);
        Task<string> FinalizeDocumentUpload(Guid documentGuid, string filePath);
    }
}
