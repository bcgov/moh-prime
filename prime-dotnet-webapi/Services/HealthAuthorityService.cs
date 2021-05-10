using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Prime.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public class HealthAuthorityService : BaseService, IHealthAuthorityService
    {
        private readonly IMapper _mapper;
        public HealthAuthorityService(
            ApiDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersByHealthAuthorityAsync(HealthAuthorityCode code)
        {
            return await _context.AuthorizedUsers
                .Where(u => u.HealthAuthorityCode == code)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
