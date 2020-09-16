using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.Models.Api;

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
            LookupEntity lookupEntity = new LookupEntity();

            lookupEntity.Colleges = await _context.Set<College>()
                .Include(c => c.CollegeLicenses)
                .Include(c => c.CollegePractices)
                .ToListAsync();
            lookupEntity.JobNames = await _context.Set<JobName>().ToListAsync();
            lookupEntity.Licenses = await _context.Set<License>()
                .Include(l => l.CollegeLicenses)
                .ToListAsync();
            lookupEntity.CareSettings = await _context.Set<CareSetting>().ToListAsync();
            lookupEntity.Practices = await _context.Set<Practice>()
                .Include(p => p.CollegePractices)
                .ToListAsync();
            lookupEntity.Statuses = await _context.Set<Status>().ToListAsync();
            lookupEntity.Countries = await _context.Set<Country>().ToListAsync();
            lookupEntity.Provinces = await _context.Set<Province>().ToListAsync();
            lookupEntity.StatusReasons = await _context.Set<StatusReason>().ToListAsync();
            lookupEntity.PrivilegeGroups = await _context.Set<PrivilegeGroup>().ToListAsync();
            lookupEntity.PrivilegeTypes = await _context.Set<PrivilegeType>().ToListAsync();
            lookupEntity.Vendors = await _context.Set<Vendor>().ToListAsync();

            return lookupEntity;
        }
    }
}
