using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IDocumentService
    {
        Task<string> GetDownloadTokenForLatestBusinessLicenceDocument(int siteId);
        Task<string> GetDownloadTokenForLatestSignedAgreementDocument(int organizationId);

        Task<Stream> GetStreamForLatestBusinessLicenceDocument(int siteId);
        Task<Stream> GetStreamForLatestSignedAgreementDocument(int organizationId);

        Task<string> GetDownloadTokenForSelfDeclarationDocument(int selfDeclarationDocumentId);
    }
}
