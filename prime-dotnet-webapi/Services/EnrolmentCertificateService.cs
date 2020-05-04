using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class EnrolmentCertificateService : BaseService, IEnrolmentCertificateService
    {
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;

        public EnrolmentCertificateService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IAccessTermService accessTermService,
            IEnrolleeProfileVersionService enroleeProfileVersionService)
            : base(context, httpContext)
        {
            _accessTermService = accessTermService;
            _enroleeProfileVersionService = enroleeProfileVersionService;
        }

        public async Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId)
        {
            var token = await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Id == accessTokenId)
                .Include(t => t.Enrollee)
                    .ThenInclude(e => e.EnrolleeOrganizationTypes)
                        .ThenInclude(org => org.OrganizationType)
                .SingleOrDefaultAsync();

            if (token == null || token.Enrollee == null)
            {
                return null;
            }

            await UpdateTokenMetadataAsync(token);

            if (token.Active)
            {
                var enrolleeId = token.EnrolleeId;
                var acceptedAccessTerm = await _accessTermService.GetMostRecentAcceptedEnrolleesAccessTermAsync(enrolleeId);
                if (acceptedAccessTerm != null)
                {
                    var enrolleeProfileHistory = await _enroleeProfileVersionService.GetEnrolleeProfileVersionBeforeDateAsync(enrolleeId, (DateTimeOffset)acceptedAccessTerm?.AcceptedDate);

                    if (enrolleeProfileHistory != null)
                    {
                        // Load JSON profile history to an Enrollee Object
                        // TODO refactor this to work with different versions of enrollee as model changes from JSON object
                        var enrolleeHistory = enrolleeProfileHistory.ProfileSnapshot.ToObject<Enrollee>();

                        // Add the organization type to each organization from JSON profile history
                        foreach (var org in enrolleeHistory.EnrolleeOrganizationTypes)
                        {
                            org.OrganizationType = await _context.OrganizationTypes.SingleAsync(o => o.Code == org.OrganizationTypeCode);
                        }

                        return EnrolmentCertificate.Create(enrolleeHistory, token.Enrollee);
                    }
                }
            }

            return null;
        }

        public async Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenAsync(Enrollee enrollee)
        {
            EnrolmentCertificateAccessToken token = new EnrolmentCertificateAccessToken()
            {
                Enrollee = enrollee,
                ViewCount = 0,
                Expires = DateTimeOffset.Now.Add(EnrolmentCertificateAccessToken.Lifespan),
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

            // TODO: View limit has been removed temporarily
            if (/*token.ViewCount >= MAX_VIEWS ||*/ DateTimeOffset.Now > token.Expires)
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
