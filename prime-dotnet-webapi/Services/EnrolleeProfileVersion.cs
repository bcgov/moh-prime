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
            if (!await EqualsPreviousVersion(enrollee))
            {
                var enrolleeProfileVersion = new EnrolleeProfileVersion
                {
                    EnrolleeId = (int)enrollee.Id,
                    ProfileSnapshot = ConvertEnrolleeToJObject(enrollee),
                    CreatedDate = DateTime.Now
                };

                _context.EnrolleeProfileVersions.Add(enrolleeProfileVersion);

                await _context.SaveChangesAsync();
            }
        }

        private JObject ConvertEnrolleeToJObject(Enrollee enrollee)
        {
            // TODO why doesn't this work in the entity configuration?
            // return JObject.FromObject(enrollee);
            return JObject.FromObject(enrollee, _camelCaseSerializer);
        }

        private async Task<Boolean> EqualsPreviousVersion(Enrollee enrollee)
        {
            var previousEnrolleeProfileVersion = await _context.EnrolleeProfileVersions
                .Where(epv => epv.EnrolleeId == enrollee.Id)
                .OrderByDescending(epv => epv.CreatedDate)
                .FirstOrDefaultAsync();

            if (previousEnrolleeProfileVersion != null)
            {
                var previousProfileSnapshot = previousEnrolleeProfileVersion.ProfileSnapshot;
                var currentProfileSnapshot = ConvertEnrolleeToJObject(enrollee);

                return JObject.DeepEquals(previousProfileSnapshot, currentProfileSnapshot);
            }

            return false;
        }
    }
}
