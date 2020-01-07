using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultTermsOfAccessService : BaseService, ITermsOfAccessService
    {
        public DefaultTermsOfAccessService(
            ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        { }

        // TODO type of license classes needs to be added to the license clases clause for selection
        public async Task SetEnrolleeTermsOfAccessAsync(Enrollee enrollee)
        {
            var termsOfAccess = new TermsOfAccess { Enrollee = enrollee };

            termsOfAccess.GlobalClause = await _context.GlobalClauses
                .OrderByDescending(g => g.EffectiveDate)
                .FirstOrDefaultAsync();

            // TODO should be based on user type
            // var userType = DetermineEnrolleeUserType(enrollee);
            termsOfAccess.UserClause = await _context.UserClauses
                // TODO add enum user type to UserClause
                // .Where(g => g.UserType == userType)
                .OrderByDescending(g => g.EffectiveDate)
                .FirstOrDefaultAsync();

            var licenseClassClauses = await _context.LicenseClassClauses
                // TODO how are these chosen?
                .Take(2)
                .ToListAsync();
            var termsOfAccessLicenseClassClauses = licenseClassClauses
                .Select(lcc => new TermsOfAccessLicenseClassClause { TermsOfAccess = termsOfAccess, LicenseClassClause = lcc });
            termsOfAccess.TermsOfAccessLicenseClassClauses
                .AddRange(termsOfAccessLicenseClassClauses);

            var limitsAndConditionsClauses = await _context.LimitsAndConditionsClauses
                // TODO how are these chosen?
                .Take(1)
                .ToListAsync();
            var termsOfAccessLimitsAndConditionsClauses = limitsAndConditionsClauses
                .Select(lacc => new TermsOfAccessLimitsAndConditionsClause { TermsOfAccess = termsOfAccess, LimitsAndConditionsClause = lacc });
            termsOfAccess.TermsOfAccessLimitsAndConditionsClauses
                .AddRange(termsOfAccessLimitsAndConditionsClauses);

            _context.Add(termsOfAccess);

            await _context.SaveChangesAsync();
        }

        public async Task<TermsOfAccess> GetEnrolleeTermsOfAccessAsync(int enrolleeId)
        {
            return await _context.TermsOfAccess
                .Include(t => t.GlobalClause)
                .Include(t => t.UserClause)
                .Include(t => t.TermsOfAccessLicenseClassClauses)
                    .ThenInclude(tacc => tacc.LicenseClassClause)
                .Include(t => t.TermsOfAccessLimitsAndConditionsClauses)
                    .ThenInclude(talc => talc.LimitsAndConditionsClause)
                .Where(t => t.EnrolleeId == enrolleeId)
                .SingleOrDefaultAsync();
        }

        private string DetermineEnrolleeUserType(Enrollee enrollee)
        {
            // TODO what are the rules around user types outside of certifications?
            return (enrollee.Certifications.Count > 0)
                // TODO add enum for user types MOA and OBO strings found throughout application
                ? "MOA"
                : "OBO";
        }

        // TODO private method to determine the license class clause(s)
        // TODO private method to determine the limits and conditions clause(s)
    }
}
