using Microsoft.AspNetCore.Http;

namespace Prime.Services
{
    public abstract class BaseService
    {
        protected readonly ApiDbContext _context;

        protected readonly IHttpContextAccessor _httpContext;

        public BaseService( ApiDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
    }
}