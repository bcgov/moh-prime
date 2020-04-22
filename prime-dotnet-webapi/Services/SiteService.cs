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
            // Don't perform any updates on final submission
            if (isCompleted)
            {
                var siteTracked = await this.GetSiteAsync(siteId);
                siteTracked.Completed = isCompleted;

                try
                {
                    return await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return 0;
                }
            }

            // TODO signing authority needs a partial update to non-BCSC fields

            var currentSite = await this.GetSiteAsync(siteId);
            var acceptedAgreementDate = currentSite.Location.Organization.AcceptedAgreementDate;
            var userId = currentSite.Location.Organization.SigningAuthority.UserId;

            // // Update signing authority
            // var currentSigningAuthority = currentSite.Location.Organization.SigningAuthority;
            // var updatedSigningAuthority = updatedSite.Location.Organization.SigningAuthority;
            // if (updatedSigningAuthority != null)
            // {
            //     // _context.Parties.Attach(currentSigningAuthority);
            //     currentSigningAuthority.JobRoleTitle = updatedSigningAuthority.JobRoleTitle;
            // }

            _context.Entry(currentSite).CurrentValues.SetValues(updatedSite);
            // _context.Organizations.Remove(currentSite.Location.Organization);
            // _context.Locations.Remove(currentSite.Location);

            if (updatedSite.Provisioner?.PhysicalAddress != null)
            {
                // _context.Addresses.Remove(currentSite.Provisioner.PhysicalAddress);
                // currentSite.Provisioner.PhysicalAddress = updatedSite.Provisioner.PhysicalAddress;
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
                currentSite.Location.AdministratorPharmaNet = updatedSite.Location.AdministratorPharmaNet;
            }

            // currentSite.Location = updatedSite.Location;

            // currentSite.Location.Organization = updatedSite.Location.Organization;
            this._context.Entry(currentSite.Location.Organization).CurrentValues.SetValues(updatedSite.Location.Organization);

            this._context.Entry(currentSite.Location.Organization.SigningAuthority).CurrentValues.SetValues(updatedSite.Location.Organization.SigningAuthority);
            // Keep userId the same
            currentSite.Location.Organization.SigningAuthority.UserId = userId;

            // Update foreign key only if not null
            if (updatedSite.VendorId != 0)
            {
                currentSite.VendorId = updatedSite.VendorId;
                _context.Entry(currentSite).Property("VendorId").IsModified = true;
            }

            // site.Location.Organization.SigningAuthority = updatedSite.Location.Organization.SigningAuthority;

            // Managed through separate API endpoint, and should never be updated
            currentSite.Location.Organization.AcceptedAgreementDate = acceptedAgreementDate;

            // await _businessEventService.CreateSiteEventAsync(site.Id, (int)updatedSite.ProvisionerId, "Site Updated");

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

        private IQueryable<Site> GetBaseSiteQuery()
        {
            return _context.Sites
                .Include(s => s.Provisioner)
                .Include(s => s.Vendor)
                .Include(s => s.Location)
                    .ThenInclude(l => l.Organization)
                        .ThenInclude(o => o.SigningAuthority)
                .Include(s => s.Location)
                    .ThenInclude(l => l.PhysicalAddress)
                .Include(s => s.Location)
                    .ThenInclude(l => l.PrivacyOfficer)
                .Include(s => s.Location)
                    .ThenInclude(l => l.AdministratorPharmaNet)
                .Include(s => s.Location)
                    .ThenInclude(l => l.TechnicalSupport);
        }
    }
}
