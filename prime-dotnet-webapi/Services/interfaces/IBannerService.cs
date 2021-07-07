using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IBannerService
    {
        Task<IEnumerable<BannerDisplayViewModel>> GetActiveBannersAsync(BannerLocationCode locationCode, DateTime atTime);
        Task<BannerViewModel> GetBannerAsync(int bannerId);
        Task<IEnumerable<BannerViewModel>> GetBannersAsync(BannerLocationCode locationCode);
        Task DeleteBannerAsync(int bannerId);
        Task<BannerViewModel> UpdateBannerAsync(int bannerId, BannerViewModel updateModel);
        Task<BannerViewModel> CreateBannerAsync(BannerLocationCode locationCode, BannerViewModel updateModel);
    }
}
