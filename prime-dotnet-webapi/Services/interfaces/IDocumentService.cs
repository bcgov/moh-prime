using System;
using System.IO;
using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IDocumentService
    {
        Task<string> GetDownloadTokenForLatestBusinessLicenceDocument(int siteId);
        Task<string> GetDownloadTokenForLatestSignedAgreementDocument(int organizationId);
        Task<string> GetDownloadTokenForSelfDeclarationDocument(int selfDeclarationDocumentId);
        Task<Stream> GetStreamForLatestBusinessLicenceDocument(int siteId);
        Task<Stream> GetStreamForLatestSignedAgreementDocument(int organizationId);
    }
}
