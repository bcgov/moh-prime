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
using Prime.Models.HealthAuthorities;

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

        public async Task<bool> HealthAuthorityExistsAsync(int healthAuthorityId)
        {
            return await _context.HealthAuthorities
                .AsNoTracking()
                .AnyAsync(e => e.Id == healthAuthorityId);
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

        public async Task<int> UpdateCareTypesAsync(int healthAuthorityId, string[] careTypes)
        {
            var healthAuthority = await _context.HealthAuthorities
                .SingleOrDefaultAsync(ha => ha.Id == healthAuthorityId);

            var healthAuthorityCareTypes = new List<HealthAuthorityCareType>();
            foreach (var careType in careTypes)
            {
                healthAuthorityCareTypes.Add(new HealthAuthorityCareType { HealthAuthorityOrganization = healthAuthority, CareType = careType });
            }

            healthAuthority.CareTypes = healthAuthorityCareTypes;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return InvalidId;
            }

            return healthAuthority.Id;
        }

        public async Task UpdateContacts<T>(int healthAuthorityOrganizationId, IEnumerable<HealthAuthorityContact> contacts) where T : HealthAuthorityContact, new()
        {
            var xref = contacts.Select(c => new T
            {
                HealthAuthorityOrganizationId = healthAuthorityOrganizationId,
                ContactId = c.Id
            });
        }
    }
}
