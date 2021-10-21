using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public class V2SiteService : BaseService, IV2SiteService
    {
        public V2SiteService(
            ApiDbContext context,
            ILogger<V2SiteService> logger)
            : base(context, logger)
        {

        }

        public async Task<bool> PecAssignableAsync(int siteId, string pec)
        {
            var siteDto = await _context.Sites
                .AsNoTracking()
                .Where(site => site.Id == siteId)
                .Select(site => new
                {
                    site.CareSettingCode,
                    site.PEC
                })
                .SingleOrDefaultAsync();

            if (siteDto?.CareSettingCode == null || string.IsNullOrWhiteSpace(pec))
            {
                return false;
            }

            if (siteDto.CareSettingCode == (int)CareSettingType.HealthAuthority
                || siteDto.PEC == pec)
            {
                return true;
            }

            return !await _context.Sites
                .AsNoTracking()
                .AnyAsync(site => site.PEC == pec);
        }
    }
}
