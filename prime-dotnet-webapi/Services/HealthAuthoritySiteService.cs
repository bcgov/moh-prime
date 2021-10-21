using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Sites;

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
            return await _context.V2HealthAuthoritySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId && s.HealthAuthorityOrganizationId == healthAuthorityId);
        }

        public async Task<bool> SiteIsEditableAsync(int healthAuthorityId, int siteId)
        {
            return await _context.V2HealthAuthoritySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId
                    && s.HealthAuthorityOrganizationId == healthAuthorityId
                    && s.Status == SiteStatusType.Editable);
        }

        public async Task<V2HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, HealthAuthoritySiteCreateModel createModel)
        {
            var site = new V2HealthAuthoritySite
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                HealthAuthorityVendorId = createModel.HealthAuthorityVendorId,
                AuthorizedUserId = createModel.AuthorizedUserId
            };
            site.AddStatus(SiteStatusType.Editable);


            _context.V2HealthAuthoritySites.Add(site);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Created");

            return _mapper.Map<V2HealthAuthoritySiteViewModel>(site);
        }

        public async Task<IEnumerable<HealthAuthoritySiteViewModel>> GetAllSitesAsync()
        {
            return await GetBaseSitesNoTrackingQuery().ToListAsync();
        }

        public async Task<IEnumerable<V2HealthAuthoritySiteViewModel>> GetSitesAsync(int healthAuthorityId)
        {
            return await _context.V2HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<V2HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider)
                .Where(has => has.HealthAuthorityOrganizationId == healthAuthorityId)
                .ToListAsync();
        }

        public async Task<V2HealthAuthoritySiteViewModel> GetSiteAsync(int siteId)
        {
            return await _context.V2HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<V2HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task<IEnumerable<BusinessDayViewModel>> GetBusinessHoursAsync(int siteId)
        {
            return await _context.Set<BusinessDay>()
                .Where(day => day.SiteId == siteId)
                .ProjectTo<BusinessDayViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<RemoteUserViewModel>> GetRemoteUsersAsync(int siteId)
        {
            return await _context.V2HealthAuthoritySites
                .Where(ha => ha.Id == siteId)
                .SelectMany(ha => ha.RemoteUsers)
                .ProjectTo<RemoteUserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task UpdateSiteAsync(int healthAuthorityId, int siteId, HealthAuthoritySiteUpdateModel updateModel)
        {
            var site = await _context.V2HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // _context.Entry(site).CurrentValues.SetValues(updateModel);

            site.SiteName = updateModel.SiteName;
            site.SiteId = updateModel.SiteId;
            site.SecurityGroupCode = updateModel.SecurityGroupCode;
            site.HealthAuthorityCareTypeId = updateModel.HealthAuthorityCareTypeId;
            site.HealthAuthorityPharmanetAdministratorId = updateModel.HealthAuthorityPharmanetAdministratorId;
            site.HealthAuthorityTechnicalSupportId = updateModel.HealthAuthorityTechnicalSupportId;

            site.PhysicalAddress = _mapper.Map<PhysicalAddress>(updateModel.PhysicalAddress);
            site.BusinessHours = _mapper.Map<ICollection<BusinessDay>>(updateModel.BusinessHours);
            site.RemoteUsers = _mapper.Map<ICollection<RemoteUser>>(updateModel.RemoteUsers);

            await _context.SaveChangesAsync();
        }

        public async Task SetSiteCompletedAsync(int siteId)
        {
            var site = await _context.V2HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.Completed = true;

            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Completed");
        }

        public async Task SiteSubmissionAsync(int siteId)
        {
            var site = await _context.V2HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.SubmittedDate = DateTimeOffset.Now;
            site.AddStatus(SiteStatusType.InReview);

            await _context.SaveChangesAsync();

            await _businessEventService.CreateSiteEventAsync(site.Id, "Health Authority Site Submitted");
        }

        private IQueryable<HealthAuthoritySiteViewModel> GetBaseSitesNoTrackingQuery()
        {
            return _context.HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider);
        }
    }
}
