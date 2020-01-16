using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class DefaultEnrolleeProfileVersionService : BaseService, IEnrolleeProfileVersionService
    {
        public DefaultEnrolleeProfileVersionService(
            ApiDbContext context, IHttpContextAccessor httpContext) : base(context, httpContext)
        { }

        public async Task<IEnumerable<EnrolleeProfileVersion>> GetEnrolleeProfileVersionsAsync(int enrolleeId)
        {
            return await _context.EnrolleeProfileHistories
                .Where(eph => eph.EnrolleeId == enrolleeId)
                .ToListAsync();
        }

        public async Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeId, int enrolleeProfileHistoryId)
        {
            return await _context.EnrolleeProfileHistories
                .Where(eph => eph.EnrolleeId == enrolleeId)
                .Where(eph => eph.Id == enrolleeProfileHistoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateEnrolleeProfileVersionAsync(Enrollee enrollee)
        {
            // TODO should Object.Equals(previous, current) or save the profile each time?
            // TODO should this update more often or only on submit, what if they update a phone number? admin view wouldn't have most recent accurate profile in history
            // TODO apply Version to Enrollee model and copy to EnrolleeProfileHistory

            var enrolleeProfileHistory = new EnrolleeProfileVersion
            {
                EnrolleeId = (int)enrollee.Id,
                ProfileSnapshot = enrollee,
                CreatedDate = DateTime.Now
            };

            _context.EnrolleeProfileHistories.Add(enrolleeProfileHistory);

            return await _context.SaveChangesAsync();
        }
    }
}
