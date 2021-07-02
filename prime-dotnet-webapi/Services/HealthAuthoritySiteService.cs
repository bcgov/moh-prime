using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Prime.Models;
using Prime.Models.HealthAuthorities;
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

        public async Task<bool> SiteExistsAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .AnyAsync(s => s.Id == siteId);
        }

        public async Task<HealthAuthoritySite> CreateSiteAsync(int healthAuthorityId, int vendorCode)
        {
            var site = new HealthAuthoritySite
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                Vendor = new HealthAuthorityVendor
                {
                    HealthAuthorityOrganizationId = healthAuthorityId,
                    VendorCode = vendorCode
                },
                // TODO set initial status change
            };

            _context.HealthAuthoritySites.Add(site);
            await _context.SaveChangesAsync();

            return site;
        }

        public async Task<IEnumerable<HealthAuthoritySite>> GetSitesAsync(int healthAuthorityId)
        {
            return await _context.HealthAuthoritySites
                .Where(has => has.HealthAuthorityOrganizationId == healthAuthorityId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<HealthAuthoritySite> GetSiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .SingleOrDefaultAsync(has => has.Id == siteId);
        }

        public async Task UpdateVendorAsync(int siteId, int vendorCode)
        {
            var site = await _context.HealthAuthoritySites
                // .Include(has => has.Vendor)
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO verify this vendor exists on the HealthAuthority list vendor
            // TODO find vendor code in HA list
            // site.Vendor.VendorCode = vendorCode;

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
                .Include(has => has.CareType)
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO can this be automapped
            site.CareType.CareType = careType;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhysicalAddressAsync(int siteId, PhysicalAddress physicalAddress)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            _mapper.Map(physicalAddress, site.PhysicalAddress);
            // site.PhysicalAddress = _mapper.Map<PhysicalAddress>(physicalAddress);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateHoursOperationAsync(int siteId, HealthAuthoritySiteHoursOperationViewModel viewModel)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // _mapper.Map(viewModel, site);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRemoteUsersAsync(int siteId, HealthAuthoritySiteRemoteUsersViewModel viewModel)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // _mapper.Map(viewModel, site);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdministratorAsync(int siteId, HealthAuthoritySiteAdministratorViewModel viewModel)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // _mapper.Map(viewModel, site);

            await _context.SaveChangesAsync();
        }

        public async Task SetSiteCompletedAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(has => has.Id == siteId);

            site.Completed = true;

            await _context.SaveChangesAsync();
        }

        public async Task FinalizeSubmissionAsync(int siteId)
        {
            var site = await _context.HealthAuthoritySites
                .Include(has => has.Status)
                .SingleOrDefaultAsync(has => has.Id == siteId);

            // TODO add status change to site

            await _context.SaveChangesAsync();
        }
    }
}
