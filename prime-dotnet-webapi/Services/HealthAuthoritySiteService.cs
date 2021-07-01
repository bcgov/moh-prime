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
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
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

        public async Task<IEnumerable<HealthAuthoritySite>> GetSitesAsync(int healthAuthorityId)
        {
            return await _context.HealthAuthoritySites
                .Where(s => s.HealthAuthorityOrganizationId == healthAuthorityId)
                .AsNoTracking()
                .ToListAsync();
        }



        public async Task<HealthAuthoritySite> CreateSiteAsync(HealthAuthoritySiteVendorViewModel viewModel)
        {
            viewModel.ThrowIfNull(nameof(viewModel));

            var site = _mapper.Map<HealthAuthoritySite>(viewModel);

            _context.HealthAuthoritySites.Add(site);
            await _context.SaveChangesAsync();

            return site;
        }

        public async Task<HealthAuthoritySite> GetSiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .SingleOrDefaultAsync(ha => ha.Id == siteId);
        }

        public async Task UpdateCareTypeAsync(int siteId, int healthAuthorityId, string careType)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(ct => ct.Id == siteId);

            site.CareType = new HealthAuthorityCareType
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                CareType = careType
            };

            await _context.SaveChangesAsync();
        }


        public async Task UpdateVendorAsync(int siteId, int healthAuthorityId, int vendorCode)
        {
            var site = await _context.HealthAuthoritySites
                .SingleOrDefaultAsync(ct => ct.Id == siteId);

            site.Vendor = new HealthAuthorityVendor
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                VendorCode = vendorCode
            };

            await _context.SaveChangesAsync();
        }
    }
}
