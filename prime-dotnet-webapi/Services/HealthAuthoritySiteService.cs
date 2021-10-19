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

        public HealthAuthoritySiteService(
            ApiDbContext context,
            ILogger<HealthAuthoritySiteService> logger,
            IMapper mapper)
            : base(context, logger)
        {
            _mapper = mapper;
        }

        public async Task<bool> SiteExistsAsync(int healthAuthorityId, int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId && s.HealthAuthorityOrganizationId == healthAuthorityId);
        }

        public async Task<HealthAuthoritySiteViewModel> CreateSiteAsync(int healthAuthorityId, int vendorCode)
        {
            // TODO dependency of Site navigational property in Vendor
            var site = new HealthAuthoritySite
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                VendorCode = vendorCode
                // TODO set initial status change (next sprint)
            };

            // TODO add business events (next sprint)

            _context.HealthAuthoritySites.Add(site);
            await _context.SaveChangesAsync();

            return _mapper.Map<HealthAuthoritySiteViewModel>(site);
        }

        public async Task<IEnumerable<HealthAuthoritySiteViewModel>> GetAllSitesAsync()
        {
            return await GetBaseSitesNoTrackingQuery().ToListAsync();
        }

        public async Task<IEnumerable<HealthAuthoritySiteViewModel>> GetSitesAsync(int healthAuthorityId)
        {
            return await GetBaseSitesNoTrackingQuery()
                .Where(has => has.HealthAuthorityOrganizationId == healthAuthorityId)
                .ToListAsync();
        }

        public async Task<HealthAuthoritySiteViewModel> GetSiteAsync(int siteId)
        {
            return await GetBaseSitesNoTrackingQuery()
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task<IEnumerable<BusinessDayViewModel>> GetBusinessHours(int siteId)
        {
            // TODO implement
            throw new NotImplementedException();
            // return await GetBaseSitesNoTrackingQuery()
            //     .Include(ha => ha.BusinessHours)
            //     .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task<IEnumerable<RemoteUserViewModel>> GetRemoteUsers(int siteId)
        {
            // TODO implement
            throw new NotImplementedException();
            // return await GetBaseSitesNoTrackingQuery()
            //     .Include(ha => ha.RemoteUsers)
            //     .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task UpdateSiteAsync(int healthAuthorityId, int siteId, HealthAuthoritySiteUpdateModel updateModel)
        {
            // TODO implement
            throw new NotImplementedException();
            // var site = await _context.HealthAuthoritySites
            //     .SingleOrDefaultAsync(has => has.Id == siteId);

            // // TODO split out into methods where appropriate to reduce method
            // //      size where checks are required, otherwise update in place

            // // TODO check vendor exists on the HealthAuthority list of vendor(s)
            // site.VendorCode = updateModel.vendorCode;
            // site.SiteName = updateModel.SiteName;
            // site.SiteId = updateModel.SiteId;
            // site.SecurityGroupCode = updateModel.SecurityGroupCode;
            // // TODO check careType exists on the HealthAuthority list of careType(s)
            // site.CareType = updateModel.CareType;
            // site.PhysicalAddress = _mapper.Map<PhysicalAddress>(updateModel.PhysicalAddress);
            // // TODO dependency of Site navigational property in BusinessDay
            // // TODO update using appropriate mapping
            // // site.BusinessHours = updateModel.BusinessHours;
            // // TODO dependency of Site navigational property in RemoteUser
            // // TODO update using appropriate mapping
            // // site.RemoteUsers = updateModel.RemoteUsers;
            // // TODO check administrator exists on the HealthAuthority list of administrator(s)
            // site.HealthAuthorityPharmanetAdministratorId = updateModel.HealthAuthorityContactId;
            // // TODO check technical support exists on the HealthAuthority list of technical support(s)
            // site.HealthAuthorityTechnicalSupportId = updateModel.HealthAuthorityContactId;

            // await _context.SaveChangesAsync();
        }

        public async Task SetSiteCompletedAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.Completed = true;

            await _context.SaveChangesAsync();
        }

        public async Task SiteSubmissionAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO add status change to site
            // TODO add business events
            site.SubmittedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
        }

        private IQueryable<HealthAuthoritySiteViewModel> GetBaseSitesNoTrackingQuery()
        {
            return _context.HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider);
        }
    }
}
