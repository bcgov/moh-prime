using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Prime.Models;
using Prime.HttpClients;
using Prime.ViewModels.Labtech;
using System.Security.Claims;

namespace Prime.Services
{
    public class LabtechService : BaseService, ILabtechService
    {
        public LabtechService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<bool> CreateLabtechAsync(LabtechCreateModel labtech, ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
