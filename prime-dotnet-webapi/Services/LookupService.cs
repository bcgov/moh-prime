using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.Models.Api;
using Prime.Models.HealthAuthorities;

namespace Prime.Services
{
    public class LookupService : BaseService, ILookupService
    {
        public LookupService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<LookupEntity> GetLookupsAsync()
        {
            return new LookupEntity
            {
                Colleges = await _context.Set<College>()
                    .AsNoTracking()
                    .Include(c => c.CollegeLicenses)
                    .Include(c => c.CollegePractices)
                    .ToListAsync(),
                JobNames = await _context.Set<JobName>()
                    .AsNoTracking()
                    .ToListAsync(),
                Licenses = await _context.Set<License>()
                    .AsNoTracking()
                    .Include(l => l.CollegeLicenses)
                    .ToListAsync(),
                CareSettings = await _context.Set<CareSetting>()
                    .AsNoTracking()
                    .ToListAsync(),
                Practices = await _context.Set<Practice>()
                    .AsNoTracking()
                    .Include(p => p.CollegePractices)
                    .ToListAsync(),
                Statuses = await _context.Set<Status>()
                    .AsNoTracking()
                    .ToListAsync(),
                Countries = await _context.Set<Country>()
                    .AsNoTracking()
                    .ToListAsync(),
                Provinces = await _context.Set<Province>()
                    .AsNoTracking()
                    .ToListAsync(),
                StatusReasons = await _context.Set<StatusReason>()
                    .AsNoTracking()
                    .ToListAsync(),
                Vendors = await _context.Set<Vendor>()
                    .AsNoTracking()
                    .ToListAsync(),
                HealthAuthorities = await _context.Set<HealthAuthority>()
                    .AsNoTracking()
                    .ToListAsync(),
                Facilities = await _context.Set<Facility>()
                    .AsNoTracking()
                    .ToListAsync(),
                CareTypes = await _context.Set<CareType>()
                    .AsNoTracking()
                    .ToListAsync(),
            };
        }
    }
}
