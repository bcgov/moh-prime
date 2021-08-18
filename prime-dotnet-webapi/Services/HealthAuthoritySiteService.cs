using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthoritySites;

namespace Prime.Services
{
    public class HealthAuthoritySiteService : BaseService, IHealthAuthoritySiteService
    {
        private readonly IMapper _mapper;

        public HealthAuthoritySiteService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper)
            : base(context, httpContext)
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
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .ProjectTo<HealthAuthoritySiteViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthAuthoritySiteViewModel>> GetSitesAsync(int healthAuthorityId)
        {
            return await _context.HealthAuthoritySites
                .Where(has => has.HealthAuthorityOrganizationId == healthAuthorityId)
                .AsNoTracking()
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

        public async Task UpdateVendorAsync(int siteId, int vendorCode)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO check vendor exists on the HealthAuthority list of vendor(s)
            site.VendorCode = vendorCode;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSiteInfoAsync(int siteId, HealthAuthoritySiteInfoViewModel viewModel)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            _mapper.Map(viewModel, site);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCareTypeAsync(int siteId, string careType)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO check careType exists on the HealthAuthority list of careType(s)
            site.CareType = careType;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhysicalAddressAsync(int siteId, AddressViewModel physicalAddress)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.PhysicalAddress = _mapper.Map<PhysicalAddress>(physicalAddress);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateHoursOperationAsync(int siteId, ICollection<BusinessDay> businessHours)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO dependency of Site navigational property in BusinessDay
            // site.BusinessHours = businessHours;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRemoteUsersAsync(int siteId, ICollection<RemoteUser> remoteUsers)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO dependency of Site navigational property in RemoteUser
            // site.RemoteUsers = remoteUsers;

            await _context.SaveChangesAsync();
        }


        public async Task UpdatePharmanetAdministratorAsync(int siteId, int contactId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            var healthAuthorityPharmanetAdministratorId = await _context.HealthAuthorityContacts
                .Where(hac => hac.ContactId == contactId)
                .Select(hac => hac.Id)
                .SingleOrDefaultAsync();

            // TODO check administrator exists on the HealthAuthority list of administrator(s)
            site.HealthAuthorityPharmanetAdministratorId = healthAuthorityPharmanetAdministratorId;

            await _context.SaveChangesAsync();
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

            // TODO add status change to site (next sprint)
            // TODO add business events (next sprint)
            site.SubmittedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
        }
    }
}
