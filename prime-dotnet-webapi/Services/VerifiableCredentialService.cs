using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Prime.Services
{
    public class VerifiableCredentialsService : BaseService, IVerifiableCredentialService
    {
        public VerifiableCredentialsService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        {

        }
    }
}
