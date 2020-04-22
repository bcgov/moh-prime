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

            var site = await this.GetSiteNoTrackingAsync(siteId);

            var acceptedAgreementDate = site.Location.Organization.AcceptedAgreementDate;

            _context.Entry(site).CurrentValues.SetValues(updatedSite);
            _context.Organizations.Remove(site.Location.Organization);
            _context.Locations.Remove(site.Location);

            if (site.Provisioner.PhysicalAddress != null)
            {
                _context.Addresses.Remove(site.Provisioner.PhysicalAddress);
                site.Provisioner.PhysicalAddress = updatedSite.Provisioner.PhysicalAddress;
            }

            // _context.Parties.Remove(site.Provisioner);

            site.Location = updatedSite.Location;
            site.Location.Organization = updatedSite.Location.Organization;
            site.Provisioner = updatedSite.Provisioner;

            // site.Location.Organization.SigningAuthority = updatedSite.Location.Organization.SigningAuthority;



            //Never update
            site.Location.Organization.AcceptedAgreementDate = acceptedAgreementDate;

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
