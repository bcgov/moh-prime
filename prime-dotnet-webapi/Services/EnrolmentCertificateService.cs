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
        private const int EXPIRY_DAYS = 7;
        private static readonly TimeSpan TOKEN_LIFESPAN = TimeSpan.FromDays(EXPIRY_DAYS);
        private const int MAX_VIEWS = 3;
        private readonly IAccessTermService _accessTermService;
        private readonly IEnrolleeProfileVersionService _enroleeProfileVersionService;

        private ImmutableDictionary<string, string> PharmaNetProvisioners = new Dictionary<string, string>()
        {
            { "CareConnect", "CareConnect@phsa.ca" },
            { "Excelleris", "support@excelleris.com" },
            { "iClinic", "help@iclinicemr.com" },
            { "MediNet", "prime@medinet.ca" },
            { "Plexia", "service@plexia.ca" }
        }.ToImmutableDictionary();

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
                    .ThenInclude(e => e.Organizations)
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
                        foreach (var org in enrolleeHistory.Organizations)
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
                Expires = DateTimeOffset.Now.Add(TOKEN_LIFESPAN),
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

        public string[] GetPharmaNetProvisionerNames()
        {
            return PharmaNetProvisioners.Keys.ToArray();
        }

        public string GetPharmaNetProvisionerEmail(string provisionerName)
        {
            string recipientEmail;

            PharmaNetProvisioners.TryGetValue(provisionerName, out recipientEmail);

            return recipientEmail;
        }

        public int GetMaxViews()
        {
            return MAX_VIEWS;
        }

        public int GetExpiryDays()
        {
            return EXPIRY_DAYS;
        }

        private async Task UpdateTokenMetadataAsync(EnrolmentCertificateAccessToken token)
        {
            if (!token.Active)
            {
                return;
            }

            if (token.ViewCount >= MAX_VIEWS || DateTimeOffset.Now > token.Expires)
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
