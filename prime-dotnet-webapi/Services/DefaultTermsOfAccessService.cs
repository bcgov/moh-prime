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

        public async Task<bool> SetEnrolleeTermsOfAccessAsync(Enrollee enrollee)
        {
            var termsOfAccess = enrollee.TermsOfAccess;

            // TODO assign the most current global clause
            termsOfAccess.GlobalClause = await _context.GlobalClauses
                .OrderByDescending(g => g.EffectiveDate)
                .FirstOrDefaultAsync();
            
            // TODO determine the enrollee user type
            // TODO assign the most recent user clause
            // TODO determine licence class clauses
            // TODO seed with lorem ipsum licence class clauses
            // TODO determine limits and conditions clauses
            // TODO seed with lorem ipsum limit and conditions clauses

            throw new System.NotImplementedException();
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
    }
}
