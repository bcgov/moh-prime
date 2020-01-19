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
                .Where(epv => epv.Id == enrolleeProfileVersionId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateEnrolleeProfileVersionAsync(Enrollee enrollee)
        {
            var enrolleeProfileVersion = new EnrolleeProfileVersion
            {
                EnrolleeId = (int)enrollee.Id,
                // TODO why doesn't this work in the entity configuration?
                // ProfileSnapshot = JObject.FromObject(enrollee),
                ProfileSnapshot = JObject.FromObject(enrollee, _camelCaseSerializer),
                CreatedDate = DateTime.Now
            };

            _context.EnrolleeProfileVersions.Add(enrolleeProfileVersion);

            await _context.SaveChangesAsync();
        }
    }
}
