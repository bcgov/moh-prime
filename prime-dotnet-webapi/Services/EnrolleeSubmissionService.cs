using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.DTOs.AgreementEngine;
using Prime.Engines;
using Prime.Models;

namespace Prime.Services
{
    public class EnrolleeSubmissionService : BaseService, IEnrolleeSubmissionService
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IMapper _mapper;

        public EnrolleeSubmissionService(
            ApiDbContext context,
            ILogger<EnrolleeSubmissionService> logger,
            IEnrolleeService enrolleeService,
            IMapper mapper)
            : base(context, logger)
        {
            _enrolleeService = enrolleeService;
            _mapper = mapper;
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

        public async Task<Submission> CreateEnrolleeSubmissionAsync(int enrolleeId, bool assignAgreement = true)
        {
            var enrollee = await _context.Enrollees
                .AsNoTracking()
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
                .Include(e => e.OboSites)
                    .ThenInclude(s => s.PhysicalAddress)
                .Include(e => e.EnrolleeCareSettings)
                .Include(e => e.EnrolleeHealthAuthorities)
                .Include(e => e.EnrolleeRemoteUsers)
                .Include(e => e.RemoteAccessSites)
                    .ThenInclude(ras => ras.Site)
                        .ThenInclude(ras => ras.PhysicalAddress)
                .Include(r => r.RemoteAccessLocations)
                    .ThenInclude(rul => rul.PhysicalAddress)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.Status)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                        .ThenInclude(esr => esr.StatusReason)
                .Include(e => e.AccessAgreementNote)
                .Include(e => e.SelfDeclarations)
                .Include(e => e.SelfDeclarationDocuments)
                .Include(e => e.IdentificationDocuments)
                .Include(e => e.Agreements)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            var enrolleeSubmission = new Submission
            {
                EnrolleeId = enrolleeId,
                ProfileSnapshot = JObject.FromObject(enrollee, JsonSerializer.Create(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
                ),
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

            return enrolleeSubmission;
        }
    }
}
