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

        public async Task<int> UpdateCareTypesAsync(int healthAuthorityId, string[] careTypes)
        {
            var healthAuthority = await _context.HealthAuthorities
                .SingleOrDefaultAsync(ha => ha.Id == healthAuthorityId);

            if (healthAuthority.CareTypes != null)
            {
                foreach (var careType in healthAuthority.CareTypes)
                {
                    _context.Remove(careType);
                }
            }

            if (careTypes.Length != 0)
            {
                foreach (var careType in careTypes)
                {
                    var newCareType = new HealthAuthorityCareType { HealthAuthorityOrganizationId = healthAuthority.Id, CareType = careType };
                    _context.Entry(newCareType).State = EntityState.Added;
                }
            }

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

        public async Task<int> UpdateVendorsAsync(int healthAuthorityId, int[] vendors)
        {
            var healthAuthority = await _context.HealthAuthorities
                .SingleOrDefaultAsync(ha => ha.Id == healthAuthorityId);

            if (healthAuthority.Vendors != null)
            {
                foreach (var vendor in healthAuthority.Vendors)
                {
                    _context.Remove(vendor);
                }
            }

            if (vendors.Length != 0)
            {
                foreach (var vendor in vendors)
                {
                    var newVendor = new HealthAuthorityVendor { HealthAuthorityOrganizationId = healthAuthority.Id, VendorCode = vendor };
                    _context.Entry(newVendor).State = EntityState.Added;
                }
            }

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

        public async Task UpdateContacts<T>(int healthAuthorityOrganizationId, IEnumerable<Contact> contacts) where T : HealthAuthorityContact, new()
        {
            var oldXrefs = await _context.HealthAuthorityContacts
                .Where(c => c.HealthAuthorityOrganizationId == healthAuthorityOrganizationId)
                .OfType<T>()
                .ToListAsync();

            _context.Remove(oldXrefs);

            var oldContacts = oldXrefs.Select(x => x.Contact);

            _context.Remove(oldContacts);

            var xref = contacts.Select(c => new T
            {
                HealthAuthorityOrganizationId = healthAuthorityOrganizationId,
                ContactId = c.Id
            });

            _context.Add(contacts);
            _context.Add(xref);

            await _context.SaveChangesAsync();
        }
    }
}
