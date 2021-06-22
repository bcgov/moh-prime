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
            IHttpContextAccessor httpContext,
            IMapper mapper)
            : base(context, httpContext)
        {
            _mapper = mapper;
        }

        public async Task<BannerViewModel> GetBannerAsync(BannerLocationCode locationCode)
        {
            var banner = await _context.Banners
                .Where(b => b.BannerLocationCode == locationCode)
                .ProjectTo<BannerViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (banner != null)
            {
                // Banner time stamps are stored as UTC, but are materialized as DateTimeKind.Unspecified
                // Once we upgrade our EF / Npgsql, we can use NodaTime instead
                banner.StartTimestamp = DateTime.SpecifyKind(banner.StartTimestamp, DateTimeKind.Utc);
                banner.EndTimestamp = DateTime.SpecifyKind(banner.EndTimestamp, DateTimeKind.Utc);
            }

            return banner;
        }

        public async Task<BannerDisplayViewModel> GetActiveBannerAsync(BannerLocationCode locationCode, DateTime atTime)
        {
            atTime = atTime.ToUniversalTime();
            return await _context.Banners
                .Where(b => b.BannerLocationCode == locationCode)
                .Where(b => b.StartTimestamp <= atTime && atTime <= b.EndTimestamp)
                .ProjectTo<BannerDisplayViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<BannerViewModel> SetBannerAsync(BannerLocationCode locationCode, BannerViewModel bannerVM)
        {
            var banner = await _context.Banners
                .SingleOrDefaultAsync(b => b.BannerLocationCode == locationCode);

            if (banner == null)
            {
                banner = new Banner
                {
                    BannerLocationCode = locationCode,
                };
                _context.Banners.Add(banner);
            }

            banner.BannerType = bannerVM.BannerType;
            banner.Title = bannerVM.Title;
            banner.Content = bannerVM.Content;
            banner.StartTimestamp = bannerVM.StartTimestamp.ToUniversalTime();
            banner.EndTimestamp = bannerVM.EndTimestamp.ToUniversalTime();

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
                .SingleOrDefaultAsync(b => b.BannerLocationCode == locationCode);

            if (banner != null)
            {
                _context.Banners.Remove(banner);
                await _context.SaveChangesAsync();
            }
        }
    }
}
