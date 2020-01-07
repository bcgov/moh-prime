using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultTermsOfAccessService: BaseService,  ITermsOfAccessService
    {
        public DefaultTermsOfAccessService(
            ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        { }

        public async Task SetEnrolleeTermsOfAccessAsync(Enrollee enrollee)
        {
            var termsOfAccess = enrollee.TermsOfAccess;

            // TODO assign the most current global clause
            termsOfAccess.GlobalClause = await _context.GlobalClauses
                .OrderByDescending(g => g.EffectiveDate)
                .FirstOrDefaultAsync();

            // TODO determine the enrollee user type
            //var userType = DetermineEnrolleeUserType(enrollee);
            // TODO assign the most recent user clause

            // TODO determine licence class clauses
            // TODO assign the licence class clause(s)

            // TODO determine limits and conditions clauses
            // TODO assign the limits and conditions clause(s)

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
                    .ThenInclude(talc => talc.LimitsConditionsClause)
                .Where(t => t.EnrolleeId == enrolleeId)
                .SingleOrDefaultAsync();
        }

        // TODO private method to determine the user type based of the enrollee
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
