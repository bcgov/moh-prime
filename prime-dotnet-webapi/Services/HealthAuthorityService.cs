using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthoritySites;
using System;

namespace Prime.Services
{
    public class HealthAuthorityService : BaseService, IHealthAuthorityService
    {
        private readonly IMapper _mapper;

        public HealthAuthorityService(
            ApiDbContext context,
            ILogger<HealthAuthorityService> logger,
            IMapper mapper)
            : base(context, logger)
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
            IQueryable<int> underReviewIds = _context.AuthorizedUsers
                .Where(u => u.Status == AccessStatusType.UnderReview)
                .Select(u => (int)u.HealthAuthorityCode)
                .Distinct();

            return await _context.HealthAuthorities
                .AsNoTracking()
                .ProjectTo<HealthAuthorityListViewModel>(_mapper.ConfigurationProvider, new { underReviewIds })
                .ToListAsync();
        }

        public async Task<HealthAuthorityViewModel> GetHealthAuthorityAsync(int id)
        {
            return await _context.HealthAuthorities
                .AsNoTracking()
                .ProjectTo<HealthAuthorityViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(ha => ha.Id == id);
        }

        // TODO: review this VM
        // This Controller is a temp fix for the time being before we most likely move HA's into organizations
        // To reduce bloat we just use one view model for the time being.
        public async Task<IEnumerable<AuthorizedUserViewModel>> GetAuthorizedUsersAsync(int healthAuthorityId)
        {
            return await _context.AuthorizedUsers
                .Where(u => u.HealthAuthorityCode == (HealthAuthorityCode)healthAuthorityId)
                .ProjectTo<AuthorizedUserViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> AuthorizedUserExistsOnOrganizationAsync(int healthAuthorityId, int authorizedUserId)
        {
            return await _context.AuthorizedUsers
                .Where(u => u.HealthAuthorityCode == (HealthAuthorityCode)healthAuthorityId
                    && u.Id == authorizedUserId)
                .AnyAsync();
        }

        public async Task UpdateCareTypesAsync(int healthAuthorityId, IEnumerable<string> careTypes)
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

        public async Task UpdateContactsAsync<T>(int healthAuthorityId, IEnumerable<ContactViewModel> contacts) where T : HealthAuthorityContact, new()
        {
            var oldContacts = await _context.HealthAuthorityContacts
                .Include(c => c.Contact.PhysicalAddress)
                .Where(c => c.HealthAuthorityOrganizationId == healthAuthorityId)
                .OfType<T>()
                .Select(c => c.Contact)
                .ToListAsync();

            // Should cascade into the HealthAuthorityContact XRef table. We have to save here to drop the Contacts before the Addresses.
            _context.Contacts.RemoveRange(oldContacts);
            await _context.SaveChangesAsync();

            _context.Addresses.RemoveRange(oldContacts.Select(c => c.PhysicalAddress).Where(a => a != null));

            var newContacts = contacts.Select(contact =>
            {
                contact.Id = 0;
                return new T
                {
                    HealthAuthorityOrganizationId = healthAuthorityId,
                    Contact = _mapper.Map<Contact>(contact)
                };
            });

            _context.HealthAuthorityContacts.AddRange(newContacts);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrivacyOfficeAsync(int healthAuthorityId, PrivacyOfficeViewModel privacyOffice)
        {
            var existing = await _context.PrivacyOffices
                .Include(po => po.PhysicalAddress)
                .SingleOrDefaultAsync(po => po.HealthAuthorityOrganizationId == healthAuthorityId);

            if (existing == null)
            {
                var newOffice = _mapper.Map<PrivacyOffice>(privacyOffice);
                newOffice.HealthAuthorityOrganizationId = healthAuthorityId;
                _context.PrivacyOffices.Add(newOffice);
            }
            else
            {
                _mapper.Map(privacyOffice, existing);
            }

            // Implicit call to _context.SaveChanges()
            await UpdateContactsAsync<HealthAuthorityPrivacyOfficer>(healthAuthorityId, new ContactViewModel[] { privacyOffice.PrivacyOfficer });
        }

        public async Task UpdateVendorsAsync(int healthAuthorityId, IEnumerable<int> vendorCodes)
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

        public async Task<bool> PerformSiteValidationBeforeUpdate(int healthAuthorityId, HealthAuthoritySiteUpdateModel updateModel)
        {
            return await _context.HealthAuthorities
                .AsNoTracking()
                .Where(ha => ha.Id == healthAuthorityId
                    && ha.Vendors.Any(v => v.Id == updateModel.HealthAuthorityVendorId)
                    && ha.CareTypes.Any(ct => ct.Id == updateModel.HealthAuthorityCareTypeId)
                    && ha.PharmanetAdministrators.Any(pa => pa.Id == updateModel.HealthAuthorityPharmanetAdministratorId)
                    && ha.TechnicalSupports.Any(ts => ts.Id == updateModel.HealthAuthorityTechnicalSupportId)
                )
                .AnyAsync();
        }
        public async Task<bool> HealthAuthorityVendorExistsAsync(int healthAuthorityVendorId)
        {
            return await _context.HealthAuthorityVendors.AnyAsync(v => v.Id == healthAuthorityVendorId);
        }
    }
}
