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

        public async Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeProfileVersionId)
        {
            return await _context.EnrolleeProfileVersions
                .Where(epv => epv.Id == enrolleeProfileVersionId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateEnrolleeProfileVersionAsync(Enrollee enrollee)
        {
            var mostRecentVersion = await _context.EnrolleeProfileVersions
                .Where(epv => epv.EnrolleeId == enrollee.Id)
                .OrderByDescending(epv => epv.CreatedDate)
                .FirstOrDefaultAsync();

            if (mostRecentVersion != null && !Object.Equals(mostRecentVersion, enrollee))
            {
                var enrolleeProfileVersion = new EnrolleeProfileVersion
                {
                    EnrolleeId = (int)enrollee.Id,
                    ProfileSnapshot = JObject.FromObject(enrollee),
                    CreatedDate = DateTime.Now
                };

                _context.EnrolleeProfileVersions.Add(enrolleeProfileVersion);

                await _context.SaveChangesAsync();
            }
        }
    }
}
