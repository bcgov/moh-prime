using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;

namespace Prime.Services
{
    public class LocationService : BaseService, ILocationService
    {
        private readonly ISiteService _siteService;

        public LocationService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            ISiteService siteService)
            : base(context, httpContext)
        {
            _siteService = siteService;
        }

        public async Task<int> CreateLocationAsync(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location), "Could not create an location, the passed in Location cannot be null.");
            }

            _context.Locations.Add(location);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create location.");
            }

            return location.Id;
        }

        public async Task<int> UpdateLocationAsync(int locationId, Location location)
        {
            var currentLocation = await GetBaseLocationQuery()
                .SingleAsync(l => l.Id == locationId);

            _context.Entry(currentLocation).CurrentValues.SetValues(location);

            UpdateLocationAddress(currentLocation, location);

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public void UpdateLocationAddress(Location current, Location updated)
        {
            if (updated.PhysicalAddress != null)
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
        }

        public async Task<int> SavePatchLocationAsync(Location location, int isCompletedSiteId = 0)
        {
            // Site + Location loop has been completed
            // TODO update this on site instead. Currently Location object is last page patched
            if (isCompletedSiteId != 0)
            {
                var site = await _siteService.GetSiteAsync(isCompletedSiteId);
                site.Completed = true;
                _context.Entry(site).State = EntityState.Modified;
            }

            _context.Entry(location).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }


        public async Task DeleteLocationAsync(int locationId)
        {
            var location = await GetBaseLocationQuery()
                .SingleOrDefaultAsync(l => l.Id == locationId);

            if (location == null)
            {
                return;
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }

        private IQueryable<Location> GetBaseLocationQuery()
        {
            return _context.Locations
                .Include(l => l.PhysicalAddress)
                .Include(l => l.BusinessHours);
        }

        public async Task<Location> GetLocationAsync(int locationId)
        {
            return await this.GetBaseLocationQuery()
            .SingleOrDefaultAsync(l => l.Id == locationId);
        }
    }
}
