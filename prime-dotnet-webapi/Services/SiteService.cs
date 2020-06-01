using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

// TODO add logging
namespace Prime.Services
{
    public class SiteService : BaseService, ISiteService
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IPartyService _partyService;
        private readonly IOrganizationService _organizationService;

        public SiteService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IBusinessEventService businessEventService,
            IPartyService partyService,
            IOrganizationService organizationService)
            : base(context, httpContext)
        {
            _businessEventService = businessEventService;
            _partyService = partyService;
            _organizationService = organizationService;
        }

        public async Task<IEnumerable<Site>> GetSitesAsync()
        {
            return await this.GetBaseSiteQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Site>> GetSitesAsync(int organizationId)
        {
            return await this.GetBaseSiteQuery()
                .Where(s => s.Location.OrganizationId == organizationId)
                .ToListAsync();
        }

        public async Task<Site> GetSiteAsync(int siteId)
        {
            return await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<int> CreateSiteAsync(int organizationId)
        {
            var organization = await _organizationService.GetOrganizationAsync(organizationId);

            if (organization == null)
            {
                throw new ArgumentNullException(nameof(organization), "Could not create a site, the passed in Organization doesnt exist.");
            }

            var location = new Location { OrganizationId = organization.Id };

            // Site provisionerId should be equal to organization signingAuthorityId
            var site = new Site
            {
                ProvisionerId = organization.SigningAuthorityId,
                Location = location
            };

            _context.Sites.Add(site);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, organization.SigningAuthorityId, "Site Created");

            return site.Id;
        }

        public async Task<int> UpdateSiteAsync(int siteId, Site updatedSite, bool isCompleted = false)
        {
            var currentSite = await this.GetSiteAsync(siteId);
            var submittedDate = currentSite.SubmittedDate;
            var currentIsCompleted = currentSite.Completed;

            _context.Entry(currentSite).CurrentValues.SetValues(updatedSite);

            if (updatedSite.Provisioner?.PhysicalAddress != null)
            {
                this._context.Entry(currentSite.Provisioner.PhysicalAddress).CurrentValues.SetValues(updatedSite.Provisioner.PhysicalAddress);
            }

            UpdateLocation(currentSite.Location, updatedSite.Location);

            // Update foreign key only if not null
            currentSite.VendorId = (updatedSite.VendorId != 0)
                ? updatedSite.VendorId
                : null;

            // Managed through separate API endpoint, and should never be updated
            currentSite.SubmittedDate = submittedDate;

            // Registration has been completed
            currentSite.Completed = (isCompleted == true)
                ? isCompleted
                : currentIsCompleted;

            await _businessEventService.CreateSiteEventAsync(currentSite.Id, (int)currentSite.Provisioner.Id, "Site Updated");

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        private void UpdateOrganization(Organization current, Organization updated)
        {
            this._context.Entry(current).CurrentValues.SetValues(updated);

            this._context.Entry(current.SigningAuthority).CurrentValues.SetValues(updated.SigningAuthority);

            if (updated.SigningAuthority?.PhysicalAddress != null)
            {
                if (current.SigningAuthority?.PhysicalAddress == null)
                {
                    current.SigningAuthority.PhysicalAddress = updated.SigningAuthority.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(current.SigningAuthority.PhysicalAddress).CurrentValues.SetValues(updated.SigningAuthority.PhysicalAddress);
                }
            }

            if (updated.SigningAuthority?.MailingAddress != null)
            {
                if (current.SigningAuthority?.MailingAddress == null)
                {
                    current.SigningAuthority.MailingAddress = updated.SigningAuthority.MailingAddress;
                }
                else
                {
                    this._context.Entry(current.SigningAuthority.MailingAddress).CurrentValues.SetValues(updated.SigningAuthority.MailingAddress);
                }
            }
        }

        private void UpdateLocation(Location current, Location updated)
        {
            this._context.Entry(current).CurrentValues.SetValues(updated);

            if (updated?.PhysicalAddress != null)
            {
                if (current.PhysicalAddress == null)
                {
                    current.PhysicalAddress = updated.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(current.PhysicalAddress).CurrentValues.SetValues(updated.PhysicalAddress);
                }
            }

            if (updated?.AdministratorPharmaNet != null)
            {
                if (current.AdministratorPharmaNet == null)
                {
                    current.AdministratorPharmaNet = updated.AdministratorPharmaNet;
                }
                else
                {
                    this._context.Entry(current.AdministratorPharmaNet).CurrentValues.SetValues(updated.AdministratorPharmaNet);
                }

                _partyService.UpdatePartyAddress(current.AdministratorPharmaNet, updated.AdministratorPharmaNet);
            }

            if (updated?.PrivacyOfficer != null)
            {
                if (current.PrivacyOfficer == null)
                {
                    current.PrivacyOfficer = updated.PrivacyOfficer;
                }
                else
                {
                    this._context.Entry(current.PrivacyOfficer).CurrentValues.SetValues(updated.PrivacyOfficer);
                }

                _partyService.UpdatePartyAddress(current.PrivacyOfficer, updated.PrivacyOfficer);
            }

            if (updated?.TechnicalSupport != null)
            {
                if (current.TechnicalSupport == null)
                {
                    current.TechnicalSupport = updated.TechnicalSupport;
                }
                else
                {
                    this._context.Entry(current.TechnicalSupport).CurrentValues.SetValues(updated.TechnicalSupport);
                }

                _partyService.UpdatePartyAddress(current.TechnicalSupport, updated.TechnicalSupport);
            }

            if (updated?.BusinessHours != null)
            {
                if (current.BusinessHours != null)
                {
                    foreach (var businessHour in current.BusinessHours)
                    {
                        _context.Remove(businessHour);
                    }
                }

                foreach (var businessHour in updated.BusinessHours)
                {
                    businessHour.LocationId = current.Id;
                    _context.Entry(businessHour).State = EntityState.Added;
                }
            }
        }

        public async Task DeleteSiteAsync(int siteId)
        {
            var site = await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);

            var provisionerId = site.ProvisionerId;

            if (site == null)
            {
                return;
            }

            _context.Addresses.Remove(site.Location.Organization.SigningAuthority.PhysicalAddress);
            _context.Addresses.Remove(site.Location.Organization.SigningAuthority.MailingAddress);
            _context.Parties.Remove(site.Location.Organization.SigningAuthority);
            _context.Organizations.Remove(site.Location.Organization);

            // Check if relation exists before delete to allow delete of incomplete registrations
            if (site.Location != null)
            {
                if (site.Location.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(site.Location.PhysicalAddress);
                }
                _context.Locations.Remove(site.Location);

                DeletePartyFromLocation(site.Location.AdministratorPharmaNet);
                DeletePartyFromLocation(site.Location.PrivacyOfficer);
                DeletePartyFromLocation(site.Location.PrivacyOfficer);
            }
            _context.Sites.Remove(site);

            await _businessEventService.CreateSiteEventAsync(siteId, (int)provisionerId, "Site Deleted");

            await _context.SaveChangesAsync();
        }

        private void DeletePartyFromLocation(Party party)
        {
            if (party != null)
            {
                if (party.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(party.PhysicalAddress);
                }
                _context.Parties.Remove(party);
            }
        }

        public async Task<Site> SubmitRegistrationAsync(int siteId)
        {
            var site = await GetSiteAsync(siteId);
            site.SubmittedDate = DateTimeOffset.Now;
            _context.Update(site);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not submit the site.");
            }

            return site;
        }

        public async Task<Site> GetSiteNoTrackingAsync(int siteId)
        {
            return await this.GetBaseSiteQuery()
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<IEnumerable<BusinessEvent>> GetSiteBusinessEvents(int siteId)
        {
            return await _context.BusinessEvents
                .Where(e => e.SiteId == siteId)
                .OrderByDescending(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<Vendor> GetVendorAsync(int vendorId)
        {
            return await _context.Vendors
                .SingleOrDefaultAsync(s => s.Id == vendorId);
        }

        public async Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, Guid documentGuid, string filename)
        {
            var businessLicence = new BusinessLicence
            {
                DocumentGuid = documentGuid,
                SiteId = siteId,
                FileName = filename,
            };

            _context.BusinessLicences.Add(businessLicence);

            var updated = await _context.SaveChangesAsync();
            if (updated < 1)
            {
                throw new InvalidOperationException($"Could not add business licence.");
            }

            return businessLicence;
        }

        public async Task<IEnumerable<BusinessLicence>> GetBusinessLicencesAsync(int siteId)
        {
            return await _context.BusinessLicences.Where(bl => bl.SiteId == siteId).ToListAsync();
        }

        private void ReplaceExistingItems<T>(ICollection<T> dbCollection, ICollection<T> newCollection, int enrolleeId) where T : class, IEnrolleeNavigationProperty
        {
            // Remove existing items
            foreach (var item in dbCollection)
            {
                _context.Remove(item);
            }

            // Create new items
            if (newCollection != null)
            {
                foreach (var item in newCollection)
                {
                    // Prevent the ID from being changed by the incoming changes
                    item.EnrolleeId = enrolleeId;
                    _context.Entry(item).State = EntityState.Added;
                }
            }
        }

        private IQueryable<Site> GetBaseSiteQuery()
        {
            return _context.Sites
                .Include(s => s.Provisioner)
                // .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Vendor)
                .Include(s => s.Location)
                    .ThenInclude(l => l.Organization)
                        .ThenInclude(o => o.SigningAuthority)
                            .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.Organization)
                        .ThenInclude(o => o.SigningAuthority)
                            .ThenInclude(p => p.MailingAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.PrivacyOfficer)
                        .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.AdministratorPharmaNet)
                        .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.TechnicalSupport)
                        .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.BusinessHours);
        }
    }
}
