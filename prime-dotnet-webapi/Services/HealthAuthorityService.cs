using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
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
            IHttpContextAccessor httpContext,
            IMapper mapper)
            : base(context, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<bool> HealthAuthorityExistsAsync(int healthAuthorityId)
        {
            return await _context.HealthAuthorities
                .AsNoTracking()
                .AnyAsync(ha => ha.Id == healthAuthorityId);
        }

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

        public async Task UpdateCareTypesAsync(int healthAuthorityId, string[] careTypes)
        {
            var oldCareTypes = await _context.HealthAuthorityCareTypes
                .Where(ct => ct.HealthAuthorityOrganizationId == healthAuthorityId)
                .ToListAsync();

            _context.HealthAuthorityCareTypes.RemoveRange(oldCareTypes);

            var newCareTypes = careTypes.Select(careType => new HealthAuthorityCareType
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                CareType = careType
            });

            _context.HealthAuthorityCareTypes.AddRange(newCareTypes);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateVendorsAsync(int healthAuthorityId, int[] vendorCodes)
        {
            var oldVendors = await _context.HealthAuthorityVendors
                .Where(ct => ct.HealthAuthorityOrganizationId == healthAuthorityId)
                .ToListAsync();

            _context.HealthAuthorityVendors.RemoveRange(oldVendors);

            var newVendors = vendorCodes.Select(code => new HealthAuthorityVendor
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                VendorCode = code
            });

            _context.HealthAuthorityVendors.AddRange(newVendors);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateContacts<T>(int healthAuthorityId, IEnumerable<Contact> contacts) where T : HealthAuthorityContact, new()
        {
            var oldContacts = await _context.HealthAuthorityContacts
                .Include(c => c.Contact)
                .Where(c => c.HealthAuthorityOrganizationId == healthAuthorityId)
                .OfType<T>()
                .ToListAsync();

            _context.HealthAuthorityContacts.RemoveRange(oldContacts);
            _context.Contacts.RemoveRange(oldContacts.Select(x => x.Contact));

            var newContacts = contacts.Select(contact => new T
            {
                HealthAuthorityOrganizationId = healthAuthorityId,
                Contact = contact
            });

            _context.Add(newContacts);

            await _context.SaveChangesAsync();
        }
    }
}
