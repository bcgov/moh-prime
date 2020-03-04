using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Prime.Models;

namespace Prime.Services
{
    public class EnrolleeProfileVersionService : BaseService, IEnrolleeProfileVersionService
    {
        private JsonSerializer _camelCaseSerializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );

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
                .SingleOrDefaultAsync(epv => epv.Id == enrolleeProfileVersionId);
        }

        /**
          * Get the most recent Profile version before a given time.
          */
        public async Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionBeforeDateAsync(int enrolleeId, DateTime dateTime)
        {
            return await _context.EnrolleeProfileVersions
            .Where(epv => epv.EnrolleeId == enrolleeId)
            .Where(epv => epv.CreatedDate < dateTime)
            .OrderByDescending(epv => epv.CreatedDate)
            .FirstOrDefaultAsync();
        }

        public async Task CreateEnrolleeProfileVersionAsync(Enrollee enrollee)
        {
            var enrolleeProfileVersion = new EnrolleeProfileVersion
            {
                EnrolleeId = enrollee.Id,
                ProfileSnapshot = JObject.FromObject(enrollee, _camelCaseSerializer),
                CreatedDate = DateTime.Now
            };

            _context.EnrolleeProfileVersions.Add(enrolleeProfileVersion);

            await _context.SaveChangesAsync();
        }

    }
}
