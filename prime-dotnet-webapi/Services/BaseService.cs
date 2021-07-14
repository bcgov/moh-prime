using Microsoft.AspNetCore.Http;

namespace Prime.Services
{
    public abstract class BaseService
    {
        protected const int InvalidId = -1;

        protected readonly ApiDbContext _context;
        protected readonly IHttpContextAccessor _httpContext;

        protected BaseService(ApiDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
    }
}
