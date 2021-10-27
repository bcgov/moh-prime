using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegateDecompiler;
using DelegateDecompiler.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Services
{
    public class HealthAuthoritySiteService : BaseService, IHealthAuthoritySiteService
    {
        private readonly IMapper _mapper;
        private readonly IBusinessEventService _businessEventService;

        public HealthAuthoritySiteService(
            ApiDbContext context,
            ILogger<HealthAuthoritySiteService> logger,
            IMapper mapper,
            IBusinessEventService businessEventService)
            : base(context, logger)
        {
            _mapper = mapper;
            _businessEventService = businessEventService;
        }

        public async Task<bool> SiteExistsAsync(int healthAuthorityId, int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId && s.HealthAuthorityOrganizationId == healthAuthorityId);
        }

        public async Task<bool> SiteIsEditableAsync(int healthAuthorityId, int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .DecompileAsync()
                .AnyAsync(s => s.Id == siteId
                   && s.HealthAuthorityOrganizationId == healthAuthorityId
                   && s.Status == SiteStatusType.Editable);
        }

        public async Task<HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, HealthAuthoritySiteCreateModel createModel)
        {
            var site = new HealthAuthoritySite
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                HealthAuthorityVendorId = createModel.HealthAuthorityVendorId,
                AuthorizedUserId = createModel.AuthorizedUserId
            };
            site.AddStatus(SiteStatusType.Editable);

            _context.HealthAuthoritySites.Add(site);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Created");

            return _mapper.Map<HealthAuthoritySiteViewModel>(site);
        }

        public async Task<IEnumerable<HealthAuthoritySiteViewModel>> GetSitesAsync(int? healthAuthorityId = null)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .If(healthAuthorityId.HasValue, q => q.Where(site => site.HealthAuthorityOrganizationId == healthAuthorityId))
                .ProjectTo<HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task UpdateSiteAsync(int siteId, HealthAuthoritySiteUpdateModel updateModel)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            _context.Entry(site).CurrentValues.SetValues(updateModel);

            // TODO not like this
            site.PhysicalAddress = _mapper.Map<PhysicalAddress>(updateModel.PhysicalAddress);
            site.BusinessHours = _mapper.Map<ICollection<BusinessDay>>(updateModel.BusinessHours);
            site.RemoteUsers = _mapper.Map<ICollection<RemoteUser>>(updateModel.RemoteUsers);

            await _context.SaveChangesAsync();
        }

        public async Task SetSiteCompletedAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.Completed = true;

            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Completed");
        }

        public async Task SiteSubmissionAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.SubmittedDate = DateTimeOffset.Now;
            site.AddStatus(SiteStatusType.InReview);

            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Submitted");
        }
    }
}
