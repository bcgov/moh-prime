using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolmentCertificateService : BaseService, IEnrolmentCertificateService
    {
        public DefaultEnrolmentCertificateService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId)
        {
            throw new NotImplementedException();
        }

        public async Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenAsync(Guid userId)
        {
            EnrolmentCertificateAccessToken token = new EnrolmentCertificateAccessToken()
            {
                UserId = userId,
                ViewCount = 0,
                Active = true
            };
            _context.EnrolmentCertificateAccessTokens.Add(token);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Enrolment Certificate access token.");
            }

            return token;
        }

        public async Task<EnrolmentCertificateAccessToken> GetCertificateAccessTokenAsync(Guid accessTokenId)
        {
            return await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Id == accessTokenId)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<EnrolmentCertificateAccessToken>> GetCertificateAccessTokensForUserIdAsync(Guid userId)
        {
            return await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
    }
}
