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
                .Where(s => s.Location.Organization.SigningAuthorityId == partyId)
                .ToListAsync();
        }

        public async Task<Site> GetSiteAsync(int siteId)
        {
            return await this.GetBaseSiteQuery()
                .SingleOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<int> CreateSiteAsync(Site site)
        {
            if (site == null)
            {
                throw new ArgumentNullException(nameof(site), "Could not create a site, the passed in Site cannot be null.");
            }

            var user = _httpContext.HttpContext.User;

            site.Location.Organization.SigningAuthorityId = await _partyService.CreatePartyAsync(site.Location.Organization.SigningAuthority);

            _context.Sites.Add(site);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create Site.");
            }

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Created");

            return site.Id;
        }

        public async Task<int> UpdateSiteAsync(int siteId, Site updatedSite, bool isCompleted = false)
        {
            // TODO wholesale change or based use view model

            var site = await this.GetBaseSiteQuery()
                .SingleAsync();

            _context.Entry(site).CurrentValues.SetValues(updatedSite);

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Updated");

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

            await _businessEventService.CreateSiteEventAsync(site.Id, "Site Deleted");

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

        private IQueryable<Site> GetBaseSiteQuery()
        {
            return _context.Sites
                .Include(s => s.Vendor)
                .Include(s => s.Location)
                    .ThenInclude(l => l.Organization)
                        .ThenInclude(o => o.SigningAuthority)
                .Include(s => s.Location)
                    .ThenInclude(l => l.Address)
                .Include(s => s.Location)
                    .ThenInclude(l => l.PrivacyOfficer)
                .Include(s => s.Location)
                    .ThenInclude(l => l.AdministratorPharmaNet)
                .Include(s => s.Location)
                    .ThenInclude(l => l.TechnicalSupport);
        }
    }
}
