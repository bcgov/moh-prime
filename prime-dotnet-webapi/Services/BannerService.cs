using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Prime.Models;
using Prime.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Prime.Services
{
    public class BannerService : BaseService, IBannerService
    {
        private readonly IMapper _mapper;
        public BannerService(
            ApiDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContext)
            : base(context, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<BannerViewModel> GetBannerAsync(BannerLocationCode locationCode)
        {
            return await _context.Banners
                .Where(b => b.BannerLocationCode == locationCode)
                .ProjectTo<BannerViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<BannerDisplayViewModel> GetActiveBannerAsync(BannerLocationCode locationCode)
        {
            var currentDate = DateTime.Today;
            var banner = await _context.Banners
                .Where(b => b.BannerLocationCode == locationCode)
                .ProjectTo<BannerDisplayViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
            // Comparing of only the Date portion.
            if (banner != null && currentDate.Date >= banner.StartDate.Date && currentDate.Date <= banner.EndDate.Date)
            {
                return banner;
            }
            return null;
        }

        public async Task<BannerViewModel> SetBannerAsync(BannerLocationCode locationCode, BannerViewModel updateModel)
        {
            var banner = await _context.Banners
                .SingleOrDefaultAsync(a => a.BannerLocationCode == locationCode);

            if (banner == null)
            {
                banner = new Banner
                {
                    BannerLocationCode = locationCode,
                };
                _context.Banners.Add(banner);
            }

            _context.Entry(banner).CurrentValues.SetValues(updateModel);

            try
            {
                await _context.SaveChangesAsync();
                return _mapper.Map<BannerViewModel>(banner);
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }

        public async Task DeleteBannerAsync(BannerLocationCode locationCode)
        {
            var banner = await _context.Banners
                            .SingleOrDefaultAsync(a => a.BannerLocationCode == locationCode);

            if (banner == null)
            {
                return;
            }

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
        }
    }
}
