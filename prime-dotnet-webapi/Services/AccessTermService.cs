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
                .Include(at => at.UserClause)
                .Include(at => at.LimitsConditionsClause)
                .Where(at => at.EnrolleeId == enrolleeId)
                .Where(at => at.Id == accessTermId)
                .Where(at => at.AcceptedDate != null)
                .FirstOrDefaultAsync();

            return accessTerm;
        }

        /**
         * Get the most recent terms !ACCEPTED access term for an enrollee.
         */
        public async Task<AccessTerm> GetMostRecentNotAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            var accessTerm = await _context.AccessTerms
                .Include(at => at.UserClause)
                .Include(at => at.LimitsConditionsClause)
                .Where(at => at.EnrolleeId == enrolleeId)
                .Where(at => at.AcceptedDate == null)
                .OrderByDescending(at => at.CreatedDate)
                .FirstOrDefaultAsync();

            return accessTerm;
        }

        /**
         * Get the most recent terms ACCEPTED access term for an enrollee.
         */
        public async Task<AccessTerm> GetMostRecentAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            var accessTerm = await _context.AccessTerms
                .Include(at => at.UserClause)
                .Include(at => at.LimitsConditionsClause)
                .Where(at => at.EnrolleeId == enrolleeId)
                .Where(at => at.AcceptedDate != null)
                .OrderByDescending(at => at.CreatedDate)
                .FirstOrDefaultAsync();

            return accessTerm;
        }

        /**
         *  Get list of ACCEPTED access terms for an enrollee
         */
        public async Task<IEnumerable<AccessTerm>> GetAcceptedAccessTerms(int enrolleeId, int year)
        {
            var accessTerms = await _context.AccessTerms
                .Include(at => at.UserClause)
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
                .OrderByDescending(at => at.CreatedDate)
                .FirstAsync();

            accessTerm.AcceptedDate = DateTimeOffset.Now;
            // Add an Expiry Date of one year in the future.
            accessTerm.ExpiryDate = DateTimeOffset.Now.Add(ACCESS_TERM_EXPIRY);

            await _context.SaveChangesAsync();
        }

        public async Task ExpireCurrentAccessTermAsync(Enrollee enrollee)
        {
            var accessTerm = await _context.AccessTerms
                .Where(at => at.EnrolleeId == enrollee.Id)
                .OrderByDescending(at => at.CreatedDate)
                .FirstOrDefaultAsync();

            if (accessTerm != null)
            {
                // Set expiry date to now, sudo expirying an access term.
                accessTerm.ExpiryDate = DateTimeOffset.Now;
            }

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
            // var current = true;

            // var accessTerm = await _context.AccessTerms
            //     .Include(at => at.AccessTermLicenseClassClauses)
            //     .Where(at => at.EnrolleeId == enrollee.Id)
            //     .Where(at => at.AcceptedDate != null)
            //     .OrderByDescending(at => at.AcceptedDate)
            //     .FirstOrDefaultAsync();

            // if (accessTerm != null)
            // {
            //     var currentAccessTerm = await GenerateAccessTermAsync(enrollee);

            //     if (accessTerm.GlobalClauseId != currentAccessTerm.GlobalClause.Id
            //        || accessTerm.UserClauseId != currentAccessTerm.UserClause.Id)
            //     {
            //         current = false;
            //     }

            //     foreach (var lcc in accessTerm.AccessTermLicenseClassClauses)
            //     {
            //         if (currentAccessTerm.AccessTermLicenseClassClauses.FindAll(c => c.LicenseClassClause.Id == lcc.LicenseClassClauseId).Count == 0)
            //         {
            //             current = false;
            //         }
            //     }
            // }
            // else
            // {
            return false;
            // }

            // return current;
        }

        /// <summary>
        /// Enrollee’s that are:
        /// Under Review ->  -> Current TOA status: --
        /// Locked, Declined -> Current TOA status: NA
        /// Required TOA -> Current TOA status: Pending
        /// Editable (AND before their renewal date) -> Current TOA status: Is their signed TOA the most current version
        /// Editable (AND on/after their renewal date) -> Current TOA status: --
        /// </summary>
        public async Task<string> GetCurrentTOAStatusAsync(Enrollee enrollee)
        {
            var currentStatus = enrollee.CurrentStatus;
            var toaStatus = "";

            if (currentStatus.IsType(StatusType.Locked) || currentStatus.IsType(StatusType.Declined))
            {
                toaStatus = "N/A";
            }
            else if (currentStatus.IsType(StatusType.RequiresToa))
            {
                toaStatus = "Pending";
            }
            else if (currentStatus.IsType(StatusType.Editable))
            {
                var accessTerm = await GetMostRecentAcceptedEnrolleesAccessTermAsync(enrollee.Id);
                var isCurrent = await this.IsCurrentByEnrolleeAsync(enrollee);

                if (accessTerm?.ExpiryDate > DateTimeOffset.Now)
                {
                    toaStatus = (accessTerm != null && isCurrent)
                        ? "Yes"
                        : "No";
                }
            }
            return toaStatus;
        }

        /**
         * Generates an Access Term based off of the enrollee
         */
        private async Task<AccessTerm> GenerateAccessTermAsync(Enrollee enrollee)
        {
            var accessTerms = new AccessTerm { Enrollee = enrollee };
            throw new NotImplementedException();
            // accessTerms.UserClause = await GetUserClause(enrollee);
            accessTerms.LimitsConditionsClause = await GetAccessTermLimitsConditionsClause(enrollee);

            return accessTerms;
        }

        private async Task<UserClause> GetUserClause(Enrollee enrollee)
        {

            throw new NotImplementedException();
            var classification = enrollee.IsRegulatedUser() ? PrimeConstants.PRIME_RU : PrimeConstants.PRIME_OBO;

            // return await _context.UserClauses
            //     .Where(g => g.EnrolleeClassification == classification)
            //     .OrderByDescending(g => g.EffectiveDate)
            //     .FirstAsync();
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
                Text = null,
                EffectiveDate = new DateTimeOffset()
            };

            if (lastNote != null)
            {
                newClause = new LimitsConditionsClause
                {
                    EnrolleeId = lastNote.EnrolleeId,
                    Text = lastNote.Note,
                    EffectiveDate = new DateTimeOffset()
                };

                _context.LimitsConditionsClauses.Add(newClause);
                await _context.SaveChangesAsync();
            }

            return newClause;

        }
    }
}
