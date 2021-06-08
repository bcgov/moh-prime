using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prime.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;

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

        // TODO: AutoMapper configuration
        public async Task<IEnumerable<HealthAuthorityListViewModel>> GetHealthAuthoritiesAsync()
        {
            return await _context.HealthAuthorities
                .AsNoTracking()
                .ProjectTo<HealthAuthorityListViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<HealthAuthorityViewModel> GetHealthAuthorityAsync(int id)
        {
            return await _context.HealthAuthorities
                .AsNoTracking()
                .ProjectTo<HealthAuthorityViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(ha => ha.Id == id);
        }

        // This Controller is a temp fix for the time being before we most likely move HA's into organizations
        // To reduce bloat we just use one view model for the time being.
        // TODO: review
        public async Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersByHealthAuthorityAsync(HealthAuthorityCode code)
        {
            return await _context.AuthorizedUsers
                .Where(u => u.HealthAuthorityCode == code)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<HealthAuthorityCode>> GetHealthAuthorityCodesWithUnderReviewAuthorizedUsersAsync()
        {
            return await _context.AuthorizedUsers
                .Where(u => u.Status == AccessStatusType.UnderReview)
                .Select(u => u.HealthAuthorityCode)
                .Distinct()
                .ToListAsync();
        }
    }
}
