using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolmentCertificateAccessService : BaseService, IEnrolmentCertificateAccessService
    {
        public DefaultEnrolmentCertificateAccessService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<EnrolmentCertificateAccessToken> CreateEnrolmentCertificateAccessTokenAsync(int enrolleeId)
        {
            EnrolmentCertificateAccessToken token = new EnrolmentCertificateAccessToken()
            {
                EnrolleeId = enrolleeId,
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
    }
}
