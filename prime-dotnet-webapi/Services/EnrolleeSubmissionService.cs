using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Prime.Engines;
using Prime.Models;

namespace Prime.Services
{
    public class EnrolleeSubmissionService : BaseService, IEnrolleeSubmissionService
    {
        private JsonSerializer _camelCaseSerializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );

        public EnrolleeSubmissionService(
            ApiDbContext context,
            IHttpContextAccessor httpContext
            ) : base(context, httpContext)
        { }

        public async Task<IEnumerable<Submission>> GetEnrolleeSubmissionsAsync(int enrolleeId)
        {
            return await _context.Submissions
                .Where(epv => epv.EnrolleeId == enrolleeId)
                .ToListAsync();
        }

        public async Task<Submission> GetEnrolleeSubmissionAsync(int enrolleeSubmissionId)
        {
            return await _context.Submissions
                .SingleOrDefaultAsync(epv => epv.Id == enrolleeSubmissionId);
        }

        /**
          * Get the most recent submission before a given date.
          */
        public async Task<Submission> GetEnrolleeSubmissionBeforeDateAsync(int enrolleeId, DateTimeOffset dateTime)
        {
            return await _context.Submissions
            .Where(epv => epv.EnrolleeId == enrolleeId)
            .Where(epv => epv.CreatedDate < dateTime)
            .OrderByDescending(epv => epv.CreatedDate)
            .FirstOrDefaultAsync();
        }

        public async Task CreateEnrolleeSubmissionAsync(Enrollee enrollee)
        {
            var enrolleeSubmission = new Submission
            {
                EnrolleeId = enrollee.Id,
                ProfileSnapshot = JObject.FromObject(enrollee, _camelCaseSerializer),
                AgreementType = new AgreementEngine().DetermineAgreementType(enrollee),
                CreatedDate = DateTimeOffset.Now
            };

            _context.Submissions.Add(enrolleeSubmission);

            await _context.SaveChangesAsync();
        }

    }
}
