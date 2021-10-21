using Microsoft.Extensions.Logging;

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
    }
}
