using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolleeProfileHistoryService : BaseService, IEnrolleeProfileHistoryService
    {
        public DefaultEnrolleeProfileHistoryService(
            ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        { }

        public async Task<IEnumerable<EnrolleeProfileHistory>> GetEnrolleeProfileHistoriesAsync(int enrolleeId)
        {
            return await _context.EnrolleeProfileHistories
                .Where(eph => eph.EnrolleeId == enrolleeId)
                .ToListAsync();
        }

        public async Task<EnrolleeProfileHistory> GetEnrolleeProfileHistoryAsync(int enrolleeId, int enrolleeProfileHistoryId)
        {
            return await _context.EnrolleeProfileHistories
                .Where(eph => eph.EnrolleeId == enrolleeId)
                .Where(eph => eph.Id == enrolleeProfileHistoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateEnrolleeProfileHistoryAsync(Enrollee enrollee)
        {
            // TODO should Object.Equals(previous, current) or save the profile each time?
            // TODO should this update more often or only on submit, what if they update a phone number? admin view wouldn't have most recent accurate profile in history
            // TODO apply Version to Enrollee model and copy to EnrolleeProfileHistory

            var enrolleeProfileHistory = new EnrolleeProfileHistory
            {
                EnrolleeId = (int)enrollee.Id,
                ProfileSnapshot = enrollee,
                Created = DateTime.Now
            };

            _context.EnrolleeProfileHistories.Add(enrolleeProfileHistory);

            return await _context.SaveChangesAsync();
        }
    }
}
