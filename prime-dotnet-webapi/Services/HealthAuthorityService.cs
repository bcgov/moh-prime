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
using Prime.ViewModels.HealthAuthoritySites.Internal;
using Prime.Engines;
using System;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;

namespace Prime.Services
{
    public class HealthAuthorityService : BaseService, IHealthAuthorityService
    {
        private readonly IDocumentManagerClient _documentClient;
        private readonly IMapper _mapper;

        public HealthAuthorityService(
            ApiDbContext context,
            IDocumentManagerClient documentClient,
            ILogger<HealthAuthorityService> logger,
            IMapper mapper)
            : base(context, logger)
        {
            _documentClient = documentClient;
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

        public async Task<bool> AuthorizedUserExistsOnHealthAuthorityAsync(int healthAuthorityId, int authorizedUserId)
        {
            return await _context.AuthorizedUsers
                .Where(u => u.HealthAuthorityCode == (HealthAuthorityCode)healthAuthorityId
                    && u.Id == authorizedUserId)
                .AnyAsync();
        }

        public async Task<bool> UpdateCareTypesAsync(int healthAuthorityId, IEnumerable<string> careTypes)
        {
            var existingCareTypes = await _context.HealthAuthorityCareTypes
                .Where(ct => ct.HealthAuthorityOrganizationId == healthAuthorityId)
                .ToListAsync();

            var incomingCareTypes = careTypes
                .Select(careType => new HealthAuthorityCareType
                {
                    HealthAuthorityOrganizationId = healthAuthorityId,
                    CareType = careType
                });

            var results = EntityMatcher
                .MatchUsing((HealthAuthorityCareType ct) => ct.CareType.ToLower())
                .Match(existingCareTypes, incomingCareTypes);

            if (await _context.HealthAuthoritySites
                    .AnyAsync(has => has.HealthAuthorityOrganizationId == healthAuthorityId
                    && results.Dropped.Select(d => d.Id).Contains(has.HealthAuthorityCareTypeId.Value)))
            {
                return false;
            }

            _context.HealthAuthorityCareTypes.RemoveRange(results.Dropped);

            _context.HealthAuthorityCareTypes.AddRange(results.Added);

            await _context.SaveChangesAsync();

            return true;
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

        public async Task<bool> UpdateVendorsAsync(int healthAuthorityId, IEnumerable<int> vendorCodes)
        {
            var existingVendors = await _context.HealthAuthorityVendors
                .Where(ct => ct.HealthAuthorityOrganizationId == healthAuthorityId)
                .ToListAsync();

            var incomingVendors = vendorCodes
                .Select(code => new HealthAuthorityVendor
                {
                    HealthAuthorityOrganizationId = healthAuthorityId,
                    VendorCode = code
                });

            var results = EntityMatcher
                .MatchUsing((HealthAuthorityVendor v) => v.VendorCode)
                .Match(existingVendors, incomingVendors);

            if (await _context.HealthAuthoritySites
                    .AnyAsync(has => has.HealthAuthorityOrganizationId == healthAuthorityId
                    && results.Dropped.Select(d => d.Id).Contains(has.HealthAuthorityVendorId)))
            {
                return false;
            }

            _context.HealthAuthorityVendors.RemoveRange(results.Dropped);

            _context.HealthAuthorityVendors.AddRange(results.Added);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ValidateSiteSelectionsAsync(int healthAuthorityId, HealthAuthoritySiteUpdateModel updateModel)
        {
            var orgDto = await _context.HealthAuthorities
                .AsNoTracking()
                .Where(ha => ha.Id == healthAuthorityId)
                .ProjectTo<HealthAuthoritySelectionDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return ValidateSiteSelectionsInternal(orgDto, _mapper.Map<SiteSelectionDto>(updateModel));
        }

        public async Task<bool> ValidateSiteSelectionsAsync(int healthAuthorityId, int healthAuthoritySiteId)
        {
            var orgDto = await _context.HealthAuthorities
                .AsNoTracking()
                .Where(ha => ha.Id == healthAuthorityId)
                .ProjectTo<HealthAuthoritySelectionDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            var siteDto = await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(site => site.Id == healthAuthoritySiteId)
                .ProjectTo<SiteSelectionDto>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return ValidateSiteSelectionsInternal(orgDto, siteDto);
        }

        private static bool ValidateSiteSelectionsInternal(HealthAuthoritySelectionDto org, SiteSelectionDto site)
        {
            static bool ValidateIfSpecified(IEnumerable<int> orgValues, int? siteValue)
            {
                return siteValue == null
                    || orgValues.Contains(siteValue.Value);
            }

            return ValidateIfSpecified(org.VendorIds, site.HealthAuthorityVendorId)
                && ValidateIfSpecified(org.CareTypeIds, site.HealthAuthorityCareTypeId)
                && ValidateIfSpecified(org.PharmanetAdministratorIds, site.HealthAuthorityPharmanetAdministratorId)
                && ValidateIfSpecified(org.TechnicalSupportIds, site.HealthAuthorityTechnicalSupportId);
        }

        public async Task<bool> VendorExistsOnHealthAuthorityAsync(int healthAuthorityId, int healthAuthorityVendorId)
        {
            return await _context.HealthAuthorityVendors
                .AsNoTracking()
                .AnyAsync(vendor => vendor.Id == healthAuthorityVendorId
                    && vendor.HealthAuthorityOrganizationId == healthAuthorityId);
        }
        public async Task<IEnumerable<int>> GetSitesByVendorAsync(int healthAuthorityId, int healthAuthorityVendorId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(has => has.HealthAuthorityOrganizationId == healthAuthorityId
                    && has.HealthAuthorityVendorId == healthAuthorityVendorId)
                .Select(has => has.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetSitesByCareTypeAsync(int healthAuthorityId, int healthAuthorityCareTypeId)
        {
            return await _context.HealthAuthoritySites
                .AsNoTracking()
                .Where(has => has.HealthAuthorityOrganizationId == healthAuthorityId
                    && has.HealthAuthorityCareTypeId == healthAuthorityCareTypeId)
                .Select(has => has.Id)
                .ToListAsync();
        }

        public async Task<HealthAuthorityOrganizationAgreementDocument> AddOrReplaceBusinessLicenceDocumentAsync(int healthAuthorityId, Guid documentGuid)
        {
            var healthAuthority = await _context.HealthAuthorities
                .Include(ha => ha.HealthAuthorityOrganizationAgreementDocument)
                .SingleOrDefaultAsync(ha => ha.Id == healthAuthorityId);

            var filename = await _documentClient.FinalizeUploadAsync(documentGuid, DestinationFolders.HealthAuthorityOrganizationAgreements);
            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            var doc = new HealthAuthorityOrganizationAgreementDocument
            {
                DocumentGuid = documentGuid,
                Filename = filename,
                UploadedDate = DateTimeOffset.Now
            };

            healthAuthority.HealthAuthorityOrganizationAgreementDocument = doc;

            _context.HealthAuthorities.Update(healthAuthority);
            await _context.SaveChangesAsync();

            return doc;
        }
    }
}
