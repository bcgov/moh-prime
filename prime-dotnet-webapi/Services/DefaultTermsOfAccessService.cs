using System;
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

        /**
         * Get the most recent terms of access for an enrollee.
         */
        public async Task<TermsOfAccess> GetTermsOfAccessAsync(Enrollee enrollee)
        {
            var termsOfAccess = new TermsOfAccess { Enrollee = enrollee };

            termsOfAccess.GlobalClause = await GetGlobalClause();
            termsOfAccess.UserClause = await GetUserClause(enrollee);
            termsOfAccess.TermsOfAccessLicenseClassClauses
                .AddRange(await GetTermsOfAccessLicenseClassClauses(enrollee, termsOfAccess));
            termsOfAccess.TermsOfAccessLimitsAndConditionsClauses
                .AddRange(await GetTermsOfAccessLimitsAndConditionsClauses(enrollee, termsOfAccess));

            return termsOfAccess;
        }

        public async Task SetEnrolleeTermsOfAccessAsync(Enrollee enrollee)
        {
            var termsOfAccess = await GetTermsOfAccessAsync(enrollee);

            termsOfAccess.EffectiveDate = DateTime.Now;

            _context.Add(termsOfAccess);

            await _context.SaveChangesAsync();
        }

        /**
         * Get the most recent terms ACCEPTED terms of access for an enrollee.
         */
        public async Task<TermsOfAccess> GetEnrolleeTermsOfAccessAsync(int enrolleeId)
        {
            var termsOfAccess = await _context.TermsOfAccess
                .Include(t => t.GlobalClause)
                .Include(t => t.UserClause)
                .Include(t => t.TermsOfAccessLicenseClassClauses)
                    .ThenInclude(tacc => tacc.LicenseClassClause)
                .Include(t => t.TermsOfAccessLimitsAndConditionsClauses)
                    .ThenInclude(talc => talc.LimitsAndConditionsClause)
                .Where(t => t.EnrolleeId == enrolleeId)
                .OrderByDescending(t => t.EffectiveDate)
                .FirstOrDefaultAsync();

            termsOfAccess.LicenseClassClauses = termsOfAccess.TermsOfAccessLicenseClassClauses
                .Select(talc => talc.LicenseClassClause).ToList();

            termsOfAccess.LimitsAndConditionsClauses = termsOfAccess.TermsOfAccessLimitsAndConditionsClauses
                .Select(talc => talc.LimitsAndConditionsClause).ToList();

            return termsOfAccess;
        }

        private async Task<GlobalClause> GetGlobalClause()
        {
            return await _context.GlobalClauses
                .OrderByDescending(g => g.EffectiveDate)
                .FirstOrDefaultAsync();
        }

        private async Task<UserClause> GetUserClause(Enrollee enrollee)
        {
            var userType = enrollee.EnrolleeClassification;

            return await _context.UserClauses
                .Where(g => g.EnrolleeClassification == enrollee.EnrolleeClassification)
                .OrderByDescending(g => g.EffectiveDate)
                .FirstOrDefaultAsync();
        }

        // TODO no provided logic for how license class clauses are chosen
        private async Task<IEnumerable<TermsOfAccessLicenseClassClause>> GetTermsOfAccessLicenseClassClauses(Enrollee enrollee, TermsOfAccess termsOfAccess)
        {
            var licenseClassClauses = await _context.LicenseClassClauses
                .Take(2)
                .ToListAsync();

            return licenseClassClauses.Select(lcc => new TermsOfAccessLicenseClassClause { TermsOfAccess = termsOfAccess, LicenseClassClause = lcc });
        }

        // TODO no provided logic for how limits and conditions clauses are chosen
        private async Task<IEnumerable<TermsOfAccessLimitsAndConditionsClause>> GetTermsOfAccessLimitsAndConditionsClauses(Enrollee enrollee, TermsOfAccess termsOfAccess)
        {
            var limitsAndConditionsClauses = await _context.LimitsAndConditionsClauses
                .Take(1)
                .ToListAsync();

            return limitsAndConditionsClauses.Select(lacc => new TermsOfAccessLimitsAndConditionsClause
            {
                TermsOfAccess = termsOfAccess,
                LimitsAndConditionsClause = lacc
            });
        }
    }
}
