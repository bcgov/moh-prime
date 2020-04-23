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
            var currentIsCompleted = currentSite.Completed;
            // BCSC Fields
            var userId = currentSite.Location.Organization.SigningAuthority.UserId;

            _context.Entry(currentSite).CurrentValues.SetValues(updatedSite);

            if (updatedSite.Provisioner?.PhysicalAddress != null)
            {
                this._context.Entry(currentSite.Provisioner.PhysicalAddress).CurrentValues.SetValues(updatedSite.Provisioner.PhysicalAddress);
            }

            this._context.Entry(currentSite.Location).CurrentValues.SetValues(updatedSite.Location);

            if (updatedSite.Location?.PhysicalAddress != null)
            {
                if (currentSite.Location.PhysicalAddress == null)
                {
                    currentSite.Location.PhysicalAddress = updatedSite.Location.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(currentSite.Location.PhysicalAddress).CurrentValues.SetValues(updatedSite.Location.PhysicalAddress);
                }
            }

            if (updatedSite.Location?.AdministratorPharmaNet != null)
            {
                if (currentSite.Location.AdministratorPharmaNet == null)
                {
                    currentSite.Location.AdministratorPharmaNet = updatedSite.Location.AdministratorPharmaNet;
                }
                else
                {
                    this._context.Entry(currentSite.Location.AdministratorPharmaNet).CurrentValues.SetValues(updatedSite.Location.AdministratorPharmaNet);
                }
            }

            if (updatedSite.Location?.AdministratorPharmaNet?.PhysicalAddress != null)
            {
                if (currentSite.Location.AdministratorPharmaNet.PhysicalAddress == null)
                {
                    currentSite.Location.AdministratorPharmaNet.PhysicalAddress = updatedSite.Location?.AdministratorPharmaNet.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(currentSite.Location.AdministratorPharmaNet.PhysicalAddress).CurrentValues.SetValues(updatedSite.Location?.AdministratorPharmaNet.PhysicalAddress);
                }
            }

            if (updatedSite.Location?.PrivacyOfficer != null)
            {
                if (currentSite.Location.PrivacyOfficer == null)
                {
                    currentSite.Location.PrivacyOfficer = updatedSite.Location.PrivacyOfficer;
                }
                else
                {
                    this._context.Entry(currentSite.Location.PrivacyOfficer).CurrentValues.SetValues(updatedSite.Location.PrivacyOfficer);
                }
            }

            if (updatedSite.Location?.PrivacyOfficer?.PhysicalAddress != null)
            {
                if (currentSite.Location.PrivacyOfficer.PhysicalAddress == null)
                {
                    currentSite.Location.PrivacyOfficer.PhysicalAddress = updatedSite.Location.PrivacyOfficer.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(currentSite.Location.PrivacyOfficer.PhysicalAddress).CurrentValues.SetValues(updatedSite.Location.PrivacyOfficer.PhysicalAddress);
                }
            }

            if (updatedSite.Location?.TechnicalSupport != null)
            {
                if (currentSite.Location.TechnicalSupport == null)
                {
                    currentSite.Location.TechnicalSupport = updatedSite.Location.TechnicalSupport;
                }
                else
                {
                    this._context.Entry(currentSite.Location.TechnicalSupport).CurrentValues.SetValues(updatedSite.Location.TechnicalSupport);
                }
            }

            if (updatedSite.Location?.TechnicalSupport?.PhysicalAddress != null)
            {
                if (currentSite.Location.TechnicalSupport.PhysicalAddress == null)
                {
                    currentSite.Location.TechnicalSupport.PhysicalAddress = updatedSite.Location.TechnicalSupport.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(currentSite.Location.TechnicalSupport.PhysicalAddress).CurrentValues.SetValues(updatedSite.Location.TechnicalSupport.PhysicalAddress);
                }
            }

            this._context.Entry(currentSite.Location.Organization).CurrentValues.SetValues(updatedSite.Location.Organization);


            this._context.Entry(currentSite.Location.Organization.SigningAuthority).CurrentValues.SetValues(updatedSite.Location.Organization.SigningAuthority);

            if (updatedSite.Location?.Organization?.SigningAuthority?.PhysicalAddress != null)
            {
                if (currentSite.Location.Organization?.SigningAuthority?.PhysicalAddress == null)
                {
                    currentSite.Location.Organization.SigningAuthority.PhysicalAddress = updatedSite.Location?.Organization.SigningAuthority.PhysicalAddress;
                }
                else
                {
                    this._context.Entry(currentSite.Location.Organization.SigningAuthority.PhysicalAddress).CurrentValues.SetValues(updatedSite.Location?.Organization.SigningAuthority.PhysicalAddress);
                }
            }

            // Keep userId the same
            currentSite.Location.Organization.SigningAuthority.UserId = userId;

            // Update foreign key only if not null
            currentSite.VendorId = (updatedSite.VendorId != 0)
                ? updatedSite.VendorId
                : null;

            // Managed through separate API endpoint, and should never be updated
            currentSite.Location.Organization.AcceptedAgreementDate = acceptedAgreementDate;

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

        private void AddOrUpdate()
        {
            // if (updatedSite.Location?.AdministratorPharmaNet != null)
            // {
            //     if (currentSite.Location.AdministratorPharmaNet == null)
            //     {
            //         currentSite.Location.AdministratorPharmaNet = updatedSite.Location.AdministratorPharmaNet;
            //     }
            //     else
            //     {
            //         this._context.Entry(currentSite.Location.AdministratorPharmaNet).CurrentValues.SetValues(updatedSite.Location.AdministratorPharmaNet);
            //     }
            // }
        }

        public async Task DeleteSiteAsync(int siteId)
        {
            var site = await _context.Sites
                .SingleOrDefaultAsync(s => s.Id == siteId);

            if (site == null)
            {
                return;
            }

            _context.Sites.Remove(site);

            await _businessEventService.CreateSiteEventAsync(site.Id, (int)site.ProvisionerId, "Site Deleted");

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
