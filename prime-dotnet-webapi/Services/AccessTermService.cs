using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class AccessTermService : BaseService, IAccessTermService
    {
        private static readonly TimeSpan ACCESS_TERM_EXPIRY = TimeSpan.FromDays(365);
        public AccessTermService(
            ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        { }

        /**
         * Get access term for an enrollee by id if accepted.
         */
        public async Task<AccessTerm> GetEnrolleesAccessTermAsync(int enrolleeId, int accessTermId)
        {
            var accessTerm = await _context.AccessTerms
                .Include(at => at.GlobalClause)
                .Include(at => at.UserClause)
                .Include(at => at.AccessTermLicenseClassClauses)
                    .ThenInclude(tacc => tacc.LicenseClassClause)
                .Include(at => at.LimitsConditionsClause)
                .Where(at => at.EnrolleeId == enrolleeId)
                .Where(at => at.Id == accessTermId)
                .Where(at => at.AcceptedDate != null)
                .FirstOrDefaultAsync();

            accessTerm.LicenseClassClauses = accessTerm.AccessTermLicenseClassClauses
                .Select(talc => talc.LicenseClassClause).ToList();

            return accessTerm;
        }

        /**
         * Get the most recent terms !ACCEPTED access term for an enrollee.
         */
        public async Task<AccessTerm> GetMostRecentNotAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            var accessTerm = await _context.AccessTerms
                .Include(at => at.GlobalClause)
                .Include(at => at.UserClause)
                .Include(at => at.AccessTermLicenseClassClauses)
                    .ThenInclude(tacc => tacc.LicenseClassClause)
                .Include(at => at.LimitsConditionsClause)
                .Where(at => at.EnrolleeId == enrolleeId)
                .Where(at => at.AcceptedDate == null)
                .OrderByDescending(at => at.CreatedDate)
                .FirstOrDefaultAsync();

            accessTerm.LicenseClassClauses = accessTerm.AccessTermLicenseClassClauses
                .Select(talc => talc.LicenseClassClause).ToList();

            return accessTerm;
        }

        /**
         * Get the most recent terms ACCEPTED access term for an enrollee.
         */
        public async Task<AccessTerm> GetMostRecentAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            var accessTerm = await _context.AccessTerms
                .Include(at => at.GlobalClause)
                .Include(at => at.UserClause)
                .Include(at => at.AccessTermLicenseClassClauses)
                    .ThenInclude(tacc => tacc.LicenseClassClause)
                .Include(at => at.LimitsConditionsClause)
                .Where(at => at.EnrolleeId == enrolleeId)
                .Where(at => at.AcceptedDate != null)
                .OrderByDescending(at => at.CreatedDate)
                .FirstOrDefaultAsync();

            accessTerm.LicenseClassClauses = accessTerm.AccessTermLicenseClassClauses
                .Select(talc => talc.LicenseClassClause).ToList();

            return accessTerm;
        }

        /**
         *  Get list of ACCEPTED access terms for an enrollee
         */
        public async Task<IEnumerable<AccessTerm>> GetAcceptedAccessTerms(int enrolleeId, int year)
        {
            var accessTerms = await _context.AccessTerms
                .Include(at => at.GlobalClause)
                .Include(at => at.UserClause)
                .Include(at => at.AccessTermLicenseClassClauses)
                    .ThenInclude(tacc => tacc.LicenseClassClause)
                .Include(at => at.LimitsConditionsClause)
                .Where(at => at.EnrolleeId == enrolleeId)
                .Where(at => at.AcceptedDate != null)
                .OrderByDescending(at => at.AcceptedDate)
                .ToListAsync();

            if (year != 0)
            {
                accessTerms = accessTerms
                    .Where(at => at.AcceptedDate.HasValue && at.AcceptedDate.Value.Year == year)
                    .ToList();
            }

            accessTerms.ForEach(at =>
            {
                at.LicenseClassClauses = at.AccessTermLicenseClassClauses
                    .Select(talc => talc.LicenseClassClause)
                    .ToList();
            });

            return accessTerms;
        }

        public async Task CreateEnrolleeAccessTermAsync(Enrollee enrollee)
        {
            var accessTerm = await GenerateAccessTermAsync(enrollee);

            accessTerm.CreatedDate = DateTimeOffset.Now;

            _context.Add(accessTerm);

            await _context.SaveChangesAsync();
        }

        public async Task AcceptCurrentAccessTermAsync(Enrollee enrollee)
        {
            var accessTerm = await _context.AccessTerms
                .Where(at => at.EnrolleeId == enrollee.Id)
                .OrderByDescending(at => at.AcceptedDate)
                .FirstAsync();

            accessTerm.AcceptedDate = DateTimeOffset.Now;
            // Add an Expiry Date of one year in the future.
            accessTerm.ExpiryDate = DateTimeOffset.Now.Add(ACCESS_TERM_EXPIRY);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AccessTermExistsOnEnrolleeAsync(int accessTermId, int enrolleeId)
        {
            return await _context.AccessTerms
                .Where(at => at.Id == accessTermId)
                .Where(at => at.EnrolleeId == enrolleeId)
                .AnyAsync();
        }

        /// <summary>
        /// Returns true if the enrollees' most recent accepted access term has no newer versions
        /// </summary>
        public async Task<bool> IsCurrentByEnrolleeAsync(Enrollee enrollee)
        {
            var current = true;

            var accessTerm = await _context.AccessTerms
                .Include(at => at.AccessTermLicenseClassClauses)
                .Where(at => at.EnrolleeId == enrollee.Id)
                .Where(at => at.AcceptedDate != null)
                .OrderByDescending(at => at.AcceptedDate)
                .FirstOrDefaultAsync();

            if (accessTerm != null)
            {
                var currentAccessTerm = await GenerateAccessTermAsync(enrollee);

                if (accessTerm.GlobalClauseId != currentAccessTerm.GlobalClause.Id
                   || accessTerm.UserClauseId != currentAccessTerm.UserClause.Id)
                {
                    current = false;
                }

                foreach (var lcc in accessTerm.AccessTermLicenseClassClauses)
                {
                    if (currentAccessTerm.AccessTermLicenseClassClauses.FindAll(c => c.LicenseClassClause.Id == lcc.LicenseClassClauseId).Count == 0)
                    {
                        current = false;
                    }
                }
            }
            else
            {
                return false;
            }

            return current;
        }

        /**
         * Generates an Access Term based off of the enrollee
         */
        private async Task<AccessTerm> GenerateAccessTermAsync(Enrollee enrollee)
        {
            var accessTerms = new AccessTerm { Enrollee = enrollee };

            accessTerms.GlobalClause = await GetGlobalClause();
            accessTerms.UserClause = await GetUserClause(enrollee);
            accessTerms.AccessTermLicenseClassClauses
                .AddRange(await GetAccessTermLicenseClassClauses(enrollee, accessTerms));
            accessTerms.LimitsConditionsClause = await GetAccessTermLimitsConditionsClause(enrollee);

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
            var userType = PrimeConstants.PRIME_OBO;

            if (enrollee.Certifications.Count > 0)
            {
                foreach (var cert in enrollee.Certifications)
                {
                    if (cert.License.DefaultPrivileges.Any(dp => dp.PrivilegeId == Privilege.RU_CODE))
                    {
                        userType = PrimeConstants.PRIME_RU;
                    }
                }
            }

            return await _context.UserClauses
                .Where(g => g.EnrolleeClassification == userType)
                .OrderByDescending(g => g.EffectiveDate)
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<AccessTermLicenseClassClause>> GetAccessTermLicenseClassClauses(Enrollee enrollee, AccessTerm accessTerms)
        {
            var licenseClassClauses = new List<LicenseClassClause>();

            if (enrollee.Certifications.Count > 0)
            {
                foreach (var org in enrollee.Organizations)
                {
                    foreach (var cert in enrollee.Certifications)
                    {
                        var mappings = await _context.LicenseClassClauseMappings
                            .Include(m => m.LicenseClassClause)
                            .Where(m => m.LicenseCode == cert.LicenseCode)
                            .Where(m => m.OrganizatonTypeCode == org.OrganizationTypeCode)
                            .ToListAsync();

                        foreach (var mapping in mappings)
                        {
                            // Check if License Class Clause already in list
                            if (licenseClassClauses.FindAll(lcc => lcc.Id == mapping.LicenseClassClauseId).Count == 0)
                            {
                                licenseClassClauses.Add(mapping.LicenseClassClause);
                            }
                        }
                    }
                }
            }

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
                EnrolleeId = enrollee.Id,
                Clause = null,
                EffectiveDate = new DateTimeOffset()
            };

            if (lastNote != null)
            {
                newClause = new LimitsConditionsClause
                {
                    EnrolleeId = lastNote.EnrolleeId,
                    Clause = lastNote.Note,
                    EffectiveDate = new DateTimeOffset()
                };

                _context.LimitsConditionsClauses.Add(newClause);
                await _context.SaveChangesAsync();
            }

            return newClause;

        }
    }
}
