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

            var location = await this.GetLocationByOrganizationIdAsync(organizationId);

            if (location == null)
            {
                location = new Location { OrganizationId = organization.Id };
            }

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

            // TODO should create a location controller to avoid these kinds of updates
            UpdateLocation(currentSite.Location, updatedSite.Location);

            // Wholesale replace the remote users
            if (currentSite?.RemoteUsers != null && currentSite?.RemoteUsers.Count() != 0)
            {
                foreach (var remoteUser in currentSite.RemoteUsers)
                {
                    foreach (var location in remoteUser.RemoteUserLocations)
                    {
                        _context.Addresses.Remove(location.PhysicalAddress);
                        _context.RemoteUserLocations.Remove(location);
                    }
                    _context.RemoteUsers.Remove(remoteUser);
                }
            }

            if (updatedSite?.RemoteUsers != null && updatedSite?.RemoteUsers.Count() != 0)
            {
                foreach (var remoteUser in updatedSite.RemoteUsers)
                {
                    remoteUser.SiteId = currentSite.Id;
                    _context.RemoteUsers.Add(remoteUser);
                }
            }

            // Update foreign key only if not null
            currentSite.VendorCode = (updatedSite.VendorCode != 0)
                ? updatedSite.VendorCode
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

        private async Task<Location> GetLocationByOrganizationIdAsync(int organizationId)
        {
            // assmuing an organization only has 1 location
            return await _context.Locations
                .Where(l => l.OrganizationId == organizationId)
                .FirstOrDefaultAsync();
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
                if (updated?.AdministratorPharmaNet?.UserId != Guid.Empty)
                {
                    current.AdministratorPharmaNetId = updated.AdministratorPharmaNetId;
                }
                else
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
            }

            if (updated?.PrivacyOfficer != null)
            {
                if (updated?.PrivacyOfficer?.UserId != Guid.Empty)
                {
                    current.PrivacyOfficerId = updated.PrivacyOfficerId;
                }
                else
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
            }

            if (updated?.TechnicalSupport != null)
            {
                if (updated?.TechnicalSupport?.UserId != Guid.Empty)
                {
                    current.TechnicalSupportId = updated.TechnicalSupportId;
                }
                else
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

        public async Task<int> SavePatchSiteAsync(Site site, bool isCompleted = false)
        {
            // Registration has been completed
            site.Completed = (isCompleted == true)
                ? isCompleted
                : site.Completed;

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
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

            _context.Sites.Remove(site);

            await _businessEventService.CreateSiteEventAsync(siteId, (int)provisionerId, "Site Deleted");

            await _context.SaveChangesAsync();
        }

        private void DeleteLocation(Site site)
        {
            // Check if relation exists before delete to allow delete of incomplete registrations
            if (site.Location != null)
            {
                if (site.Location.PhysicalAddress != null)
                {
                    _context.Addresses.Remove(site.Location.PhysicalAddress);
                }
                _context.Locations.Remove(site.Location);

                DeletePartyFromLocation(site.Location.AdministratorPharmaNet);
                DeletePartyFromLocation(site.Location.TechnicalSupport);
                DeletePartyFromLocation(site.Location.PrivacyOfficer);
            }
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

        public async Task<Vendor> GetVendorAsync(int vendorCode)
        {
            return await _context.Vendors
                .SingleOrDefaultAsync(v => v.Code == vendorCode);
        }

        public async Task<BusinessLicence> AddBusinessLicenceAsync(int siteId, Guid documentGuid, string filename)
        {
            var businessLicence = new BusinessLicence
            {
                DocumentGuid = documentGuid,
                SiteId = siteId,
                FileName = filename,
                UploadedDate = DateTimeOffset.Now
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
            return await _context.BusinessLicences
                .Where(bl => bl.SiteId == siteId)
                .ToListAsync();
        }

        public async Task<BusinessLicence> GetLatestBusinessLicenceAsync(int siteId)
        {
            return await _context.BusinessLicences
                .Where(bl => bl.SiteId == siteId)
                .OrderByDescending(bl => bl.UploadedDate)
                .FirstOrDefaultAsync();
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
                    .ThenInclude(l => l.BusinessHours)
                .Include(s => s.RemoteUsers)
                    .ThenInclude(r => r.RemoteUserLocations)
                        .ThenInclude(rul => rul.PhysicalAddress)
                .Include(s => s.BusinessLicences);
        }
    }
}
