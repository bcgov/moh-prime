using System;
using System.IO;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    public interface IDocumentAccessTokenService
    {
        Task<DocumentAccessToken> CreateDocumentAccessToken(Guid documentGuid);
        Task DeleteDocumentAccessToken(Guid documentAccessTokenId);
    }
}
