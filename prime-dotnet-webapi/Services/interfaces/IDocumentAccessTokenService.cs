using System;
using System.IO;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IDocumentAccessTokenService
    {
        Task<DocumentAccessToken> GetDocumentAccessTokenAsync(Guid documentAccessTokenId);
        Task<DocumentAccessToken> CreateDocumentAccessTokenAsync(Guid documentGuid);
        Task DeleteDocumentAccessTokenAsync(Guid documentAccessTokenId);
    }
}
