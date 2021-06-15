using System;
using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services
{
    /// <summary>
    /// This services manages the tokens used for persistent external access to a Document, such as the links to Business Licenses sent to HIBC during site registration.
    /// </summary>
    public interface IDocumentAccessTokenService
    {
        Task<DocumentAccessToken> GetDocumentAccessTokenAsync(Guid documentAccessTokenId);
        Task<DocumentAccessToken> CreateDocumentAccessTokenAsync(Guid documentGuid);
        Task DeleteDocumentAccessTokenAsync(Guid documentAccessTokenId);
    }
}
