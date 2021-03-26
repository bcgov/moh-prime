using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IBannerService
    {
        Task<BannerViewModel> GetBannerAsync(int bannerId);
        Task<IEnumerable<BannerViewModel>> GetBannersAsync();
        Task<BannerViewModel> GetBannerByLocationAsync(BannerLocationCode locationCode);
        Task<BannerDisplayViewModel> GetActiveBannerByLocationAsync(BannerLocationCode locationCode);
        Task<BannerViewModel> CreateBannerAsync(Banner banner);
        Task RemoveBannerAsync(int bannerId);
        Task<BannerViewModel> UpdateBannerAsync(int bannerId, BannerViewModel updateModel);
        Task<BannerViewModel> CreateOrUpdateBannerAsync(BannerLocationCode locationCode, BannerViewModel updateModel);
        Task RemoveBannerByLocationAsync(BannerLocationCode locationCode);

    }
}
