using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class BannerService : BaseService, IBannerService
    {
        public BannerService(
            ApiDbContext context,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        { }

        public async Task<Banner> CreateBannerAsync(Banner banner)
        {
            _context.Banners.Add(banner);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create banner.");
            }

            return banner;
        }

        public async Task<Banner> GetBannerAsync(int bannerId)
        {
            return await _context.Banners.SingleOrDefaultAsync(b => b.Id == bannerId);
        }

        public async Task<IEnumerable<Banner>> GetBannersAsync()
        {
            return await _context.Banners.ToListAsync();
        }

        public async Task<Banner> GetActiveBannerByLocationAsync(BannerLocationCode locationCode)
        {
            var currentDate = DateTime.Today;
            return await _context.Banners
                .Where(b => currentDate >= b.StartDate && currentDate <= b.EndDate)
                .SingleOrDefaultAsync(b => b.BannerLocationCode == locationCode);
        }

        public async Task RemoveBannerAsync(int bannerId)
        {
            var banner = await _context.Banners
                .SingleOrDefaultAsync(a => a.Id == bannerId);

            if (banner == null)
            {
                return;
            }

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
        }

        public async Task<Banner> UpdateBannerAsync(int bannerId, BannerUpdateViewModel updateModel)
        {
            var banner = await _context.Banners
                .SingleOrDefaultAsync(a => a.Id == bannerId);

            _context.Entry(banner).CurrentValues.SetValues(updateModel); // reflection

            try
            {
                await _context.SaveChangesAsync();
                return banner;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
    }
}
