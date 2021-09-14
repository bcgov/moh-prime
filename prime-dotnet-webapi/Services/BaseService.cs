using Microsoft.Extensions.Logging;

namespace Prime.Services
{
    public abstract class BaseService
    {
        protected const int InvalidId = -1;

        protected readonly ApiDbContext _context;
        protected readonly ILogger _logger;

        protected BaseService(ApiDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
