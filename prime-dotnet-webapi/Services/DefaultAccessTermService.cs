using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultAccessTermService : BaseService, IAccessTermService
    {
        public DefaultAccessTermService(
            ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        { }

        /**
         * Get the most recent terms of access for an enrollee.
         */
        public async Task<AccessTerm> GetAccessTermAsync(Enrollee enrollee)
        {
            var accessTerms = new AccessTerm { Enrollee = enrollee };

            accessTerms.GlobalClause = await GetGlobalClause();
            accessTerms.UserClause = await GetUserClause(enrollee);
            accessTerms.AccessTermLicenseClassClauses
                .AddRange(await GetAccessTermLicenseClassClauses(enrollee, accessTerms));
            accessTerms.LimitsConditionsClause = await GetAccessTermLimitsConditionsClause(enrollee);

            return accessTerms;
        }

        public async Task CreateEnrolleeAccessTermAsync(Enrollee enrollee)
        {
            var accessTerm = await GetAccessTermAsync(enrollee);

            accessTerm.CreatedDate = DateTime.Now;
            accessTerm.AcceptedDate = null;

            _context.Add(accessTerm);

            await _context.SaveChangesAsync();
        }

        public async Task SetAcceptedDateForTermsOfAccessAsync(Enrollee enrollee)
        {
            var termsOfAccess = await _context.AccessTerms
                .Where(toa => toa.EnrolleeId == enrollee.Id)
                .OrderByDescending(toa => toa.AcceptedDate)
                .FirstOrDefaultAsync();

            termsOfAccess.AcceptedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        /**
         * Get the most recent terms ACCEPTED terms of access for an enrollee.
         */
        public async Task<AccessTerm> GetEnrolleeAccessTermsAsync(int enrolleeId)
        {
            var accessTerms = await _context.AccessTerms
                .Include(t => t.GlobalClause)
                .Include(t => t.UserClause)
                .Include(t => t.AccessTermLicenseClassClauses)
                    .ThenInclude(tacc => tacc.LicenseClassClause)
                .Include(t => t.LimitsConditionsClause)
                .Where(t => t.EnrolleeId == enrolleeId)
                .OrderByDescending(t => t.AcceptedDate)
                .FirstOrDefaultAsync();

            accessTerms.LicenseClassClauses = accessTerms.AccessTermLicenseClassClauses
                .Select(talc => talc.LicenseClassClause).ToList();

            return accessTerms;
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
        private async Task<IEnumerable<AccessTermLicenseClassClause>> GetAccessTermLicenseClassClauses(Enrollee enrollee, AccessTerm accessTerms)
        {
            var licenseClassClauses = await _context.LicenseClassClauses
                .Take(2)
                .ToListAsync();

            return licenseClassClauses.Select(lcc => new AccessTermLicenseClassClause { AccessTerm = accessTerms, LicenseClassClause = lcc });
        }

        private async Task<LimitsConditionsClause> GetAccessTermLimitsConditionsClause(Enrollee enrollee)
        {
            var lastNote = await _context.AccessAgreementNotes
                                .Where(n => n.EnrolleeId == enrollee.Id)
                                .OrderByDescending(n => n.CreatedTimeStamp)
                                .FirstOrDefaultAsync();

            var newClause = new LimitsConditionsClause
            {
                EnrolleeId = lastNote.EnrolleeId,
                Clause = null,
                EffectiveDate = new DateTime()
            };

            if (lastNote != null)
            {
                newClause = new LimitsConditionsClause
                {
                    EnrolleeId = lastNote.EnrolleeId,
                    Clause = lastNote.Note,
                    EffectiveDate = new DateTime()
                };

                _context.LimitsConditionsClauses.Add(newClause);
                await _context.SaveChangesAsync();
            }

            return newClause;

        }
    }
}
