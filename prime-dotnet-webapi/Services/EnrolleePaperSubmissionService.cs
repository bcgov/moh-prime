using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Prime.Models;
using Prime.Engines;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Services
{
    public class EnrolleePaperSubmissionService : BaseService, IEnrolleePaperSubmissionService
    {
        private const string PaperGpidPrefix = "NOBCSC";

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

        public async Task<bool> PaperSubmissionExistsAsync(int enrolleeId)
        {
            var gpid = await _context.Enrollees
                .AsNoTracking()
                .Where(e => e.Id == enrolleeId)
                .Select(e => e.GPID)
                .SingleOrDefaultAsync();

            if (gpid == null)
            {
                return false;
            }

            return gpid.StartsWith(PaperGpidPrefix);
        }

        public async Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel createModel)
        {
            createModel.ThrowIfNull(nameof(createModel));

            var enrollee = _mapper.Map<Enrollee>(createModel);
            enrollee.UserId = Guid.NewGuid();
            enrollee.GPID = Gpid.NewGpid(PaperGpidPrefix);

            _context.Enrollees.Add(enrollee);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateEnrolleeEventAsync(enrollee.Id, "Enrollee Created");

            return enrollee;
        }

        public async Task UpdateEnrolleeCareSettingsById(int enrolleeId, PaperEnrolleeCareSettingViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            var careSettings = updateModel.CareSettingCodes.Select(code =>
                new EnrolleeCareSetting
                {
                    CareSettingCode = code
                }
            ).ToList();

            var healthAuthorities = updateModel.HealthAuthorityCodes.Select(code =>
                new EnrolleeHealthAuthority
                {
                    HealthAuthorityCode = code
                }
            ).ToList();

            enrollee.EnrolleeCareSettings = careSettings;
            enrollee.EnrolleeHealthAuthorities = healthAuthorities;

            _context.Update(enrollee);

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

        public async Task UpdateEnrolleeCertificationsById(int enrolleeId, PaperEnrolleeCertificationsViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            var certifications = updateModel.Certifications.Select(cert =>
                _mapper.Map<Certification>(cert)
            ).ToList();

            enrollee.Certifications = certifications;

            _context.Update(enrollee);

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
    }
}
