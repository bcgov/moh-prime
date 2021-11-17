using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public class EnrolmentCertificateService : BaseService, IEnrolmentCertificateService
    {
        public EnrolmentCertificateService(
            ApiDbContext context,
            ILogger<EnrolmentCertificateService> logger)
            : base(context, logger)
        { }

        public async Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId)
        {
            var token = await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Id == accessTokenId)
                .Include(t => t.Enrollee)
                    .ThenInclude(e => e.EnrolleeCareSettings)
                        .ThenInclude(org => org.CareSetting)
                .Include(t => t.Enrollee)
                    .ThenInclude(e => e.Agreements)
                .SingleOrDefaultAsync();

            if (token == null || token.Enrollee == null)
            {
                return null;
            }

            await UpdateTokenMetadataAsync(token);

            if (token.Active)
            {
                return EnrolmentCertificate.Create(token.Enrollee);
            }

            return null;
        }

        public async Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenAsync(int enrolleeId)
        {
            EnrolmentCertificateAccessToken token = new EnrolmentCertificateAccessToken()
            {
                EnrolleeId = enrolleeId,
                ViewCount = 0,
                Expires = DateTimeOffset.Now.Add(EnrolmentCertificateAccessToken.Lifespan),
                Active = true
            };

            _context.EnrolmentCertificateAccessTokens.Add(token);
            await _context.SaveChangesAsync();

            return token;
        }

        public async Task<IEnumerable<EnrolmentCertificateAccessToken>> GetCertificateAccessTokensForUserIdAsync(Guid userId)
        {
            return await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Enrollee.UserId == userId
                    && t.Active)
                .ToListAsync();
        }

        private async Task UpdateTokenMetadataAsync(EnrolmentCertificateAccessToken token)
        {
            if (!token.Active)
            {
                return;
            }

            // TODO: View limit has been removed temporarily
            if (/*token.ViewCount >= MAX_VIEWS ||*/ DateTimeOffset.Now > token.Expires)
            {
                token.Active = false;
            }
            else
            {
                token.ViewCount++;
            }

            await _context.SaveChangesAsync();
        }
    }
}
