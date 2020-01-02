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
        private readonly IPrivilegeService _privilegeService;

        public DefaultEnrolmentCertificateService(
            ApiDbContext context, IHttpContextAccessor httpContext, IPrivilegeService privilegeService)
            : base(context, httpContext)
        {
            _privilegeService = privilegeService;
        }

        public async Task<EnrolmentCertificate> GetEnrolmentCertificateAsync(Guid accessTokenId)
        {
            var enrollee = await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Id == accessTokenId && t.Active)
                .Select(t => t.Enrollee)
                .SingleOrDefaultAsync();
            if (enrollee == null)
            {
                return null;
            }

            // Add privileges to Enrollee
            enrollee.Privileges = _privilegeService.GetPrivilegesForEnrollee(enrollee);

            // TODO Refactor this shortcut. This is only for POC of this service.
            try
            {
                var token = await _context.EnrolmentCertificateAccessTokens
                    .SingleOrDefaultAsync(t => t.Id == accessTokenId);
                token.ViewCount++;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // ¯\_(ツ)_/¯
            }

            EnrolmentCertificate enrolmentCertificate = EnrolmentCertificate.Create(enrollee);

            // Add organization types names to certificate to display organizations without lookup tables
            enrolmentCertificate.OrganizationTypes = GetOrganizationTypesForEnrolleeQuery(enrollee.Id);

            return enrolmentCertificate;
        }

        public async Task<EnrolmentCertificateAccessToken> CreateCertificateAccessTokenAsync(Enrollee enrollee)
        {
            // Add privileges to Enrollee
            enrollee.Privileges = _privilegeService.GetPrivilegesForEnrollee(enrollee);

            EnrolmentCertificateAccessToken token = new EnrolmentCertificateAccessToken()
            {
                Enrollee = enrollee,
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

        public async Task<IEnumerable<EnrolmentCertificateAccessToken>> GetCertificateAccessTokensForUserIdAsync(Guid userId)
        {
            return await _context.EnrolmentCertificateAccessTokens
                .Where(t => t.Enrollee.UserId == userId && t.Active)
                .ToListAsync();
        }

        private ICollection<OrganizationType> GetOrganizationTypesForEnrolleeQuery(int? enrolleeId)
        {
            return _context.OrganizationTypes
                        .Include(ot => ot.Organizations)
                        .Where(ot => ot.Organizations.Any(o => o.EnrolleeId == enrolleeId))
                        .ToList();
        }
    }
}
