using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Prime.Models;

namespace Prime.Services
{
    public class EnrolleeProfileVersionService : BaseService, IEnrolleeProfileVersionService
    {
        public EnrolleeProfileVersionService(
            ApiDbContext context,
            IHttpContextAccessor httpContext
            ) : base(context, httpContext)
        { }

        public async Task<IEnumerable<EnrolleeProfileVersion>> GetEnrolleeProfileVersionsAsync(int enrolleeId)
        {
            return await _context.EnrolleeProfileVersions
                .Where(epv => epv.EnrolleeId == enrolleeId)
                .ToListAsync();
        }

        public async Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeProfileHistoryId)
        {
            return await _context.EnrolleeProfileVersions
                .Where(epv => epv.Id == enrolleeProfileHistoryId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateEnrolleeProfileVersionAsync(Enrollee enrollee)
        {
            // TODO should Object.Equals(previous, current) or save the profile each time?

            var mostRecentVersion = await _context.EnrolleeProfileVersions
                .Where(epv => epv.EnrolleeId == enrollee.Id)
                .OrderByDescending(epv => epv.CreatedDate)
                .FirstOrDefaultAsync();

            if (mostRecentVersion != null && !Object.Equals(mostRecentVersion, enrollee))
            {
                return 0;
            }

            var enrolleeProfileHistory = new EnrolleeProfileVersion
            {
                EnrolleeId = (int)enrollee.Id,
                ProfileSnapshot = JObject.FromObject(enrollee),
                CreatedDate = DateTime.Now
            };

            _context.EnrolleeProfileVersions.Add(enrolleeProfileHistory);

            return await _context.SaveChangesAsync();
        }
    }
}
