using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DelegateDecompiler.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Auth;
using Prime.Models;
using Prime.ViewModels;
using Prime.Models.Api;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using System.Security.Claims;
using System.Linq.Expressions;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Services
{
    public class EnrolleePaperSubmissionService : BaseService, IEnrolleePaperSubmissionService
    {
        private readonly IMapper _mapper;
        private readonly IBusinessEventService _businessEventService;

        public EnrolleePaperSubmissionService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IBusinessEventService businessEventService)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _businessEventService = businessEventService;
        }

        public async Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel createModel)
        {
            createModel.ThrowIfNull(nameof(createModel));

            var enrollee = _mapper.Map<Enrollee>(createModel);
            enrollee.UserId = new Guid();
            enrollee.GPID = GeneratePaperGpid();

            _context.Enrollees.Add(enrollee);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateEnrolleeEventAsync(enrollee.Id, "Enrollee Created");

            return enrollee;
        }

        public async Task UpdateEnrolleeCareSettingsById(int enrolleeId, PaperEnrolleeCareSettingViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrolleeDemographicsById(int enrolleeId, PaperEnrolleeDemographicViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrolleeOboSitesById(int enrolleeId, PaperEnrolleeOboSiteViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrolleeCertificationsById(int enrolleeId, PaperEnrolleeCertificationViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrolleeSelfDeclarationsById(int enrolleeId, PaperEnrolleeSelfDeclarationViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        // TODO: Document stuffffffff
        public async Task UpdateEnrolleeAgreementsById(int enrolleeId, PaperEnrolleeAgreementViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        private static string GeneratePaperGpid()
        {
            IEnumerable<char> prefix = "NOBCSC";

            Random r = new Random();
            int length = 14;
            string characterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,?!@#$%*";

            IEnumerable<char> chars = Enumerable.Repeat(characterSet, length).Select(s => s[r.Next(s.Length)]);

            return new string(prefix.Concat(chars).ToArray());
        }
    }
}
