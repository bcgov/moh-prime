using System;
using Microsoft.AspNetCore.Http;

namespace Prime.Services
{
    public class DefaultHibcApiService : BaseService, IHibcApiService
    {
        public DefaultHibcApiService(
            ApiDbContext context, IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public void ValidateCollegeLicense()
        {

        }
    }
}
