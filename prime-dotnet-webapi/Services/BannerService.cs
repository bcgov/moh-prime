using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public class BannerService : BaseService, IBannerService
    {
        private readonly IMapper _mapper;

        public BannerService(
            ApiDbContext context,
            ILogger<BannerService> logger,
            IMapper mapper)
            : base(context, logger)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<BannerDisplayViewModel>> GetActiveBannersAsync(BannerLocationCode locationCode, DateTime atTime)
        {
            atTime = atTime.ToUniversalTime();
            return await _context.Banners
                .Where(b => b.BannerLocationCode == locationCode)
                .Where(b => b.StartTimestamp <= atTime && atTime <= b.EndTimestamp)
                .ProjectTo<BannerDisplayViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BannerViewModel> GetBannerAsync(int bannerId)
        {
            var banner = await _context.Banners
                .Where(b => b.Id == bannerId)
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

        public async Task<IEnumerable<BannerViewModel>> GetBannersAsync(BannerLocationCode locationCode)
        {
            var banners = await _context.Banners
                .Where(b => b.BannerLocationCode == locationCode)
                .ProjectTo<BannerViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            banners.ForEach(banner =>
            {
                // Banner time stamps are stored as UTC, but are materialized as DateTimeKind.Unspecified
                // Once we upgrade our EF / Npgsql, we can use NodaTime instead
                banner.StartTimestamp = DateTime.SpecifyKind(banner.StartTimestamp, DateTimeKind.Utc);
                banner.EndTimestamp = DateTime.SpecifyKind(banner.EndTimestamp, DateTimeKind.Utc);
            });

            return banners;
        }

        public async Task DeleteBannerAsync(int bannerId)
        {
            var banner = await _context.Banners
                .SingleOrDefaultAsync(b => b.Id == bannerId);

            if (banner != null)
            {
                _context.Banners.Remove(banner);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<BannerViewModel> UpdateBannerAsync(int bannerId, BannerViewModel updateModel)
        {
            var banner = await _context.Banners
                .SingleOrDefaultAsync(b => b.Id == bannerId);

            banner.BannerType = updateModel.BannerType;
            banner.Title = updateModel.Title;
            banner.Content = updateModel.Content;
            banner.StartTimestamp = updateModel.StartTimestamp.ToUniversalTime();
            banner.EndTimestamp = updateModel.EndTimestamp.ToUniversalTime();

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

        public async Task<BannerViewModel> CreateBannerAsync(BannerLocationCode locationCode, BannerViewModel updateModel)
        {
            var banner = new Banner
            {
                BannerLocationCode = locationCode,
                BannerType = updateModel.BannerType,
                Title = updateModel.Title,
                Content = updateModel.Content,
                StartTimestamp = updateModel.StartTimestamp.ToUniversalTime(),
                EndTimestamp = updateModel.EndTimestamp.ToUniversalTime(),
            };

            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();
            return _mapper.Map<BannerViewModel>(banner);
        }
    }
}
