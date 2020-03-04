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
        private static readonly TimeSpan TOKEN_LIFESPAN = TimeSpan.FromDays(7);
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
                        // TODO find simpler way to load relationships from JSON object
                        var organizations = enrolleeHistory.Organizations;

                        for (var i = 0; i < enrolleeHistory.Organizations.Count; i++)
                        {
                            var result = _context.Organizations
                                .Where(o => o.Id == enrolleeHistory.Organizations.ElementAt(i).Id)
                                .Include(o => o.OrganizationType)
                                .FirstOrDefault();

                            enrolleeHistory.Organizations.ElementAt(i).OrganizationType = result.OrganizationType;
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
                Expires = new DateTimeOffset(DateTime.Today.Add(TOKEN_LIFESPAN), new TimeSpan(0, -7, 0, 0, 0)),
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

        public string GetPharmaNetProvisionerEmail(string provisionerName, ref string otherEmail)
        {
            string recipientEmail;

            if (provisionerName == "Other")
            {
                recipientEmail = otherEmail;
                otherEmail = "";
            }
            else
            {
                PharmaNetProvisioners.TryGetValue(provisionerName, out recipientEmail);
            }

            return recipientEmail;
        }

        private async Task UpdateTokenMetadataAsync(EnrolmentCertificateAccessToken token)
        {
            if (!token.Active)
            {
                return;
            }

            if (token.ViewCount >= MAX_VIEWS || new DateTimeOffset(DateTime.Today, new TimeSpan(0, -7, 0, 0, 0)) > token.Expires)
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
