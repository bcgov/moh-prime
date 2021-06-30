using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.ViewModels;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.Models.HealthAuthorities;

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

        public async Task<HealthAuthoritySite> GetSiteAsync(int siteId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .SingleOrDefaultAsync(ha => ha.Id == siteId);
        }

        public async Task UpdateCareTypeAsync(int siteId, string careType)
        {
            // var oldCareTypes = await _context.HealthAuthorityCareTypes
            //     .Where(ct => ct.HealthAuthorityOrganizationId == healthAuthorityId)
            //     .ToListAsync();

            // _context.HealthAuthorityCareTypes.RemoveRange(oldCareTypes);

            // var newCareTypes = careTypes.Select(careType => new HealthAuthorityCareType
            // {
            //     HealthAuthorityOrganizationId = healthAuthorityId,
            //     CareType = careType
            // });

            // _context.HealthAuthorityCareTypes.AddRange(newCareTypes);

            await _context.SaveChangesAsync();
        }


        public async Task UpdateVendorAsync(int siteId, int vendorCode)
        {
            // var oldVendors = await _context.HealthAuthorityVendors
            //     .Where(ct => ct.HealthAuthorityOrganizationId == healthAuthorityId)
            //     .ToListAsync();

            // _context.HealthAuthorityVendors.RemoveRange(oldVendors);

            // var newVendors = vendorCodes.Select(code => new HealthAuthorityVendor
            // {
            //     HealthAuthorityOrganizationId = healthAuthorityId,
            //     VendorCode = code
            // });

            // _context.HealthAuthorityVendors.AddRange(newVendors);

            await _context.SaveChangesAsync();
        }
    }
}
