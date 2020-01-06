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
            // TODO assign the most current global clause
            // TODO determine the enrollee user type
            // TODO assign the most recent user clause
            // TODO determine licence class clauses?
            // TODO seed with lorem ipsum licence class clauses
            // TODO determine limits and conditions clauses?
            // TODO seed with lorem ipsum limit and conditions clauses

            throw new System.NotImplementedException();
        }

        public async Task<TermsOfAccess> GetEnrolleeTermsOfAccessAsync(int enrolleeId)
        {
            // TODO initially providing only minimal extraction of terms of access for API response
            return await _context.TermsOfAccess
                .Include(t => t.GlobalClause)
                .Include(t => t.UserClause)
                .Include(t => t.TermsOfAccessLicenseClassClauses)
                .Include(t => t.TermsOfAccessLimitsAndConditionsClauses)
                .Where(t => t.EnrolleeId == enrolleeId)
                .SingleOrDefaultAsync();
        }
    }
}
