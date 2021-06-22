using Microsoft.AspNetCore.Http;

namespace Prime.Services
{
    public abstract class BaseService
    {
        protected readonly ApiDbContext _context;

        protected readonly IHttpContextAccessor _httpContext;

        protected const int InvalidId = -1;

        protected BaseService(ApiDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
    }
}
