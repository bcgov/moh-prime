using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Prime.Models;
using Prime.Engines;
using Prime.ViewModels.PaperEnrollees;
using System.Collections.Generic;

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

        public async Task UpdateCareSettingsAsync(int enrolleeId, PaperEnrolleeCareSettingViewModel paperCareSettings)
        {
            var newCareSettings = paperCareSettings.CareSettingCodes.Select(code => new EnrolleeCareSetting
            {
                EnrolleeId = enrolleeId,
                CareSettingCode = code
            });

            var newHealthAuthorities = paperCareSettings.HealthAuthorityCodes.Select(code => new EnrolleeHealthAuthority
            {
                EnrolleeId = enrolleeId,
                HealthAuthorityCode = code
            });

            await ReplaceCollection(enrolleeId, newCareSettings);
            await ReplaceCollection(enrolleeId, newHealthAuthorities);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateDemographicsAsync(int enrolleeId, PaperEnrolleeDemographicViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOboSitesAsync(int enrolleeId, PaperEnrolleeOboSiteViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCertificationsAsync(int enrolleeId, ICollection<PaperEnrolleeCertificationViewModel> updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            var certifications = _mapper.Map<ICollection<Certification>>(updateModel);

            enrollee.Certifications = certifications;

            _context.Update(enrollee);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSelfDeclarationsAsync(int enrolleeId, PaperEnrolleeSelfDeclarationViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        // TODO: Document stuffffffff
        public async Task UpdateAgreementsAsync(int enrolleeId, PaperEnrolleeAgreementViewModel updateModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(updateModel);

            await _context.SaveChangesAsync();
        }

        private async Task ReplaceCollection<T>(int enrolleeId, IEnumerable<T> newItems) where T : class, IEnrolleeNavigationProperty
        {
            var oldItems = await _context.Set<T>()
                .Where(x => x.EnrolleeId == enrolleeId)
                .ToListAsync();

            _context.Set<T>().RemoveRange(oldItems);
            _context.Set<T>().AddRange(newItems);
        }
    }
}
