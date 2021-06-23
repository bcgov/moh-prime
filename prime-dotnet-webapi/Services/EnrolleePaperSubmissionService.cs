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

        public async Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel viewModel)
        {
            viewModel.ThrowIfNull(nameof(viewModel));

            var enrollee = _mapper.Map<Enrollee>(viewModel);

            enrollee.UserId = Guid.NewGuid();
            enrollee.GPID = Gpid.NewGpid(PaperGpidPrefix);
            enrollee.Addresses = new[]
            {
                new EnrolleeAddress
                {
                    Address = _mapper.Map<PhysicalAddress>(viewModel.PhysicalAddress)
                }
            };
            enrollee.AddEnrolmentStatus(StatusType.Editable);
            enrollee.AddEnrolmentStatus(StatusType.UnderReview)
                .AddStatusReason(StatusReasonType.PaperEnrollee);

            _context.Enrollees.Add(enrollee);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateEnrolleeEventAsync(enrollee.Id, "Enrollee Paper Submission Created");

            return enrollee;
        }

        public async Task UpdateCareSettingsAsync(int enrolleeId, PaperEnrolleeCareSettingViewModel viewModel)
        {
            var newCareSettings = viewModel.CareSettingCodes.Select(code => new EnrolleeCareSetting
            {
                CareSettingCode = code
            });

            var newHealthAuthorities = viewModel.HealthAuthorityCodes.Select(code => new EnrolleeHealthAuthority
            {
                HealthAuthorityCode = code
            });

            await ReplaceCollection(enrolleeId, newCareSettings);
            await ReplaceCollection(enrolleeId, newHealthAuthorities);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateDemographicsAsync(int enrolleeId, PaperEnrolleeDemographicViewModel viewModel)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.Addresses)
                    .ThenInclude(a => a.Address)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _mapper.Map(viewModel, enrollee);
            _mapper.Map(viewModel.PhysicalAddress, enrollee.PhysicalAddress);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOboSitesAsync(int enrolleeId, IEnumerable<PaperEnrolleeOboSiteViewModel> viewModels)
        {
            var newSites = _mapper.Map<IEnumerable<OboSite>>(viewModels);

            await ReplaceCollection(enrolleeId, newSites);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCertificationsAsync(int enrolleeId, IEnumerable<PaperEnrolleeCertificationViewModel> viewModels)
        {
            var newCerts = _mapper.Map<IEnumerable<Certification>>(viewModels);

            await ReplaceCollection(enrolleeId, newCerts);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSelfDeclarationsAsync(int enrolleeId, PaperEnrolleeSelfDeclarationViewModel viewModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(viewModel);

            await _context.SaveChangesAsync();
        }

        // TODO: Document stuffffffff
        public async Task UpdateAgreementsAsync(int enrolleeId, PaperEnrolleeAgreementViewModel viewModel)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _context.Entry(enrollee).CurrentValues.SetValues(viewModel);

            await _context.SaveChangesAsync();
        }

        private async Task ReplaceCollection<T>(int enrolleeId, IEnumerable<T> newItems) where T : class, IEnrolleeNavigationProperty
        {
            var oldItems = await _context.Set<T>()
                .Where(x => x.EnrolleeId == enrolleeId)
                .ToListAsync();

            foreach (var item in newItems)
            {
                item.EnrolleeId = enrolleeId;
            }

            _context.Set<T>().RemoveRange(oldItems);
            _context.Set<T>().AddRange(newItems);
        }

        public async Task FinailizeSubmissionAsync(int enrolleeId)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            enrollee.AddEnrolmentStatus(StatusType.RequiresToa);
            enrollee.AddEnrolmentStatus(StatusType.UnderReview);

            await _context.SaveChangesAsync();
        }
    }
}
