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
        private static readonly TimeSpan TOKEN_LIFESPAN = TimeSpan.FromDays(7);
        private static readonly int MAX_VIEWS = 3;

        private readonly IPrivilegeService _privilegeService;

        public DefaultEnrolmentCertificateService(
            ApiDbContext context, IHttpContextAccessor httpContext, IPrivilegeService privilegeService)
            : base(context, httpContext)
        {
            _privilegeService = privilegeService;
        }

        public async Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId)
        {
            var token = await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Id == accessTokenId)
                .Include(t => t.Enrollee)
                .SingleOrDefaultAsync();

            if (token == null || token.Enrollee == null)
            {
                return null;
            }

            enrollee.Privileges = await _privilegeService.GetPrivilegesForEnrolleeAsync(enrollee);
            await UpdateTokenMetadataAsync(token);

            if (token.Active)
            {
                return EnrolmentCertificate.Create(token.Enrollee);
            }
            else
            {
                return null;
            }
        }

        public async Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenAsync(Enrollee enrollee)
        {
            EnrolmentCertificateAccessToken token = new EnrolmentCertificateAccessToken()
            {
                Enrollee = enrollee,
                ViewCount = 0,
                Expires = DateTime.Today.Add(TOKEN_LIFESPAN),
                Active = true
            };
            _context.EnrolmentCertificateAccessTokens.Add(token);

            if (await _context.SaveChangesAsync() < 1)
            {
                throw new InvalidOperationException("Could not create Enrolment Certificate access token.");
            }

            return token;
        }

        public async Task<IEnumerable<EnrolmentCertificateAccessToken>> GetCertificateAccessTokensForUserIdAsync(Guid userId)
        {
            return await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Enrollee.UserId == userId && t.Active)
                .ToListAsync();
        }

        private async Task UpdateTokenMetadataAsync(EnrolmentCertificateAccessToken token)
        {
            if (!token.Active)
            {
                return;
            }

            if (token.ViewCount >= MAX_VIEWS)
            {
                token.Active = false;
            }
            else if (DateTime.Today > token.Expires)
            {
                token.Active = false;
            }
            else
            {
                token.ViewCount++;
            }

            if (await _context.SaveChangesAsync() < 1)
            {
                throw new InvalidOperationException("Could not update Enrolment Certificate access token.");
            }
        }
    }
}
