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

        public async Task<BannerViewModel> CreateBannerAsync(Banner banner)
        {
            _context.Banners.Add(banner);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create banner.");
            }

            return _mapper.Map<BannerViewModel>(banner); ;
        }

        public async Task<BannerViewModel> GetBannerAsync(int bannerId)
        {
            return await _context.Banners
            .Where(b => b.Id == bannerId)
            .ProjectTo<BannerViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<BannerViewModel> GetBannerByLocationAsync(BannerLocationCode locationCode)
        {
            return await _context.Banners
            .Where(b => b.BannerLocationCode == locationCode)
            .ProjectTo<BannerViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<BannerViewModel>> GetBannersAsync()
        {
            return await _context.Banners
            .ProjectTo<BannerViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<BannerDisplayViewModel> GetActiveBannerByLocationAsync(BannerLocationCode locationCode)
        {
            var currentDate = DateTime.Today;
            return await _context.Banners
                .Where(b => currentDate >= b.StartDate && currentDate <= b.EndDate)
                .Where(b => b.BannerLocationCode == locationCode)
                .ProjectTo<BannerDisplayViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
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

        public async Task RemoveBannerByLocationAsync(BannerLocationCode locationCode)
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

        public async Task<BannerViewModel> UpdateBannerAsync(int bannerId, BannerViewModel updateModel)
        {
            var banner = await _context.Banners
                .SingleOrDefaultAsync(a => a.Id == bannerId);

            _context.Entry(banner).CurrentValues.SetValues(updateModel); // reflection

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

        public async Task<BannerViewModel> CreateOrUpdateBannerAsync(BannerLocationCode locationCode, BannerViewModel updateModel)
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

            _context.Entry(banner).CurrentValues.SetValues(updateModel); // reflection

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
    }
}
