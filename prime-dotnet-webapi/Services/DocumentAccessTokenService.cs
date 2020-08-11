using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<DocumentAccessToken> GetDocumentAccessAsync(Guid documentAccessTokenId)
        {
            return await _context.DocumentAccessToken.SingleOrDefaultAsync(e => e.Id == documentAccessTokenId);
        }

        public async Task<DocumentAccessToken> CreateDocumentAccessTokenAsync(Guid documentGuid)
        {
            var documentAccessToken = new DocumentAccessToken { DocumentGuid = documentGuid };
            _context.Add(documentAccessToken);

            if (await _context.SaveChangesAsync() < 1)
            {
                throw new InvalidOperationException("Could not create Document Access Token.");
            }

            return documentAccessToken;
        }

        public async Task DeleteDocumentAccessTokenAsync(Guid documentAccessTokenId)
        {
            var documentAccessToken = await _context.DocumentAccessToken.SingleOrDefaultAsync(e => e.Id == documentAccessTokenId);

            if (documentAccessToken == null)
            {
                return;
            }

            _context.DocumentAccessToken.Remove(documentAccessToken);
            await _context.SaveChangesAsync();
        }
    }
}
