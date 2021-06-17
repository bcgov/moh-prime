using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DocumentAccessTokenService : BaseService, IDocumentAccessTokenService
    {
        public DocumentAccessTokenService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<DocumentAccessToken> GetDocumentAccessTokenAsync(Guid documentAccessTokenId)
        {
            return await _context.DocumentAccessTokens
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == documentAccessTokenId);
        }

        public async Task<DocumentAccessToken> CreateDocumentAccessTokenAsync(Guid documentGuid)
        {
            var token = new DocumentAccessToken
            {
                DocumentGuid = documentGuid
            };

            _context.Add(token);
            if (await _context.SaveChangesAsync() < 1)
            {
                throw new InvalidOperationException("Could not create Document Access Token.");
            }

            return token;
        }

        public async Task DeleteDocumentAccessTokenAsync(Guid documentAccessTokenId)
        {
            var token = await _context.DocumentAccessTokens
                .SingleOrDefaultAsync(d => d.Id == documentAccessTokenId);

            if (token != null)
            {
                _context.DocumentAccessTokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }
    }
}
