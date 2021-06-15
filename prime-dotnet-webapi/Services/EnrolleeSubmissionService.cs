using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Prime.Engines;
using Prime.Models;
using Prime.DTOs.AgreementEngine;

namespace Prime.Services
{
    public class EnrolleeSubmissionService : BaseService, IEnrolleeSubmissionService
    {
        private readonly JsonSerializer _camelCaseSerializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );

        private readonly IEnrolleeService _enrolleeService;
        private readonly IMapper _mapper;

        public EnrolleeSubmissionService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IEnrolleeService enrolleeService
            ) : base(context, httpContext)
        {
            _mapper = mapper;
            _enrolleeService = enrolleeService;
        }

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

        public async Task CreateEnrolleeSubmissionAsync(int enrolleeId, bool assignAgreement = true)
        {
            var enrollee = await _enrolleeService.GetEnrolleeNoTrackingAsync(enrolleeId);

            var enrolleeSubmission = new Submission
            {
                EnrolleeId = enrollee.Id,
                ProfileSnapshot = JObject.FromObject(enrollee, _camelCaseSerializer),
                RequestedRemoteAccess = enrollee.EnrolleeRemoteUsers.Any(),
                CreatedDate = DateTimeOffset.Now
            };

            if (assignAgreement)
            {
                var agreementDto = _mapper.Map<AgreementEngineDto>(enrollee);
                enrolleeSubmission.AgreementType = AgreementEngine.DetermineAgreementType(agreementDto);
            }

            _context.Submissions.Add(enrolleeSubmission);

            await _context.SaveChangesAsync();
        }
    }
}
