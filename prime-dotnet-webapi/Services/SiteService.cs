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

        public SiteService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IBusinessEventService businessEventService,
            IPartyService partyService)
            : base(context, httpContext)
        {
            _businessEventService = businessEventService;
            _partyService = partyService;
        }

        public async Task<IEnumerable<Site>> GetSitesAsync()
        {
            return await this.GetBaseSiteQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Site>> GetSitesAsync(int partyId)
        {
            return await this.GetBaseSiteQuery()
                .Where(s => s.ProvisionerId == partyId)
                .ToListAsync();
        }

        public async Task<Site> GetSiteAsync(int siteId)
        {
            return await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<int> CreateSiteAsync(Party provisioner)
        {
            if (provisioner == null)
            {
                throw new ArgumentNullException(nameof(provisioner), "Could not create a site, the passed in Party cannot be null.");
            }

            var provsionerId = await _partyService.CreatePartyAsync(provisioner);

            var organization = await this.GetOrganizationByPartyIdAsync(provsionerId);

            if (organization == null)
            {
                organization = new Organization
                { SigningAuthorityId = provsionerId };

                _context.Organizations.Add(organization);

                await _context.SaveChangesAsync();
            }

            var location = new Location { OrganizationId = organization.Id };

            var site = new Site
            {
                ProvisionerId = provsionerId,
                Location = location
            };

            _context.Sites.Add(site);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, provsionerId, "Site Created");

            return site.Id;
        }

        public async Task<int> UpdateSiteAsync(int siteId, Site updatedSite, bool isCompleted = false)
        {
            // TODO signing authority needs a partial update to non-BCSC fields
            // TODO clean up and simplify update function

            var currentSite = await this.GetSiteAsync(siteId);
            var acceptedAgreementDate = currentSite.Location.Organization.AcceptedAgreementDate;
            var submittedDate = currentSite.SubmittedDate;
            var currentIsCompleted = currentSite.Completed;
            // BCSC Fields
            var userId = currentSite.Location.Organization.SigningAuthority.UserId;

            _context.Entry(currentSite).CurrentValues.SetValues(updatedSite);

            if (updatedSite.Provisioner?.PhysicalAddress != null)
            {
                this._context.Entry(currentSite.Provisioner.PhysicalAddress).CurrentValues.SetValues(updatedSite.Provisioner.PhysicalAddress);
            }

            UpdateLocation(currentSite.Location, updatedSite.Location);

            UpdateOrganization(currentSite.Location.Organization, updatedSite.Location.Organization);

            // Keep userId the same from BCSC card, do not update
            currentSite.Location.Organization.SigningAuthority.UserId = userId;

            // Update foreign key only if not null
            currentSite.VendorId = (updatedSite.VendorId != 0)
                ? updatedSite.VendorId
                : null;

            // Managed through separate API endpoint, and should never be updated
            currentSite.Location.Organization.AcceptedAgreementDate = acceptedAgreementDate;
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

            _context.Parties.Remove(site.Location.Organization.SigningAuthority);
            _context.Organizations.Remove(site.Location.Organization);
            _context.Locations.Remove(site.Location);
            _context.Parties.Remove(site.Location.AdministratorPharmaNet);
            _context.Parties.Remove(site.Location.PrivacyOfficer);
            _context.Parties.Remove(site.Location.TechnicalSupport);
            _context.Sites.Remove(site);

            await _businessEventService.CreateSiteEventAsync(siteId, (int)provisionerId, "Site Deleted");

            await _context.SaveChangesAsync();
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

        public async Task AcceptCurrentOrganizationAgreementAsync(int signingAuthorityId)
        {
            var organization = await _context.Organizations
                .Where(e => e.SigningAuthorityId == signingAuthorityId)
                .FirstOrDefaultAsync();

            organization.AcceptedAgreementDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<Organization> GetOrganizationByPartyIdAsync(int partyId)
        {
            return await _context.Organizations
                .SingleOrDefaultAsync(o => o.SigningAuthorityId == partyId);
        }

        public async Task<Vendor> GetVendorAsync(int vendorId)
        {
            return await _context.Vendors
                .SingleOrDefaultAsync(s => s.Id == vendorId);
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
                    .ThenInclude(l => l.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.PrivacyOfficer)
                .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.AdministratorPharmaNet)
                .ThenInclude(p => p.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.TechnicalSupport)
                        .ThenInclude(p => p.PhysicalAddress);
        }
    }
}
